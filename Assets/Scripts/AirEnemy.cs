using UnityEngine;
using System.Collections;

public class AirEnemy : MonoBehaviour {
	//explosion on destruction
	public GameObject explosion;
	//hight range
	public Vector2 hightRange = new Vector2(10f, 18f);
	//speed range
	public Vector2 airEnemySpeedRange = new Vector2(7f, 12f);
	//speed
	public float airEnemySpeed = 10f;
	//hp
	public float hp = 75f;
	//lvl script
	public MainScript mainScript;
	//position with hight
	private Vector3 temp;
	public void Start () {
		mainScript = GameObject.FindWithTag("MainScript").GetComponent<MainScript>();
		airEnemySpeed = Random.Range(airEnemySpeedRange.x, airEnemySpeedRange.y);
		temp = transform.position;
		temp.y = Random.Range(hightRange.x, hightRange.y);
		transform.position = temp;
		airEnemySpeed *= mainScript.constOfDif;
		hp *= mainScript.constOfDif;
		MainScript.Gold += 10;
		mainScript.UpdateGui();

	}
	public void Update () {
		transform.Translate(Vector3.forward * (airEnemySpeed * Time.deltaTime));

	}
	public void TakeDmg (float dmg) {
		hp -= dmg;
		if (hp <= 0) {
			audio.Play();
			mainScript.enemyCount --;
			MainScript.skor += (int) (airEnemySpeed + temp.y + hp);
			GameObject newExplosion;
			newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			Destroy(newExplosion, 0.2f);
			Destroy(gameObject);
		}
	}

}