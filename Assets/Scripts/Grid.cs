using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	//grid of tiles on witch twr as build
	public Transform grid;
	//mouse hovering over a field
	public Material hoverOverField;
	//layer witch is used
	public LayerMask setingLayer;
	//existing twrs
	public GameObject[] twr;
	//starting material of a tile
	private Material originalMaterial;
	//last hovered tile
	private GameObject lastHitObject;
	public int m = 0;
	//text witch shows what twr is selected
	public GUIText twrText;
	public void Start () {
		twrText.text = "Air Missile Tower Tier 1";
		MainScript.UpdateGui();
	}
	public void Update () {
		Ray beam = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit alight = new RaycastHit();
		if (Physics.Raycast(beam, out alight, 1000f, setingLayer)) {
			if (Input.GetKeyDown ("1")) {
				twrText.text = "Air Missile Tower Tier 1";
				m = 0;
			} else if (Input.GetKeyDown ("2")) {
				twrText.text = "Land Laser Tower Tier 1";
				m = 1;
			}
			MainScript.UpdateGui();
			if (lastHitObject) {
				lastHitObject.renderer.material = originalMaterial;
			}
			lastHitObject = alight.collider.gameObject;
			originalMaterial = lastHitObject.renderer.material;
			lastHitObject.renderer.material = hoverOverField;
		} else {
			if (lastHitObject) {
				lastHitObject.renderer.material = originalMaterial;
				lastHitObject = null;
			}
		}
		if (Input.GetMouseButtonDown(0) && lastHitObject && MainScript.Gold >= twr[m].price) {
			if (lastHitObject.tag == "FreeFilde") {
				GameObject newTwr = Instantiate(twr[m], lastHitObject.transform.position, Quaternion.identity) as GameObject;
				MainScript.Gold -= twr[m].price;
				MainScript.UpdateGui();
				Vector3 temp = newTwr.transform.localEulerAngles;
				temp.y = Random.Range(0,360);
				newTwr.transform.localEulerAngles = temp;
				lastHitObject.tag = "TakenField";
			}
		}
	}
}