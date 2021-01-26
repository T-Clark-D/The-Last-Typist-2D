using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {
    public GameObject BasicZombiePrefab;
	public GameObject FattyZombiePrefab;
	public int numOfBasicZombies = 20;
	public int numOfFattyZombies = 10;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < numOfBasicZombies; i++){ 
        GameObject zombieInstance = (GameObject)Instantiate(BasicZombiePrefab, new Vector3(Random.Range(-40,40), Random.Range(-46, -5), 0), Quaternion.identity);
	}
		for (int i = 0; i < numOfFattyZombies; i++)
		{
			GameObject zombieInstance = (GameObject)Instantiate(FattyZombiePrefab, new Vector3(Random.Range(-40, 40), Random.Range(-46, -5), 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
