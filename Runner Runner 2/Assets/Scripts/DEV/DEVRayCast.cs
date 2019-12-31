using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVRayCast : MonoBehaviour {

    public GameObject target;
    // private int layerMask = 1 << 9;
    public LayerMask layerMask;

    public Material OpaqueWall;
    public Material TransparentWall;

    private GameObject lastCollision = null;
    // private List<GameObject> lastCollisionsArr = new List<GameObject>();

    private bool setMaterial = false;


    private void Awake() {
        // Collides with all objects except the characters
        // layerMask = ~layerMask;

        // Only collides with objects that have the "Obstacles" tag
        // layerMask = 10;
    }

    void Update() {
        Vector3 raycastDir = target.transform.position - transform.position;
        float maxDistance = Vector3.Distance(transform.position, target.transform.position);

        RaycastHit hit;

        bool hitWall = Physics.Raycast(transform.position, raycastDir, out hit, maxDistance, layerMask);
        // bool hitWall = Physics.RaycastAll(transform.position, raycastDir, maxDistance, layerMask);
        Debug.DrawRay(transform.position, raycastDir, Color.yellow);

        if (hitWall) {
            this.lastCollision = hit.collider.gameObject;

            if (!setMaterial) {
                setMaterial = true;
                OpaqueWall = this.lastCollision.GetComponent<Renderer>().material;
            }

            this.lastCollision.GetComponent<Renderer>().material = TransparentWall;

            /////////////////////////////////////////////////////////////////////////////////////////////

            // if (!lastCollisionsArr.Contains(hit.collider.gameObject)) {
            //     this.lastCollisionsArr.Add(hit.collider.gameObject);
            //     int lastAdded = lastCollisionsArr.Count - 1;

            //     OpaqueWall = this.lastCollisionsArr[lastAdded].GetComponent<Renderer>().material;

            //     this.lastCollisionsArr[lastAdded].GetComponent<Renderer>().material = TransparentWall;
            // }

            // this.lastCollision = hit.collider.gameObject;

        } else {
            if (lastCollision != null) {
                setMaterial = false;
                this.lastCollision.GetComponent<Renderer>().material = OpaqueWall;
                this.lastCollision = null;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////

            // if (lastCollisionsArr.Contains(this.lastCollision)) {
            //     int getIndex = lastCollisionsArr.IndexOf(this.lastCollision);
            //     this.lastCollisionsArr[getIndex].GetComponent<Renderer>().material = OpaqueWall;
            //     this.lastCollisionsArr.RemoveAt(getIndex);
            // }
        }
    }
}













// public GameObject target;
//     // private int layerMask = 1 << 9;
//     public LayerMask layerMask;


//     // private void Awake() {
//     //     // Collides with all objects except the characters
//     //     layerMask = ~layerMask;
//     // }


//     private void Update() {
//         Vector3 raycastDir = target.transform.position - transform.position;
//         float maxDistance = Vector3.Distance(transform.position, target.transform.position);

//         RaycastHit hit;

//         bool hitWall = Physics.Raycast(transform.position, raycastDir, out hit, maxDistance, layerMask);

//         // Check if the raycast intersects with the sphere
//         if (hitWall) {
//             target.transform.localScale = new Vector3(5, 5, 5);
//         } else {
//             target.transform.localScale = new Vector3(0, 0, 0);
//         }
//     }