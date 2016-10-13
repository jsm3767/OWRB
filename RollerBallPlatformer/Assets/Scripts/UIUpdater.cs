using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {

	public GameObject ball;
	public GameObject textBox;
	private Text boxText;

	// Use this for initialization
	void Start () {
		boxText = textBox.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){
		if (ball.transform.position.x <= this.transform.position.x + 3 && this.transform.position.x >= ball.transform.position.x - 3 ) {
			if(tag == "sign1"){
				boxText.text = "That's really all there is too it!  Explore around, and you may find something interesting...";
			}
			if(tag == "tut1"){
				boxText.text = "Use the A and D keys to rotate the ball, and the Spacebar to jump.";
			}
			if(tag == "tut2"){
				boxText.text = "You dont have to hold down either of the movement keys to maintain speed.  Instead they are used for acceleration.";
			}
			if(tag == "tut3"){
				boxText.text = "You might want to go a slower when trying to jump on or over blocks.";
			}
			if(tag == "tut4"){
				boxText.text = "For the tutorial we've limited your max rotation speed, but once you're out you'll be able to spin as fast as you can.";
			}
			if(tag == "tut5"){
				boxText.text = "If You ever get stuck you can use Backspace to return to a checkpoint.";
			}
		}
	}

	void OnTriggerExit(){
		boxText.text = "";
	}
}
