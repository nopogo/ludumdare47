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
    public float jetPackForceMultiplier = 1f;
    public float dragMultiplier = 5f;
    public float torque = 1f;

    float angularDragStart;
    float dragStart;
    // float degreeClampY = 70f;
    // float degreeClampX = 45f;
    public float turnLookSpeed = 6f;

    float rollDirection = 0f;
    

    Rigidbody playerRigidbody;

    Camera mainCamera;
    CharacterController characterController;

    bool playJetPackNoise = false;

    void Awake(){
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Confined;
        playerRigidbody  = GetComponent<Rigidbody>();
        angularDragStart = playerRigidbody.angularDrag;
        dragStart        = playerRigidbody.drag;
    }

    public void EnableGravity(bool enable){
        if(enable){
            GetComponent<BoxCollider>().enabled = false;
            characterController.enabled = true;
            playerRigidbody.constraints  = RigidbodyConstraints.FreezeRotationX;
            playerRigidbody.constraints  = RigidbodyConstraints.FreezeRotationZ;
            playerRigidbody.transform.rotation = Quaternion.Euler(Vector3.zero);
        }else{
            GetComponent<BoxCollider>().enabled = true;
            characterController.enabled = false;
            playerRigidbody.constraints  = RigidbodyConstraints.None;
        }
    }

    void Update(){
        if(playerRigidbody.useGravity == true){
           FirstPersonCameraMovement();
           MovementControlGravity();
        }    
    }


    void FixedUpdate(){
        if(Time.timeScale != 1){
            return;
        }
        if(playerRigidbody.useGravity == false){
            PlayerMovementInput();
            ApplyPlayerForce();
            PlayerLookInput();
        }      
    }

    void PlayerMovementInput(){
        playJetPackNoise = false;
        if(Input.GetKey(KeyCode.LeftShift)){
            killMomentum = true;
            assending    = false;
            decending    = false;
            playJetPackNoise = true;
            return;
        }else{
            killMomentum = false;
        }
        //vertical is forward backward
        //horizontal is rotational "looking" left right
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        if(horizontalAxis!= 0f || verticalAxis != 0f){
            playJetPackNoise = true;
        }

        if(Input.GetKey(KeyCode.Space)){
            assending = true;
            playJetPackNoise = true;
        }else{
            assending = false;
        }

        if(Input.GetKey(KeyCode.LeftControl)){
            decending = true;
            playJetPackNoise = true;
        }else{
            decending = false;
        }

        rollDirection = 0f;

        if(Input.GetKey(KeyCode.Q)){
            playJetPackNoise = true;
            rollDirection = 1f;
        }
        if(Input.GetKey(KeyCode.E)){
            playJetPackNoise = true;
            rollDirection = -1f;
        }
        if(GlobalFunctions.instance.gravity){
            playJetPackNoise = false;
        }
        AudioManager.instance.PlayJetPack(playJetPackNoise);
    }

    float _currentAngleY;
    float _currentAngleX;

    void PlayerLookInput(){
        if(killMomentum){
            return;
        }
        float motionVertical = Input.GetAxis("Mouse Y");
        float motionHorizontal = Input.GetAxis("Mouse X");
        motionVertical = motionVertical * turnLookSpeed;
        motionHorizontal = motionHorizontal * turnLookSpeed;
        // _currentAngleY += motionVertical;
        // _currentAngleX += motionHorizontal;

        Vector3 lookDirection = new Vector3(-motionVertical, motionHorizontal, 0f);
        Vector3 localLookDirection = transform.TransformDirection(lookDirection);
        // Quaternion deltaRotation = Quaternion.Euler(localLookDirection  * Time.deltaTime);

        // Debug.Log(lookDirection);
        playerRigidbody.AddTorque(localLookDirection, ForceMode.Impulse);
        // playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);

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
            verticalDirection   = 1f;
        }
        if(decending){
            verticalDirection   = -1f;
        }
        playerRigidbody.AddForce(transform.up * verticalDirection * jetPackForceMultiplier, ForceMode.Impulse);
        //Forward backward
        playerRigidbody.AddForce(transform.forward *verticalAxis * jetPackForceMultiplier, ForceMode.Impulse);
        //Move left right
        playerRigidbody.AddForce(transform.right * horizontalAxis * jetPackForceMultiplier, ForceMode.Impulse);
        //Roll
        playerRigidbody.AddTorque(transform.forward * rollDirection * torque, ForceMode.Impulse);
    }

    float turnSpeed = 10f;

    void FirstPersonCameraMovement(){
        _currentAngleX +=  Input.GetAxis("Mouse X") * turnSpeed;
        _currentAngleY +=  Input.GetAxis("Mouse Y") * turnSpeed;

        Quaternion look = Quaternion.Euler(-_currentAngleY, _currentAngleX, 0f);
        transform.localRotation = look;
    }

    Vector3 moveInput;
    Vector3 moveVector;
    

    float playerSpeed = 6f;
    float jumpStrength = 10f;
    float verticalSpeed = 0f;
    float gravityModifier = 25f;


    void MovementControlGravity() {

        moveInput  = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVector = transform.TransformDirection(moveInput) * playerSpeed ;

        if(Input.GetKeyDown(KeyCode.Space)) {
            verticalSpeed = jumpStrength;
        }

        else if (characterController.isGrounded){
            verticalSpeed = -1f;
        }
        
        verticalSpeed -= gravityModifier * Time.deltaTime;

        moveVector.y = verticalSpeed;
        moveVector *= Time.deltaTime;
        characterController.Move(moveVector);
    

        // // if(CameraManager.instance.currentCameraMode == CameraMode.FirstPerson){
        // rotX += Input.GetAxis(GlobalStrings.mouseX) * playerSpeed * currentSpeedModifier;
        // rotationInput = new Vector3(0, rotX, 0f);
        // playerController.PlayerInput.look = rotationInput;
        // }else{
        //     playerController.PlayerInput.look = new Vector3(0f, CameraManager.instance.thirdPersonCamera.rotation.eulerAngles.y, 0f);
        // }
        
    }
}
