using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {
	//target of twrs
	public Transform targ;
	//light that moves
	public GameObject ligh;
	//is quit button selected
	public bool quit =  false;
	//some variblae to check sound and be saved if sound changed in past
	public static bool la = true;
	public void Update () {
		targ.transform.Translate(Vector3.forward * (1 * Time.deltaTime));
		if (Time.time >= 20 && la) {
			la = false;
			ligh.audio.Play();
		}
	}
	//when mouse enter object
	public void OnMouseEnter () {
		gameObject.renderer.material.color = Color.green;
	}
	//when it leaves object
	public void OnMouseExit () {
		gameObject.renderer.material.color = Color.white;

	}
	//when clicked and lifted mouse button
	public void OnMouseUp () {
		if (quit) {
			Application.Quit();
		} else {
			Application.LoadLevel(1);
		}
	}
}