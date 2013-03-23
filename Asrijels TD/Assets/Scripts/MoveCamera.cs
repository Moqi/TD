using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public float cameraSpeed = 60f;
	public void Update () {
		float scroll = Input.GetAxis ("Mouse ScrollWheel") * cameraSpeed;
		float posX = Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
		float posZ = Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;
		transform.Translate(posX, scroll, posZ);
	}
}