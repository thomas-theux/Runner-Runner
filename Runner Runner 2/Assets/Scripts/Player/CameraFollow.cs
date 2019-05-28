using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public int cameraID = 0;
	private Transform target;

	public float smoothSpeed = 3.0f;
	public Vector3 isoOffset;


    private void Awake() {
        this.target = this.transform.parent.transform;
        this.transform.parent = null;
    }


	private void LateUpdate() {
        Vector3 desiredPos = this.target.position + this.isoOffset;
        Vector3 smoothedPos = Vector3.Lerp(this.transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        this.transform.position = smoothedPos;

	}
}
