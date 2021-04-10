using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {
    public GameObject BasicZombiePrefab;
	public GameObject FattyZombiePrefab;
	public int numOfBasicZombies = 1;
	public int numOfFattyZombies = 1;

	AudioManager audioManager;

	enum ZombieType
    {
		Basic,
		Fatty
    }

	// Use this for initialization
	void Start () {
		audioManager = FindObjectOfType<AudioManager>();

        for(int i = 0; i < numOfBasicZombies; i++){
			spawnZombie(ZombieType.Basic);
			audioManager.Play("Zombie Spawn");
	    }
		for (int i = 0; i < numOfFattyZombies; i++)
		{
			spawnZombie(ZombieType.Fatty);
			audioManager.Play("Zombie Spawn");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnZombie(ZombieType type)
    {
		GameObject zombieInstance;
		switch (type)
        {
			case ZombieType.Basic:
				zombieInstance = (GameObject)Instantiate(BasicZombiePrefab, new Vector3(Random.Range(-40, 40), Random.Range(-46, -5), 0), Quaternion.identity);
				break;
			case ZombieType.Fatty:
				zombieInstance = (GameObject)Instantiate(FattyZombiePrefab, new Vector3(Random.Range(-40, 40), Random.Range(-46, -5), 0), Quaternion.identity);
				break;
		}
    }
}
