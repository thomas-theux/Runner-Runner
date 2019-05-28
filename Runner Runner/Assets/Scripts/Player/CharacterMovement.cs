using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour {

    public int charID = 0;
    private CharacterController cc;

    public float moveSpeed = 10.0f;
    public float jumpForce = 2.0f;
    public float gravityScale = 5.0f;

    // REWIRED
	private float moveHorizontal;
	private float moveVertical;
    private bool jumptBtn;


    private void Awake() {
        cc = this.GetComponent<CharacterController>();
    }


    private void Update() {
        this.GetInput();
        this.MoveCharacter();
    }


    private void GetInput() {
        moveHorizontal = ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal");
        moveVertical = ReInput.players.GetPlayer(charID).GetAxis("LS Vertical");

        jumptBtn = ReInput.players.GetPlayer(charID).GetButtonDown("Bottom Btn");
    }


    private void MoveCharacter() {
        // Get movement input
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        movement *= moveSpeed;

        // Normalize diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1);

        // Move character
        cc.Move(movement * Time.deltaTime);

        // Rotate character in the direction of movement
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }
    }

}



// if (cc.isGrounded)
// moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
// moveDirection *= speed;
// {
// // We are grounded, so recalculate
// // move direction directly from axes


// if (Input.GetButton("Jump"))
// {
// moveDirection.y = jumpSpeed;
// }
// }

// // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
// // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
// // as an acceleration (ms^-2)
// moveDirection.y -= gravity * Time.deltaTime;

// // Move the controller
// cc.Move(moveDirection * Time.deltaTime);
// }
