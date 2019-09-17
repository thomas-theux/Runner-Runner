using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVRayCast : MonoBehaviour {

    public GameObject target;
    private int layerMask = 1 << 9;

    public Material OpaqueWall;
    public Material TransparentWall;


    private void Awake() {
        // Collides with all objects except the characters
        layerMask = ~layerMask;

        // Only collides with objects that have the "Obstacles" tag
        // layerMask = 10;
    }

    void Update() {
        Vector3 raycastDir = target.transform.position - transform.position;
        float maxDistance = Vector3.Distance(transform.position, target.transform.position);

        RaycastHit hit;

        bool hitWall = Physics.Raycast(transform.position, raycastDir, out hit, maxDistance, layerMask);
        Debug.DrawRay(transform.position, raycastDir, Color.yellow);

        if (hitWall) {
            hit.collider.gameObject.GetComponent<Renderer>().material = TransparentWall;
        } else {
            hit.collider.gameObject.GetComponent<Renderer>().material = OpaqueWall;
        }
    }

}
