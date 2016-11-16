using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Ball;

public class Checkpoint : MonoBehaviour {

	private GameObject player;
	private Ball playerBall;
	private BallUserControl playerControl;
	private MeshRenderer[] checkpointRenderer;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerBall = player.GetComponent<Ball> ();
		playerControl = player.GetComponent<BallUserControl> ();
		checkpointRenderer = GetComponentsInChildren<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") {
			playerControl.setCheckpoint(this.transform.position, gameObject);

			for(int i = 0; i < checkpointRenderer.Length; i ++){
				checkpointRenderer[i].material.color = Color.green;
			}
		}
	}
}
