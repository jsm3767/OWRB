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

	public Button reset;

	private List<GameObject> orbs;
	// Use this for initialization
	void Start () {
		reset.onClick.AddListener (ResetGame);

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
			default:
				orbScript.SpawnLocation = new Vector3(0,0,-8);
				break;
			}
		}
		pauseMenu.SetActive (false);
	}

	void ResetGame(){
		foreach (GameObject orbOBJ in orbs) {
			Orb orbScript = orbOBJ.GetComponent<Orb>();
			orbScript.Enable();
		}
		Transform t = player.transform;
		t.position = new Vector3 (-120, 101, 0);
		
		storedVelocity = Vector3.zero;
		storedRotationVelocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		BallUserControl ball = player.GetComponent<BallUserControl>();

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
	}
}
