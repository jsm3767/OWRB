using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public GameObject sphere;
	public float cameraFollowThreshold = 1;
	private Transform sphereTransform;
	private Vector3 spherePos;

	// Use this for initialization
	void Start () {
		sphereTransform = sphere.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		spherePos = sphereTransform.position;
		if (transform.position.x - spherePos.x >= cameraFollowThreshold) {
			spherePos.x += cameraFollowThreshold;
		} else if (transform.position.x - spherePos.x <= -cameraFollowThreshold) {
			spherePos.x -= cameraFollowThreshold;
		} else {
			spherePos.x = transform.position.x;
		}
		spherePos.z -= 3;
		transform.position = spherePos;
	}
}
