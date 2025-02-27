using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 movement;
    public bool doubleJumpActive = true;
    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform playerBase;
    [SerializeField] float playerSpeed = 9f;
    [SerializeField] float jumpForce = 5.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")).normalized;
        playerMovement(movement);
        doubleJumpCheck();
    }

    private void playerMovement(Vector3 movement){
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
    }

    // If the player touches the ground double jump is reset
    void doubleJumpCheck(){
        if (Physics.CheckSphere(playerBase.position, 0.1f, FloorMask) && !doubleJumpActive){
            doubleJumpActive = true;
        }
    }
}