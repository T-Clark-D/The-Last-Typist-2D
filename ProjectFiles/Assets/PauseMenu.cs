using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;

    public Animator transition;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
        	if(GameIsPaused) {
        		Resume();
        	} else {
        		Pause();
        	}
        }
    }

    

    public void Resume() {
		pauseMenuUI.SetActive(false);
    	GameIsPaused = false;
        Time.timeScale = 1f;
    }

	void Pause() {
    	pauseMenuUI.SetActive(true);
    	GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void LoadMenu() {
       
        Time.timeScale = 100;
        GameIsPaused = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
