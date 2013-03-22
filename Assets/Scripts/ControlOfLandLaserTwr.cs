using UnityEngine;
using System.Collections;

public class ControlOfLandLaserTwr : MonoBehaviour {
	//ammo for a twr
	public GameObject laserMissaile;
	//speed of fire
	public float speedOfFiere = 1f;
	//rotation of twr
	public float rotationSpeed = 5f;
	//freze time after fire
	public float frezTime = 0.01f;
	//nusle position
	public Transform[] spawnLaser;
	//position of a rotating part of a twr
	public Transform twrRotation;
	//price of a twr
	public  static int price = 10;
	//efect of fireing, boom
	public GameObject efectOfFire;
	//enemy that is beeing targeted
	private GameObject enemyTarget;
	//when to fire next time
	private float timeOfANextShot;
	//when it can start rotating once more
	private float timeOfNextRotation;
	//point to witch to rotate
	private Quaternion positionOfRotation;
	//called every frame
	public void Update () {
		if (enemyTarget) {
			//turns to enemy if there is one
			if (Time.time >= timeOfNextRotation) {
				CalculateAimSpot(enemyTarget.transform.position);
				twrRotation.rotation = Quaternion.Lerp(twrRotation.rotation, positionOfRotation, Time.deltaTime * rotationSpeed);
			}
			//if its time to shot, shots
			if (Time.time >= timeOfANextShot) {
				Shot();
			}
		}
	}
	//fires lasers
	public void Shot () {
		//plays audio
		audio.Play();
		//sets time of shots and rotatio
		timeOfANextShot = Time.time + speedOfFiere;
		timeOfNextRotation = Time.time + frezTime;
		//starts and destroys efect of fire, and fires lasers
		GameObject newExplosion;
		foreach (Transform asd in spawnLaser) {
			Instantiate(laserMissaile, asd.position, asd.rotation);
			newExplosion = Instantiate(efectOfFire, asd.position, asd.rotation) as GameObject;
			Destroy(newExplosion, 0.2f);
		}
	}
	//when target is the range of twr
	public void OnTriggerStay(Collider other){
		if (!enemyTarget) {
			if (other.gameObject.tag == "LandEnemy") {
				//sets target
				timeOfANextShot = Time.time + (speedOfFiere * 0.5f);
				enemyTarget = other.gameObject;
			}
		}
	}
	//when target leaves frees the twr
	public void OnTriggerExit(Collider other){
		if (other.gameObject == enemyTarget) {
			enemyTarget = null;
		}
	}
	//finds target location
	public void CalculateAimSpot (Vector3 targetPosition) {
		Vector3 aimSpot = new Vector3 (targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, targetPosition.z - transform.position.z);
		//sets target location
		positionOfRotation = Quaternion.LookRotation (aimSpot);
	}



}