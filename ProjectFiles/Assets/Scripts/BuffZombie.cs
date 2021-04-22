using UnityEngine;

public class BuffZombie : Enemies
{
    void Start()
    {
        type = WaveManager.ZombieType.Basic;
        targetHeadOffset = new Vector3(0, 1.5f, 0);
        targetTextOffset = new Vector3(0.5f, 5, 0);
        health = 1;
        speed = 1;
        head = transform.GetChild(0).GetChild(0);
        InitializeEnemy();
        FlipDirection();
        SetWordLength(5);
        instantiateText();
    }
    void Update()
    {
        Zlayering();
        if (!isDead)
        {
            updateTargetPosition();
            updateTextLocation();
            Movement();
            FlipDirection();
        }
    }

}