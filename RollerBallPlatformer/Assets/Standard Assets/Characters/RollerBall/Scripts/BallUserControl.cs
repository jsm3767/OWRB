using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class BallUserControl : MonoBehaviour
    {
        private Ball ball; // Reference to the ball controller.

        private Vector3 move;
        // the world-relative desired move direction, calculated from the camForward and user input.

        private Transform cam; // A reference to the main camera in the scenes transform
        private Vector3 camForward; // The current forward direction of the camera
        private bool jump; // whether the jump button is currently pressed
		private bool jumpAllowed;
		private int jumpCooldown = 7;

		private Vector3 spherePos;
		private Rigidbody sphereRB;
		private Vector3 checkpoint;

		private bool pause;
		public bool Pause { get { return pause; } set { pause = value; } }

        private void Awake()
		{
			pause = false;
			spherePos = transform.position;

			checkpoint = new Vector3 (-120, 101, 0);
            // Set up the reference.
            ball = GetComponent<Ball>();
			jumpAllowed = true;

            // get the transform of the main camera
            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
            }
        }


        private void Update()
        {
			if (pause) {
				return;
			}

            // Get the axis and jump input.

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = 0;

			if(Input.GetKeyDown(KeyCode.Backspace)){
				resetToCheckpoint();
			}

			//float v = CrossPlatformInputManager.GetAxis("Vertical");
			if (jumpAllowed && jumpCooldown > 0) {
				jumpCooldown -= 1;
			}
			if (jumpAllowed && jumpCooldown == 0) {
				jump = CrossPlatformInputManager.GetButton("Jump");
			}
			if (jump) {
				jumpAllowed = false;
				jumpCooldown = 3;
			}
			// calculate move direction
            if (cam != null)
            {
                // calculate camera relative direction to move:
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                move = (v*camForward + h*cam.right).normalized;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                move = (v*Vector3.forward + h*Vector3.right).normalized;
            }

			Vector3 position = transform.position;
			position.z = 0;
			transform.position = position;
        }

		private void OnCollisionEnter(Collision c){
			jumpAllowed = true;
		}

        private void FixedUpdate()
        {
			if (pause)
				return;
            // Call the Move function of the ball controller
            ball.Move(move, jump);
            jump = false;
		}

		public void setCheckpoint(Vector3 newCheckpoint){
			checkpoint = newCheckpoint;
		}
		
		public void resetToCheckpoint(){
			spherePos = checkpoint;
			transform.position = spherePos;
			
			Rigidbody sphereRB = GetComponent<Rigidbody> ();
			sphereRB.velocity = Vector3.zero;
		}
    }
}
