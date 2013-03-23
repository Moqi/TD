using UnityEngine;
using System.Collections;

public class LandEnemy : MonoBehaviour {

	//explosion on destruction
	public GameObject explosion;
	//speed
	public float landEnemySpeed = 8f;
	//hp
	public float landEnemyHp = 100f;
	//range of speed
	public Vector2 speedRange = new Vector2(5f, 10f);
	//instance of main script, lvl control
	public MainScript mainScript;
	private float landEnemyMaxHp;
	private bool t = false;
	public void Start () {
		t = true;
		//set speed and hp, get gold
		mainScript = GameObject.FindWithTag("MainScript").GetComponent<MainScript>();
		landEnemySpeed = Random.Range (speedRange.x, speedRange.y);
		landEnemySpeed *= mainScript.constOfDif;
		landEnemyHp *= mainScript.constOfDif;
		landEnemyMaxHp = landEnemyHp;
		MainScript.Gold += 10;
		mainScript.UpdateGui();
	}
	//move
	public void Update () {
		transform.Translate(Vector3.forward * (landEnemySpeed * Time.deltaTime));

	}
	//let it take dmg and be killed
	public void TakeDmg (float dmg) {
		landEnemyHp -= dmg;
		if (landEnemyHp <= 0 && t) {
			t = false;
			audio.Play();
			mainScript.enemyCount --;
			MainScript.skor += (int) (landEnemyMaxHp + landEnemySpeed);
			GameObject newExplosion;
			mainScript.UpdateGui();
			newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
			Destroy(newExplosion, 0.2f);
			Destroy(gameObject);
		}
	}

}