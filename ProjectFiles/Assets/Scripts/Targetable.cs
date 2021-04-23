using UnityEngine;
using System.Collections;

public abstract class Targetable : MonoBehaviour {
    public string targetWord;
    public bool isTargetable;
    public int wordLength;
    public GameObject wordManager;
    WordManager WMScript;
    public Vector3 targetPosition;
    public Vector3 targetHeadOffset;
    public Vector3 targetTextOffset;
    public Transform head;
    public GameObject textPrefabInstance;

    public GameObject canvasObj;
    public Player player;

    public void SetWordLength(int length)
    {
        wordLength = length;
    }
    public void SetWord()
    {
        wordManager = GameObject.Find("wm");
        WMScript = wordManager.GetComponent<WordManager>();
        targetWord = WMScript.WVomit.getRandomWord(wordLength);
        //print(targetWord);
    }
    //public  abstract void ShowText();
    public void Zlayering()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = transform.position.y;
        transform.position = newPosition;
    }

}
