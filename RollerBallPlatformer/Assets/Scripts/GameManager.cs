using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class GameManager : MonoBehaviour {

	public GameObject orbPrefab;
	public GameObject player;

	public GameObject pauseMenu;
	private bool pause;
	private Vector3 storedRotationVelocity;
	private Vector3 storedVelocity;
	
	public Text orbCount;
	public Text timer;

	private float timePlayed;

	public Button reset;

	public Canvas canvasInterface;
	public List<Button> specialButtons;
	public Button jumpSpecial;
	public Button phaseSpecial;
	public Button boostSpecial;
	public Button flipSpecial;
	public Button springSpecial;
	public Button rocketSpecial;
	BallUserControl ball;

	private List<GameObject> orbs;
	// Use this for initialization
	void Start () {
		timePlayed = 0;
		reset.onClick.AddListener (ResetGame);
		specialButtons = new List<Button> ();
		specialButtons.Add (jumpSpecial);
		specialButtons.Add (phaseSpecial);
		specialButtons.Add (boostSpecial);
		specialButtons.Add (flipSpecial);
		specialButtons.Add (springSpecial);
		specialButtons.Add (rocketSpecial);
		rocketSpecial.onClick.AddListener (delegate {SelectSpecial("rocket", rocketSpecial);});
		springSpecial.onClick.AddListener (delegate {SelectSpecial("spring", springSpecial);});
		flipSpecial.onClick.AddListener (delegate {SelectSpecial("flip", flipSpecial);});
		boostSpecial.onClick.AddListener (delegate {SelectSpecial("boost", boostSpecial);});
		phaseSpecial.onClick.AddListener (delegate {SelectSpecial("phase", phaseSpecial);});
		jumpSpecial.onClick.AddListener (delegate {SelectSpecial("jump", jumpSpecial);});

		pause = false;
		orbs = new List<GameObject> ();
		for (int i = 0; i < 20; i++) {
			GameObject orb = Instantiate(orbPrefab);
			orbs.Add(orb);
			Orb orbScript = orb.GetComponent<Orb>();
			switch(i){
			case 0:
				orbScript.SpawnLocation = new Vector3(0,25,0);
				break;
			case 1:
				orbScript.SpawnLocation = new Vector3(2,2,0);
				break;
			case 2:
				orbScript.SpawnLocation = new Vector3(0,28,0);
				break;
			case 3:
				orbScript.SpawnLocation = new Vector3(-57.0f,-28.5f,0.0f);
				break;
			case 4:
				orbScript.SpawnLocation = new Vector3(-267.0f,-18f,0.0f);
				break;
			case 5:
				orbScript.SpawnLocation = new Vector3(51.0f,-24.5f,0.0f);
				break;
			default:
				orbScript.SpawnLocation = new Vector3(0,0,-8);
				break;
			}
		}
		ball = player.GetComponent<BallUserControl>();
		pauseMenu.SetActive (false);
		SelectSpecial ("jump", jumpSpecial);
	}

	void ResetGame(){
		timePlayed = 0;
		foreach (GameObject orbOBJ in orbs) {
			Orb orbScript = orbOBJ.GetComponent<Orb>();
			orbScript.Enable();
		}
		Transform t = player.transform;
		t.position = new Vector3 (-120, 101, 0);
		
		storedVelocity = Vector3.zero;
		storedRotationVelocity = Vector3.zero;
		SelectSpecial ("jump", jumpSpecial);
	}

	void SelectSpecial(string name, Button selected){
		ball.Special = name;
		Image buttonImage;

		for (int i = 0; i < specialButtons.Count; i++) {
			buttonImage = specialButtons[i].GetComponent<Image>();
			buttonImage.color = Color.white;
		}

		buttonImage = selected.GetComponent<Image> ();
		buttonImage.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		if(!pause)
			timePlayed += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Escape)) {
			pauseMenu.SetActive(!pauseMenu.activeSelf);

			ball.Pause = !ball.Pause;
			Rigidbody ballRB = player.GetComponent<Rigidbody>();
			if(pause == false){
				storedVelocity = ballRB.velocity;
				storedRotationVelocity = ballRB.angularVelocity;
				ballRB.constraints = RigidbodyConstraints.FreezeAll;
			}else{
				ballRB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | 
					RigidbodyConstraints.FreezePositionZ;
				ballRB.angularVelocity = storedRotationVelocity;
				ballRB.velocity = storedVelocity;
			}
			pause = !pause;
		}

		if(Input.GetKeyDown(KeyCode.Backspace)){
			ball.resetToCheckpoint();
		}

		int numOrbsCollected = 0;
		foreach (GameObject orb in orbs) {
			Orb orbScript = orb.GetComponent<Orb>();
			if(orbScript.Collected)
				numOrbsCollected++;
		}
		orbCount.text = numOrbsCollected + " / 20";
		timer.text = "Time: " + timePlayed;
	}
}
