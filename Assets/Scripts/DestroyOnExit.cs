using UnityEngine;
using System.Collections;
//when enemy reaches its goal
public class DestroyOnExit : MonoBehaviour {
	//main script instance
	public MainScript mainScript;
	public void Start () {
		//find instance of main script in sceen
		mainScript =  GameObject.FindWithTag("MainScript").GetComponent<MainScript>();
	}
	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "LandEnemy" || other.gameObject.tag == "AirEnemy") {
			//destroy creep
			Destroy(other.gameObject);
			//upadate general data
			mainScript.enemyCount --;
			MainScript.hpPlayer --;
			MainScript.Gold -= 20;
			mainScript.UpdateGui();
			//end game
			if (MainScript.hpPlayer >= 0) {
				Application.LoadLevel(0);
			}
		}
		
	}
}