using UnityEngine;
using System.Collections;

public class AirMissileTwr : MonoBehaviour {

	//exposion effect
	public GameObject fireEffect;
	//missle prefab
	public GameObject fallowingMissile;
	//speed of fire
	public float speedOfFiere = 1f;
	//rotation of twr
	public float rotationSpeed = 5f;
	//freze time after fire
	public float frezTime = 0.01f;
	//nusle position
	public Transform[] spawnMissile;
	//targeted enemy
	private Transform targetEnemy;
	//part that rotates round y axis
	public Transform rotationLeftRight;
	//part that tilts
	public Transform rotationUpDown;
	//spot that rotates first
	public Transform pointOfRotationLeftRight;
	//spot that tilts
	public Transform pointOfTilting;
	//price of twr
	public static int price  = 10;
	//pause time between shots
	private float timeOfNextShot;
	private int m = 0;
	// Update is called once per frame
	//aim and fire
	public void Update () {
		if (targetEnemy) {
			pointOfRotationLeftRight.LookAt(targetEnemy);
			pointOfRotationLeftRight.eulerAngles = new Vector3(0, pointOfRotationLeftRight.eulerAngles.y, 0);
			pointOfTilting.LookAt(targetEnemy);

			rotationLeftRight.rotation = Quaternion.Lerp(rotationLeftRight.rotation, pointOfRotationLeftRight.rotation, Time.deltaTime * rotationSpeed);
			rotationUpDown.rotation = Quaternion.Lerp(rotationLeftRight.rotation, pointOfTilting.rotation, Time.deltaTime * rotationSpeed);
			if (Time.time >= timeOfNextShot) {
				Shot();
			}
		}
	}
	//acquire target
	public void OnTriggerStay(Collider other){
		if (!targetEnemy) {
			if (other.gameObject.tag == "AirEnemy") {
				timeOfNextShot = Time.time + (speedOfFiere * 0.5f);
				targetEnemy = other.gameObject.transform;
			}
		}
		
	}
	//target leaves range
	public void OnTriggerExit(Collider other){
		if (other.gameObject.transform == targetEnemy) {
			targetEnemy = null;
		}
		
	}
	//fire missle, explosion
	public void Shot() {
		audio.Play();
		timeOfNextShot = Time.time + speedOfFiere;
		GameObject newMissile;
		GameObject newExplosion;
		newMissile = Instantiate(fallowingMissile, spawnMissile[m].position, spawnMissile[m].rotation) as GameObject;
		newExplosion = Instantiate(fireEffect, spawnMissile[m].position, spawnMissile[m].rotation) as GameObject;
		Destroy(newExplosion, 0.2f);
		m++;
		if (m == spawnMissile.Length) {
			m = 0;
		}
	}
}