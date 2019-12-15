using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVResetDoubleJump : MonoBehaviour {

    public void ResetDoubleJump() {
        GameObject charModelGO = transform.parent.gameObject;
        GameObject characterGO = charModelGO.transform.parent.gameObject;

        print(characterGO.name);
    }

}
