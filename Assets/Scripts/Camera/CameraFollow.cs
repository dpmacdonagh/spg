using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform cameraTarget;
    public float offsetY = 20f;
    public float offSet = 13f;

    void Update() {
        // offsetZ = offsetY * Mathf.Tan(Mathf.Deg2Rad * (90f - xRotation));
        // offsetX = offsetY * Mathf.Tan(Mathf.Deg2Rad * (90f - xRotation));
        transform.position = new Vector3(cameraTarget.position.x + offSet, cameraTarget.position.y + offsetY, cameraTarget.position.z - offSet);
    }
}