using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class UnlockMechanic : MonoBehaviour {

	public GameObject player;
	public Canvas UserInterface;
	public GameObject gameManagerObj;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = gameManagerObj.GetComponent<GameManager> ();
		foreach (Button b in gameManager.specialButtons) {
			b.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
