using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {
    Player player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        player.OnPlayerDeath += GameOver;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void DrawHealthBar()
    {
        //to do Draw Healthbar
    } 
    public void GameOver()
    {
        //to do game over UI
    }
}
