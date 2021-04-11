using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public Text timer;
    public Text waveNumber;
    public int currentWave;
    public float timeLeft;
    public bool isWaveOngoing = false;

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
        InitiateNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaveOngoing)
            InitiateNextWave();
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timer.text = ((int)timeLeft).ToString();
        }
        else
            timer.text = "0"; //isWaveOngoing = false;
        if (timeLeft < 58)
            waveNumber.text = "";
    }

    void InitiateNextWave()
    {
        spawnZombie(ZombieType.Basic);
        currentWave++;
        waveNumber.text = "Wave " + currentWave.ToString();
        timeLeft = 61;
        isWaveOngoing = true;
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

}
