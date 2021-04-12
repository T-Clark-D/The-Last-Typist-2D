using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class Player : MonoBehaviour
{

    public GameObject PlayertextPrefab;
    Text textbox;
    GameObject canvasObj;

    public GameObject gunPivotContainer;
    GameObject gunPivotInstance;
    GameObject textInstance;
    //attributes
    public Boolean CombatMode = false;
    public float speed = 7;
    public float health = 5;
    //sprites
   // public SpriteRenderer SR;
    public Sprite idle;
    public Sprite combat;
    //to do update health when damage taken
    public event Action DamageTaken;
    public event Action OnPlayerDeath;

    private Animator anim;
    private Rigidbody2D RB;
    // Use this for initialization
    Targetable currentTarget;
    private Vector3 aimDirection;
    private List<Targetable> targetedInstances;

    AudioManager audioManager;
    void Start()
    {
        targetedInstances = new List<Targetable>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        //SR = GetComponent<SpriteRenderer>();
        UIUpdates();
        canvasObj = GameObject.Find("Canvas");
        audioManager = FindObjectOfType<AudioManager>();
    }

    //TODO
   
    // Update is called once per frame
    void Update()
    {
        if (!CombatMode)
        {
            Movement();
            Vector3 newPosition = transform.position;
            newPosition.z = transform.position.y;
            transform.position = newPosition;

            //check Space toggle
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchtoCombatMode();
            }
        } else {
            //keeps text instance above player when moved by enemies 
            textInstance.transform.position = transform.position + new Vector3(0, 3, 0);
            //check Space toggle
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchToMovementMode();
            }
            GetTextInput();
            FindMatchingTarget();
            AimAtTarget();
        }
    }
    //TODO
    void UIUpdates()
    {
        if (health <= 0)
        {
            OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 direction = input.normalized;
        Vector2 velocity = direction * speed;
        transform.Translate(velocity * Time.deltaTime);
        if(Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetBool("isRunningDown", true);
        }else if (Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("isRunningDown", false);
        }
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            anim.SetBool("isRunningUp", true);
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("isRunningUp", false);
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            anim.SetBool("isRunningLeft", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("isRunningLeft", false);
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            anim.SetBool("isRunningRight", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("isRunningRight", false);
        }
    }

    void GetTextInput()
    {
        textbox.text += Input.inputString;
        if (Input.GetKeyDown(KeyCode.Backspace) && textbox.text.Length > 1)
        {
            String backSpaceText = textbox.text.Substring(0, textbox.text.Length - 2);
            textbox.text = backSpaceText;
        }
    }

    void SwitchtoCombatMode()
    {
        CombatMode = true;
        audioManager.Play("Combat Mode");
        anim.SetBool("isInCombatMode", true);
        //instantiate Gun
        //SR.sprite = combat;
        gunPivotInstance = (GameObject)Instantiate(gunPivotContainer);
        gunPivotInstance.transform.SetParent(transform);
        gunPivotInstance.transform.position = transform.position + new Vector3(-1f, 0.4f, 0);
        gunPivotInstance.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("player");
        textInstance = (GameObject)Instantiate(PlayertextPrefab);
        //textInstance.transform.parent = canvasObj.transform;
        textInstance.transform.SetParent(canvasObj.transform);
        textbox = textInstance.GetComponent<Text>();
        textbox.text = "";
        //places the text above the head
        textInstance.transform.position = transform.position + new Vector3(0, 3, 0);

    }

    void SwitchToMovementMode()
    {
        anim.SetBool("isInCombatMode", false);
        CombatMode = false;
        audioManager.Play("Default Mode");
        //SR.sprite = idle;
        Destroy(textInstance);
        //Destroy(gunInstance);
        Destroy(gunPivotInstance);
    }

    void FindMatchingTarget()
    {
        //check if text input matches enemies
        var TargetableObjects = FindObjectsOfType<Targetable>();
        foreach (Targetable target in TargetableObjects)
        {
            if (target.targetWord == textbox.text)
            {
                ((Enemies)target).Death();
                textbox.text = "";
                applyKnockback();
            }
            else if (textbox.text.Length <= target.targetWord.Length && textbox.text == target.targetWord.Substring(0, textbox.text.Length) && textbox.text.Length != 0)
            {
                ((Enemies)target).TargetedText(textbox.text.Length);
                currentTarget = target;
                targetedInstances.Add(target);
               
                //((Enemies)target).TargetedText(textbox.text.Length);
            }
        } 
        foreach (Targetable targeted in targetedInstances)
                {
                if(!(textbox.text.Length <= targeted.targetWord.Length && textbox.text == targeted.targetWord.Substring(0, textbox.text.Length) && textbox.text.Length != 0))
                    {
                        ((Enemies)targeted).TargetedText(0);
                        //targetedInstances.Remove(targeted);
                    }
                }
    }

    private void applyKnockback()
    {       
        RB.AddForce(-aimDirection.normalized * 60, ForceMode2D.Impulse); 
        currentTarget.head.GetComponent<Rigidbody2D>().AddForce(aimDirection.normalized * 120, ForceMode2D.Impulse);
    }

    void AimAtTarget()
    {
        //aim at target
        if (currentTarget != null)
        {
            aimDirection = new Vector3(currentTarget.targetPosition.x, currentTarget.targetPosition.y,0) -new Vector3(transform.position.x, transform.position.y,0);
            Vector3 gunAngle = gunPivotInstance.transform.eulerAngles;
            float angle = Vector3.Angle(new Vector3(1, 0, 0), aimDirection);
            //right side down
            if (currentTarget.transform.position.y < transform.position.y && currentTarget.transform.position.x > transform.position.x)
            {
                gunPivotInstance.transform.eulerAngles = new Vector3(0, 0, -angle);
                gunPivotInstance.transform.localScale = new Vector3(1, 1, 1);
            }
            //right side up
            else if (currentTarget.transform.position.y > transform.position.y && currentTarget.transform.position.x > transform.position.x)
            {
                gunPivotInstance.transform.eulerAngles = new Vector3(0, 0, angle);
                gunPivotInstance.transform.localScale = new Vector3(1, 1, 1);
            }
            //left side down
            else if (currentTarget.transform.position.y < transform.position.y && currentTarget.transform.position.x < transform.position.x)
            {
                gunPivotInstance.transform.eulerAngles = new Vector3(0, 0, 180 - angle);
                gunPivotInstance.transform.localScale = new Vector3(-1, 1, 1);
            }
            //left side up
            else if (currentTarget.transform.position.y > transform.position.y && currentTarget.transform.position.x < transform.position.x)
            {
                gunPivotInstance.transform.eulerAngles = new Vector3(0, 0, -(180 - angle));
                gunPivotInstance.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}

