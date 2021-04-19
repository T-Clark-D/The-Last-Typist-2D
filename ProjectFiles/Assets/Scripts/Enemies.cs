using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public abstract class Enemies :Targetable {
    public int health;
    public Animator anim;
    public bool isLookingRight;
    public GameObject textPrefab;
    public Text enemieTextBox;
    public int speed = 0;
    public bool isDead = false;
    public float scale = 2f;


    public void InitializeEnemy()
    {
        //Never delete dead animation even tho its useless....its not
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        canvasObj = GameObject.Find("Canvas");
        isTargetable = true;
        targetPosition = head.position + targetHeadOffset;
    }
    public void FlipDirection()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, transform.localScale.z);
            isLookingRight = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, transform.localScale.z);
            isLookingRight = true;
        }
    }
    public void flipDirectionOther()
    {
        if (player.transform.position.x > transform.position.x)
        {
            //transform.GetComponentInChildren<Transform>().localScale = new Vector3(-1 * scale, scale, transform.localScale.z);
            transform.localScale = new Vector3(-1*scale, scale, transform.localScale.z);
            isLookingRight = false;
        }
        else
        {
            //transform.GetComponentInChildren<Transform>().localScale = new Vector3(-1 * scale, scale, transform.localScale.z);
            transform.localScale = new Vector3(1 * scale, scale, transform.localScale.z);
            isLookingRight = true;
        }
    }
    public void instantiateText()
    {
        textPrefabInstance = (GameObject)Instantiate(textPrefab);
        textPrefabInstance.transform.SetParent(canvasObj.transform, false);
        enemieTextBox = textPrefabInstance.GetComponent<Text>();
        SetWord();
        enemieTextBox.text = targetWord;
        textPrefabInstance.transform.position = new Vector3(transform.position.x, transform.position.y, -80) + targetTextOffset;
    }
    //updates text location as enemie moves
    public void updateTextLocation()
    {
        textPrefabInstance.transform.position = new Vector3(transform.position.x, transform.position.y, -80) + targetTextOffset;
    }
    //updates the target position sot that player can dynamically aim at head
    public void updateTargetPosition()
    {
        targetPosition = head.position + targetHeadOffset;
    }
    public void Movement()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 unitdirection = (player.transform.position - transform.position).normalized;
        transform.Translate(unitdirection * speed * Time.deltaTime);
    }
    public void TargetedText(int textLength)
    {
        if (!isDead)
        {
            if (textLength == 0)
            {
                enemieTextBox.text = targetWord;
            }
            else if (!isDead)
            {
                enemieTextBox.text = "<color=red>" + targetWord.Substring(0, textLength) + "</color>" + targetWord.Substring(textLength, wordLength - textLength);
            }
        }
    }
    public void Death()
    {
        isDead = true;
        Destroy(anim);
        //temporarily deprecated
        //ragDollTRansform();
        //var emission = head.GetChild(3).GetComponent<ParticleSystem>().emission;
        //emission.enabled = true;
        head.GetComponentInChildren<ParticleSystem>().Play();
        head.GetComponent<SpriteRenderer>().enabled = false;
        SpriteRenderer[] HeadRenderers = head.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in HeadRenderers)
            sr.enabled = false;
        Destroy(textPrefabInstance);
        targetWord = "@@@@@@@@@@";
    }

    public void ragDollTRansform()
    {
        HingeJoint2D[] hingeJoints;
        hingeJoints = gameObject.GetComponentsInChildren<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
            joint.enabled = true;

        BoxCollider2D[] colliders;
        colliders = gameObject.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D box in colliders)
            box.enabled = true;
    }

}
