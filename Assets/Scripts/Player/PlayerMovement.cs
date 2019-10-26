using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  [SerializeField] private Vector3 targetDirection;
  [SerializeField] private Vector3 currentDirection;
  [SerializeField] private Vector3 currentVector;
  [SerializeField] private Vector3 previousPosition;
  [SerializeField] private float currentSpeed;
  [SerializeField] private float targetDirectionMultiplier;
  [SerializeField] private float currentDirectionMultiplier;
  [SerializeField] private float walkMultiplier = 3f;
  [SerializeField] private float runMultiplier = 4f;
  [SerializeField] private float sprintMultiplier = 6f;
  [SerializeField] private Vector3 aimLocation;
  [SerializeField] private float directionChangeSpeed = 1.0f;
  [SerializeField] private Animator playerAnim;
  [SerializeField] private CharacterController controller;

  void Update () {
    SetAimLocation ();
    SetDirectionMultiplier();
    SetSpeed();
    SetVector();
    RotateCharacter();
    MoveCharacter ();
  }

  private void MoveCharacter () {
    targetDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")).normalized;
    targetDirection = transform.TransformDirection(targetDirection);

    currentDirection = Vector3.Lerp(currentDirection, targetDirection, directionChangeSpeed * Time.deltaTime);



    controller.Move((currentDirection * currentDirectionMultiplier) * Time.deltaTime);
  }

  void SetDirectionMultiplier() {
    if (Input.GetButton("Walk")) {
      targetDirectionMultiplier = walkMultiplier;
    } else if (Input.GetButton("Sprint")) {
      targetDirectionMultiplier = sprintMultiplier;
    } else {
      targetDirectionMultiplier = runMultiplier;
    }

    currentDirectionMultiplier = Mathf.Lerp(currentDirectionMultiplier, targetDirectionMultiplier, directionChangeSpeed * Time.deltaTime);
  }

  void SetSpeed() {
    currentSpeed = controller.velocity.magnitude;
    playerAnim.SetFloat("speed", currentSpeed);
  }

  void SetVector() {
    if (previousPosition == null) {
      previousPosition = transform.position;
    }

    currentVector = Vector3.Lerp(currentVector, transform.InverseTransformDirection((transform.position - previousPosition).normalized), directionChangeSpeed * Time.deltaTime);
    previousPosition = transform.position;

    playerAnim.SetFloat ("x", currentVector.x);
    playerAnim.SetFloat ("z", currentVector.z);
  }

  void SetAimLocation () {
    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);;
    RaycastHit hit;

    if (Physics.Raycast (ray, out hit)) {
      aimLocation = hit.point;
    }

  }

  void RotateCharacter () {
      if (aimLocation != null) {
        Quaternion rotation = Quaternion.LookRotation (aimLocation - transform.position);
        rotation.x = 0;
        rotation.z = 0;
        // transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * directionChangeSpeed);
        transform.rotation = rotation;
      }
    }
}