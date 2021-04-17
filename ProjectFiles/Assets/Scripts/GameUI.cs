using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    Player player;

    public Text timer;
    public Text waveNumber;
    public Text wordsPerMinute;

    public Image health1;
    public Image health2;
    public Image health3;
    public Image health4;
    public Image health5;

    public Sprite[] blood;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
        player.OnPlayerDeath += GameOver;
        player.DamageTaken += TakeDamage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void TakeDamage()
    {
        switch (player.health)
        {
            case 0:
                health5.sprite = blood[1];
                break;
            case 1:
                health4.sprite = blood[1];
                break;
            case 2:
                health3.sprite = blood[1];
                break;
            case 3:
                health2.sprite = blood[1];
                break;
            case 4:
                health1.sprite = blood[1];
                break;
        }
    } 
    public void GameOver()
    {
        waveNumber.text = "GAME OVER";
    }
}
