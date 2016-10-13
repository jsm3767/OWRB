using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Ball;

public class Teleport : MonoBehaviour {

	public GameObject sphere;
	private Transform sphereTransform;
	private Vector3 spherePos;
	private Rigidbody sphereRB;
	private Ball ball;

	// Use this for initialization
	void Start () {
		sphereTransform = sphere.GetComponent<Transform> ();
		spherePos = sphereTransform.position;
		ball = sphere.GetComponent<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		spherePos = new Vector3 (0, 1, 0);
		sphereTransform.position = spherePos;

		Rigidbody sphereRB = sphere.GetComponent<Rigidbody> ();
		sphereRB.velocity = Vector3.zero;
		sphereRB.maxAngularVelocity = 500;

		ball.setJumpPower (10);
	}
}
