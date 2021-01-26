using UnityEngine;

public class FattyZombie : Enemies{
	void Start () {
		targetHeadOffset = new Vector3(0, 1.5f, 0);
		targetTextOffset = new Vector3(2, 6, 0);
		health = 1;
		speed = 1;
		head = transform.GetChild(0).GetChild(0).GetChild(0);
		InitializeEnemy();
		flipDirectionOther();
		SetWordLength(0);
		instantiateText();
	}
	void Update () {
		Zlayering();
		if (!isDead)
		{
			updateTargetPosition();
			updateTextLocation();
			Movement();
			flipDirectionOther();
		}
	}
}
