using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    
    //BUTTONS
    float horizontalAxis = 0f;
    float verticalAxis = 0f;
    bool assending = false;
    bool decending = false;
    bool killMomentum = false;


    //Constants
    const float jetPackForceMultiplier = 1f;
    const float dragMultiplier = 5f;
    const float torque = 1f;

    float angularDragStart;
    float dragStart;
    float degreeClampY = 70f;
    float degreeClampX = 45f;
    float turnLookSpeed = 6f;
    

    Rigidbody playerRigidbody;

    Camera mainCamera;


    void Awake(){
        Cursor.visible = false;
        mainCamera = Camera.main;
        playerRigidbody  = GetComponent<Rigidbody>();
        angularDragStart = playerRigidbody.angularDrag;
        dragStart        = playerRigidbody.drag;
    }

    void Update(){
        if(Time.timeScale != 1){
            return;
        }
        PlayerMovementInput();
        PlayerLookInput();
    }

    void FixedUpdate(){
        ApplyPlayerForce();
    }

    void PlayerMovementInput(){
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            killMomentum = true;
            assending    = false;
            decending    = false;
            return;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            killMomentum = false;
        }
        //vertical is forward backward
        //horizontal is rotational "looking" left right
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space)){
            assending = true;
            decending = false;
            return;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            assending = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            assending = false;
            decending = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            decending = false;
        }
    }

    float _currentAngleY;
    float _currentAngleX;

    void PlayerLookInput(){
        float motionVertical = Input.GetAxis("Mouse Y");
        float motionHorizontal = Input.GetAxis("Mouse X");
        motionVertical = motionVertical * turnLookSpeed;
        motionHorizontal = motionHorizontal * turnLookSpeed;
        _currentAngleY += motionVertical;
        _currentAngleX += motionHorizontal;

        _currentAngleY = Mathf.Clamp(_currentAngleY, -degreeClampY, degreeClampY);
        _currentAngleX = Mathf.Clamp(_currentAngleX, -degreeClampX, degreeClampX);
        Quaternion look = Quaternion.Euler(-_currentAngleY, _currentAngleX, 0f);//mainCamera.transform.localRotation.eulerAngles.y, );
        mainCamera.transform.localRotation = look;
    }

    void ApplyPlayerForce(){

        //Kill momentum
        if(killMomentum){
            playerRigidbody.drag = dragMultiplier;
            playerRigidbody.angularDrag = dragMultiplier;
            return;
        }else{
            playerRigidbody.drag = dragStart;
            playerRigidbody.angularDrag = angularDragStart;
        }
        //Up down
        float verticalDirection = 0f;
        if(assending){
            verticalDirection = 1f;
        }
        if(decending){
            verticalDirection = -1f;
        }
        playerRigidbody.AddForce(transform.up * verticalDirection * jetPackForceMultiplier, ForceMode.Impulse);
        //Forward backward
        playerRigidbody.AddForce(transform.forward *verticalAxis * jetPackForceMultiplier, ForceMode.Impulse);
        //Rotate left right
        playerRigidbody.AddTorque(transform.up * horizontalAxis * torque, ForceMode.Impulse);
    }
}
