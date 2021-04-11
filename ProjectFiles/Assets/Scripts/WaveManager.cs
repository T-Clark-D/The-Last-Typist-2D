using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    readonly float roundTime = 20;

    public Text timer;
    public Text waveNumber;
    public float currentWave;
    public float timeLeft;
    public bool isWaveOngoing = false;

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
        currentWave = 8;
        StartCoroutine("InitiateNextWave");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaveOngoing)
            StartCoroutine("InitiateNextWave");

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timer.text = ((int)timeLeft).ToString();
        }

        if(zombiesDone == true)
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
        //FlimsyZombie[] flimsies;
        BasicZombie[] basics;
        FattyZombie[] fatties;
        //BuffZombie[] buffs;

        //flimsies = FindObjectsOfType<FlimsyZombie>();
        basics = FindObjectsOfType<BasicZombie>();
        fatties = FindObjectsOfType<FattyZombie>();
        //buffs = FindObjectsOfType<BuffZombie>();


        /*foreach (FlimsyZombie i in flimsies)
        {
            if (i.isDead == false) return false;
        }*/
        foreach (BasicZombie i in basics)
        {
            if (i.isDead == false) return false;
        }
        foreach (FattyZombie i in fatties)
        {
            if (i.isDead == false) return false;
        }
        /*foreach (BuffZombie i in buffs)
        {
            if (i.isDead == false) return false;
        }*/
        zombiesDone = false;
        return true;
    }

    void zombieTime()
    {
        baseZombies = (int)(17 + 3*currentWave);
        
        ///Calculate the percent of each zombie and the resulting number
        flimsyPercent = 720 / (currentWave + 8);
        float numFlimsy = baseZombies*flimsyPercent;

        if (currentWave < 39)
            basicPercent = (float)((0.01)*(-0.09*currentWave*currentWave + 56));
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
            buffPercent = 2200/-currentWave + 90;
        float numBuff = baseZombies * buffPercent;

        //startSpawning(ZombieType.Flimsy, numFlimsy);
        StartCoroutine(startSpawning(ZombieType.Basic, numBasic));
        StartCoroutine(startSpawning(ZombieType.Fatty, numFatty));
        //startSpawning(ZombieType.Buff, numBuff);
    }

    IEnumerator startSpawning(ZombieType type, float numZombies)
    {
        if (numZombies != 0)
        {
            while (timeLeft > 0)
            {
                float rate = roundTime / numZombies;
                yield return new WaitForSeconds(rate);
                if (timeLeft > 0)
                    spawnZombie(type);
            }

            zombiesDone = true;
        }
    }

}
