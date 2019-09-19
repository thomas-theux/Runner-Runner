using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour {

    public void PlayStepSound() {
        FindObjectOfType<AudioManager>().PlayRandom("BasicStep", 0.9f, 1.1f);
    }

}
