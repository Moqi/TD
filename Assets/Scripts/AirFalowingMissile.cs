using UnityEngine;
using System.Collections;

public class AirFalowingMissile : MonoBehaviour {

	//explosion effect
	public GameObject explosion;
	//targeted enemy
	public Transform enemyTarget;
	//range
	public  float missileRange = 25f;
	//speed
	public float missileSpeed = 30f;
	//dmg
	public float missileDmg = 30f;
	//curent distance
	private float distanceTraveled;
	private GameObject newExplosion;
	public void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * missileSpeed);
		distanceTraveled += Time.deltaTime * missileSpeed;
		//if distance traveled is greater then the range kill object
		if (distanceTraveled >= missileRange) {
			newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			Destroy(newExplosion, 0.2f);
			Destroy(gameObject);
			//if not falow enemy
		} else if (enemyTarget) {
			transform.LookAt(enemyTarget);
		}

	}
	//do dmg and die
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "AirEnemy") {
			other.gameObject.SendMessage("TakeDmg", missileDmg, SendMessageOptions.DontRequireReceiver);
			newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			Destroy(newExplosion, 0.2f);
			Destroy(gameObject);

		}
		
	}

}