using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterMovement : MonoBehaviour {

    public int charID = 0;
    private CharacterController cc;

    public float moveSpeed = 10.0f;

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

        // Normalize diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1);

        // Move character
        cc.Move(movement * moveSpeed * Time.deltaTime);

        // Rotate character in the direction of movement
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }
    }

}
