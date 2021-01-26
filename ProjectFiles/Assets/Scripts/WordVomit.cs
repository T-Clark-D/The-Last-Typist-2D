using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class WordVomit{
    List<string> oneLetterWords;
    List<string> twoLetterWords;
    List<string> threeLetterWords;
    List<string> fourLetterWords;
    List<string> fiveLetterWords;
    string readFromFilePath;

    // Use this for initialization
    public WordVomit() {
        
        readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthOneEnglish.txt";
        oneLetterWords = File.ReadAllLines(readFromFilePath).ToList();
        readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthTwoEnglish.txt";
        twoLetterWords = File.ReadAllLines(readFromFilePath).ToList();
        readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthThreeEnglish.txt";
        threeLetterWords = File.ReadAllLines(readFromFilePath).ToList();
        readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthFourEnglish.txt";
        fourLetterWords = File.ReadAllLines(readFromFilePath).ToList();
        readFromFilePath = Application.streamingAssetsPath + "/TextFiles/lengthFiveEnglish.txt";
        fiveLetterWords = File.ReadAllLines(readFromFilePath).ToList();

    }
	
	// Update is called once per frame
	void Update () {
	}
    public string getRandomWord(int length)
    {

    switch (length)
        {
            case 0:
                return "";
            case 1:
                return oneLetterWords[UnityEngine.Random.Range(1, oneLetterWords.Count)];
            case 2:
                return twoLetterWords[UnityEngine.Random.Range(1, twoLetterWords.Count)];
            case 3:
                return threeLetterWords[UnityEngine.Random.Range(1, threeLetterWords.Count)];
            case 4:
                return fourLetterWords[UnityEngine.Random.Range(1, fourLetterWords.Count)];
            case 5:
                return fiveLetterWords[UnityEngine.Random.Range(1, fiveLetterWords.Count)];
            default:
                return "the";
        }
    }
}
