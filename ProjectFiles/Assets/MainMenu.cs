using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;




public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public Text difficulty_text;
    public Animator transition;
    public Sprite hard;
    public Sprite easy;
    public Sprite medium;
    public Button difButton;
    int difficultyInt = 1;

    
    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
        Time.timeScale = 1f;

    }

    

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

    public void changeDifficulty ()
    {


        switch (difficultyInt)
        {
            case 1:
                difficultyInt = 2;
                difButton.gameObject.GetComponent<Image>().sprite = medium;
                difButton.image.rectTransform.sizeDelta = new Vector3(720,120,1);

                break;

            case 2:
                difficultyInt = 3;
                difButton.gameObject.GetComponent<Image>().sprite = hard;
                difButton.image.rectTransform.sizeDelta = new Vector3(480, 120, 1);
                break;

            case 3:
                difficultyInt = 1;
                difButton.gameObject.GetComponent<Image>().sprite = easy;
                difButton.image.rectTransform.sizeDelta = new Vector3(480, 120, 1);
                break;

            default: print("Idiot in the case switch, change difficulty");
                break;

        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
