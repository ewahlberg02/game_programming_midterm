using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public bool doubleJumpActive = true;
    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform playerBase;
    [SerializeField] private float baseSpeed = 9f;
    [SerializeField] private float playerSpeed = 9f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpForce = 5.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        doubleJumpCheck();
    }

    private void playerMovement(){
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ);
        Vector3 moveVector = transform.TransformDirection(movement) * playerSpeed;
        rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
        
        // If there is no WASD input stop player movement
        if (movement == Vector3.zero){
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        // Jump logic
        if(Input.GetKeyDown(KeyCode.Space)){
            if(Physics.CheckSphere(playerBase.position, 0.1f, FloorMask) || doubleJumpActive){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                if (doubleJumpActive){
                    doubleJumpActive = false;
                }
            }
        }

        // Sprint Logic
        if(Input.GetKey(KeyCode.LeftShift)){
            playerSpeed = baseSpeed * sprintMultiplier;
        } else{
            playerSpeed = baseSpeed;
        }
    }

    // If the player touches the ground double jump is reset
    void doubleJumpCheck(){
        if (Physics.CheckSphere(playerBase.position, 0.1f, FloorMask) && !doubleJumpActive){
            ResetDoubleJump();
        }
    }

    public void ResetDoubleJump(){
        doubleJumpActive = true;
    }
}