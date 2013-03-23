using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	//mouse hovering over a field
	public Material hoverOverField;
	//layer witch is used
	public LayerMask setingLayer;
	//existing twrs
	public GameObject[] twr;
	private int priceOfTwr;
	//starting material of a tile
	private Material originalMaterial;
	//last hovered tile
	private GameObject lastHitObject;
	private int m = 0;
	//text witch shows what twr is selected
	public GUIText twrText;
	public MainScript mainScript;
	public void Start () {
		mainScript =  GameObject.FindWithTag("MainScript").GetComponent<MainScript>();
		twrText.text = "Air Missile Tower Tier 1";
		priceOfTwr = AirMissileTwr.price;
	}
	public void Update () {
		Ray beam = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit alight = new RaycastHit();
		if (Physics.Raycast(beam, out alight, 1000f, setingLayer)) {
			if (Input.GetKeyDown ("1")) {
				twrText.text = "Air Missile Tower Tier 1";
				m = 0;
				priceOfTwr = AirMissileTwr.price;
			} else if (Input.GetKeyDown ("2")) {
				twrText.text = "Land Laser Tower Tier 1";
				m = 1;
				priceOfTwr = ControlOfLandLaserTwr.price;
			}
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
		if (Input.GetMouseButtonDown(0) && lastHitObject && MainScript.Gold >= priceOfTwr) {
			if (lastHitObject.tag == "FreeFilde") {
				GameObject newTwr = Instantiate(twr[m], lastHitObject.transform.position, Quaternion.identity) as GameObject;
				MainScript.Gold -= priceOfTwr;
				mainScript.UpdateGui();
				Vector3 temp = newTwr.transform.localEulerAngles;
				temp.y = Random.Range(0,360);
				newTwr.transform.localEulerAngles = temp;
				lastHitObject.tag = "TakenField";
			}
		}
	}

}