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
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void FuckU()
    {
        print("FUCK U");
    }
    public void changeDifficulty ()
    {

        string text = difficulty_text.text;

        switch (text)
        {
            case "Easy":
                difficulty_text.text = "Medium";
                break;

            case "Medium":
                difficulty_text.text = "Hard";
                break;

            case "Hard":
                difficulty_text.text = "Easy";
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
