using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public bool doubleJumpActive = true;
    private bool sprintActive;
    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform playerBase;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private float baseSpeed = 9f;
    [SerializeField] private float playerSpeed = 9f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float jumpForce = 5.5f;
    private Vector3 movement;
    private float speedBonus = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(checkMovement());
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

        movement = new Vector3(moveX, 0, moveZ);
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
                if(jumpSound != null)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
                if (doubleJumpActive){
                    doubleJumpActive = false;
                }
            }
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            playerSpeed = baseSpeed * sprintMultiplier * speedBonus;
            sprintActive = true;
        } else{
            playerSpeed = baseSpeed * speedBonus;
            sprintActive = false;
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

    // bonus is a vector2 where x is the multiplicative speed bonus and y is the duration.
    public void Speedup(Vector2 bonus) {
        StartCoroutine(resetSpeedBonus(bonus));
    }

    // Check if the footsteps array has objects and if it does play one from a random range
    private void walkSounds()
    {
        int stepSoundEffect = Random.Range(0,10);
        if (footsteps != null)
        {
            audioSource.PlayOneShot(footsteps[stepSoundEffect]);
        }

    }
    
    // Check if the player is moving on the ground, then call the walkSounds method,
    // and finally recursively call itself after the alloted time.
    private IEnumerator checkMovement()
    {
        float footstepSpeed = 0.3f;
        if (sprintActive){
            footstepSpeed /= 1.3f;
        }

        if (movement != Vector3.zero && Physics.CheckSphere(playerBase.position, 0.1f, FloorMask))
        {
            walkSounds();
        }
        yield return new WaitForSeconds(footstepSpeed);
        StartCoroutine(checkMovement());
    }
    private IEnumerator resetSpeedBonus(Vector2 bonus) {
        speedBonus += bonus.x;
        yield return new WaitForSeconds(bonus.y);
        speedBonus -= bonus.x;
        Debug.Log("Speed boost deactivated.");
    }
}