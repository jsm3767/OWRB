using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Orb : MonoBehaviour {

	private bool collected;
	private Vector3 spawnLocation;
	
	public bool Collected { get { return collected; } }
	public Vector3 SpawnLocation 
	{ 
		set { 
			spawnLocation = value;
			Transform t = GetComponent<Transform>();
			t.position = value;
		} 
	}

	// Use this for initialization
	void Start () {
		collected = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c){
		Disable ();
	}

	public void Disable(){
		collected = true;
		Transform t = GetComponent<Transform> ();
		t.position = new Vector3 (0, 0, -8);
	}

	public void Enable(){
		collected = false;
		Transform t = GetComponent<Transform> ();
		t.position = spawnLocation;
	}
}
