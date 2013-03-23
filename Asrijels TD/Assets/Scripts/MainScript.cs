using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	//player dmg, static so other scripts can use it and change it all objects
	static public int dmgPlayer = 0;
	//is wave on or off, is enemy spawn
	public bool activeWave = false;
	public bool spawnEnemy =  false;
	//total hit points of a player, and his total score
	public static int hpPlayer = 10;
	public static int skor = 0;
	//level/wave that player is on
	public static int lvl = 0;
	//increse of dificulty
	public float constOfDif = 1f;
	//period of that that creeps are spawn in, the Length of a wave/lvl
	public float wavePeriod = 30f;
	//pause time, time between lvls
	public float pauseTime = 10f;
	//time when a wave ends
	private float endWaveTime = 0f;
	//GUI text
	public static GUIText guiGold;
	public static GUIText guiPlayerHP;
	public static GUIText guiScore;
	public static GUIText guiLVL;
	//gold for buying twrs
	public static int Gold = 100;
	//array of enemys (type of enemys)
	public GameObject[] enemy;
	//land enemy spawn points
	public Transform land;
	//air enemy spawn points;
	public Transform air;
	//array of land and air spawn points
	private Transform[] spawnLand;
	private Transform[] spawnAir;
	//enemy spawn period
	public float respawnMinBaseTime = 0.5f;
	public float respawnMaxBaseTime = 5f;
	private float respawnMin = 0.5f;
	private float respawnMax = 5f;
	public float respawnInterval = 3f;
	//enemy count, counts live enemy, wave can't start if there is more then 0 enemys
	public int enemyCount = 0;
	//abs time of a last spawn
	private float timeOfALastSpawn = 0f;
	//counters
	private int n;
	//first function that is called
	public void Awake () {
		Gold = 100;
		lvl = 0;
		skor = 0;
		hpPlayer = 10;
		//set all the gui parts
		guiGold = GameObject.Find("Gold").guiText;
		guiPlayerHP = GameObject.Find("HP").guiText;
		guiScore = GameObject.Find("Scor").guiText;
		guiLVL = GameObject.Find("Lvl").guiText;
	}
	//secend funtion that is called
	public void Start () {
		int i = 0;
		//same nubmer of land and air spawn points
		n = air.childCount;
		respawnMax = respawnMaxBaseTime;
		spawnAir = new Transform[n];
		spawnLand = new Transform[n];
		//set all the spawn points
		foreach (Transform ai in air) {
			spawnAir[i] =  ai;
			i++;
		}
		i = 0;
		foreach (Transform la in land) {
			spawnLand[i] = la;
			i++;
		}
		UpdateGui();
		SetNextWave();
		StartNewWave();
	}
	//called every frame
	public void Update () {
		if (activeWave) {
			if (Time.time >= endWaveTime) {
				spawnEnemy = false;
				if (enemyCount <= 0) {
					enemyCount = 0;
					//if end waves in time and there are no creeps wave ends
					StartCoroutine("EndWave");
				}
			}
			if (spawnEnemy) {
				if (Time.time > (timeOfALastSpawn + respawnInterval)) {
					SpawnEnemy();
				}
			}
		}
	}
	//updates gui
	public void UpdateGui () {
		guiGold.text = "Gold: " + Gold;
		guiPlayerHP.text = "HP: " + hpPlayer;
		guiScore.text = "Score: " + skor;
		guiLVL.text = "Lvl: " + lvl;
	}
	//sets next wave, its lvl and its deficulty
	public void SetNextWave () {
		//sets wave lvl
		lvl++;
		//sets dificulty ratio
		constOfDif = ((Mathf.Pow(lvl, 2)) * 0.005f) + 1;
		//sets respawn min and max time
		respawnMin = respawnMinBaseTime * (1/constOfDif);
		respawnMax = respawnMaxBaseTime / constOfDif;
		if ((respawnMin - respawnMax) < 0.1f) {
			respawnMin =  respawnMax - 0.1f;
		}
	}
	//starts set wave
	public void StartNewWave () {
		//starts spawning enemys
		SpawnEnemy();
		//sets time to end wave
		endWaveTime = Time.time + wavePeriod;
		//sets wave to active
		activeWave = true;
		//sets spawn enemys to true
		spawnEnemy = true;
	}
	//spawns enemy in a wave, random spawn point random enemy
	public void SpawnEnemy () {
		//selects a type of enemy
		int enmy =  Random.Range(0, enemy.Length);
		//selects a spawn point
		int s = Random.Range(0, n);
		//spawns enemy
		switch (enmy) {
			case 0:
				Instantiate (enemy[0], spawnAir[s].position, spawnAir[s].rotation);
				spawnAir[s].audio.Play();
				break;
			case 1:
				Instantiate(enemy[1], spawnLand[s].position, spawnLand[s].rotation);
				spawnLand[s].audio.Play();
				break;
			default:
				break;
		}
		//incrises number of enemy
		enemyCount++;
		//gets time when enemy is spawn
		timeOfALastSpawn =  Time.time;
		//sets respawn interval from given range
		respawnInterval = (float) Random.Range(respawnMin,respawnMax);
	}
	//ends wave and starts pause time
	public IEnumerator EndWave () {
		activeWave = false;
		yield return new WaitForSeconds(pauseTime);
		SetNextWave();
		StartNewWave();
	} 



}