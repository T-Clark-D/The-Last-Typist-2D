using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    readonly float roundTime = 60;

    public Text timer;
    public Text waveNumber;
    public Text wordsPerMinute;
    public float currentWave;
    public float timeLeft;
    public bool isWaveOngoing = false;

    static int totalLetters = 0;
    public float totalTime = 0;

    public bool zombiesDone = false;

    public int baseZombies;
    public float flimsyPercent;
    public float basicPercent;
    public float fattyPercent;
    public float buffPercent;

    public GameObject BasicZombiePrefab;
    public GameObject FattyZombiePrefab;

    AudioManager audioManager;

    enum ZombieType
    {
        Flimsy,
        Basic,
        Fatty,
        Buff
    }
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currentWave = 0;
        StartCoroutine("InitiateNextWave");
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if (!isWaveOngoing)
            StartCoroutine("InitiateNextWave");

        if (timeLeft > 0)
        {
            timeLeft -= deltaTime;
            totalTime += deltaTime;
            timer.text = ((int)timeLeft).ToString();
            wordsPerMinute.text = "WPM " + ((int)Math.Round((totalLetters / 5) / (totalTime / 60))).ToString();
        }
        else if (zombiesDone) { totalTime += deltaTime; wordsPerMinute.text = "WPM " + ((int)Math.Round((totalLetters / 5) / (totalTime / 60))).ToString(); }


        if (zombiesDone == true)
            if (isRoundOver()) isWaveOngoing = false;
        
    }

    IEnumerator InitiateNextWave()
    {
        currentWave++;
        isWaveOngoing = true;
        timer.text = roundTime.ToString();
        waveNumber.text = "Wave " + currentWave.ToString();
        yield return new WaitForSeconds(3);
        waveNumber.text = "";
        timeLeft = roundTime;
        totalTime = 0;
        totalLetters = 0;
        zombieTime();
    }

    void spawnZombie(ZombieType type)
    {
        GameObject zombieInstance;
        switch (type)
        {
            case ZombieType.Basic:
                zombieInstance = (GameObject)Instantiate(BasicZombiePrefab, new Vector3(UnityEngine.Random.Range(-40, 40), Random.Range(-46, -5), 0), Quaternion.identity);
                break;
            case ZombieType.Fatty:
                zombieInstance = (GameObject)Instantiate(FattyZombiePrefab, new Vector3(UnityEngine.Random.Range(-40, 40), Random.Range(-46, -5), 0), Quaternion.identity);
                break;
        }
    }

    bool isRoundOver()
    {
        Enemies[] basics;

        basics = FindObjectsOfType<Enemies>();

        //if (basics.Length! > 0) //This caused glitches
          //  return true;
        
        foreach (Enemies i in basics)
        {
            if (i.isDead == false) return false;
        }

        zombiesDone = false;
        return true;
    }

    void zombieTime()
    {
        baseZombies = (int)(17 + 3*currentWave);

        ///Calculate the percent of each zombie and the resulting number
        flimsyPercent = (float)(0.01 * (720 / (currentWave + 8)));
        float numFlimsy = baseZombies*flimsyPercent;

        if (currentWave < 39)
            basicPercent = (float)((0.01)*(-0.03*currentWave*currentWave + 56));
        else
            basicPercent = 1209 / currentWave + 4;
        float numBasic = baseZombies * basicPercent;

        fattyPercent = 0;
        if (currentWave < 51 && currentWave > 9)
            fattyPercent = (float)((0.01) * (-Math.Pow(0.3 * currentWave - 10.4, 2) + 57));
        else if(currentWave > 50)
            fattyPercent = 925 / currentWave + 15;
        float numFatty = baseZombies * fattyPercent;

        buffPercent = 0;
        if(currentWave > 24)
            buffPercent = (float)(0.01 * ((2200 / -currentWave) + 90));
        float numBuff = baseZombies * buffPercent;

        //waveNumber.text = numFlimsy.ToString() + "\n" + numBasic.ToString() + "\n" + numFatty.ToString() + "\n" + numBuff.ToString();

        //StartCoroutine(startSpawning(ZombieType.Flimsy, numFlimsy));
        StartCoroutine(startSpawning(ZombieType.Basic, numBasic));
        StartCoroutine(startSpawning(ZombieType.Fatty, numFatty));
        //StartCoroutine(startSpawning(ZombieType.Buff, numBuff));
    }

    IEnumerator startSpawning(ZombieType type, float numZombies)
    {
        if (numZombies != 0)
            while (timeLeft > 0)
            {
                float rate = roundTime / numZombies;
                spawnZombie(type);
                yield return new WaitForSeconds(rate);
            }
        else
            yield return new WaitForSeconds(timeLeft);

        zombiesDone = true;

    }

    public static void zombieDied(int letters) => totalLetters += letters;

}
