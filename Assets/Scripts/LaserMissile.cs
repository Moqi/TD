using UnityEngine;
using System.Collections;

public class LaserMissile : MonoBehaviour {
	//speed of a misle
	public float laserSpeed = 120f;
	//range of a missle
	public float laserRange = 30f;
	//dmg of a missle
	public float laserDmg = 20f;
	//traveled distance
	private float distanceTraveled;
	//in every frame
	public void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * laserSpeed);
		distanceTraveled += Time.deltaTime * laserSpeed;
		//when out of range
		if (distanceTraveled >= laserRange) {
			Destroy(gameObject);
		}
	}
	//when hits enemy
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "LandEnemy") {
			other.gameObject.SendMessage("TakeDmg", laserDmg, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
		
	}
}