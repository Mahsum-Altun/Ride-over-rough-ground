using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
  private bool moveLeft, moveRight, moveForward, moveBackward;
  //Create the rigidbody and the collider
  public Rigidbody rb;
  private Collider col;
  //Create a variable you can change in inspector windown for speed
  [SerializeField]
  private float speed;
  [SerializeField]
  private float smoothRotation;
  public Rigidbody rbGo;
  //Create a variable for the PlayerCollider class
  PlayerCollider m_playerCollider;
  //Created to tune the sound of an object in the air and on the ground
  public AudioSource audioSource1, audioSource2, audioSource3;
  
  void Start ()
  {
    //Get the PlayerCollider class
    m_playerCollider = GetComponent<PlayerCollider>();
    //Get the collider
    col = GetComponent<Collider>();
  }

  void Update ()
  {  
    //Reset speed when object turns upside down
    float dot = Vector3.Dot(transform.up,Vector3.up);
    if (dot <= -0.5f)
    {
        speed = 0;
    }
     //Is OnCollisionStay on the ground?
     if (m_playerCollider.IsOnGround)
     {
         //Moves the object forward
        if (moveForward)
        {
          rb.AddRelativeForce(Vector3.forward * speed); 
        } 
        //Moves the object backwards
        if (moveBackward)
        {
          rb.AddRelativeForce(Vector3.back * speed);
        }
        //Turns up sound if OnCollisionStay is on the ground
        audioSource1.volume = 0.11f;
        audioSource2.volume = 0.11f;
        audioSource3.volume = 0.11f;
     }
     else
    {
        //OnCollisionStay mutes sound if in mid-air
        audioSource1.volume = 0f;
        audioSource2.volume = 0f;
        audioSource3.volume = 0f;
    }
     //Get the vehicle's speed
     var velocity = rb.velocity;
     var localVel = transform.InverseTransformDirection(velocity);

     if (moveLeft)
     {
        // Turn the vehicle direction to the left
        transform.Rotate(Vector3.up, -90 * smoothRotation * Time.deltaTime);
        // Go in the direction the vehicle is facing     
        if (localVel.z > 0) 
        {
            rb.velocity = transform.forward * rb.velocity.magnitude;
        }
         else
         {
             rb.velocity = -transform.forward * rb.velocity.magnitude;
         }
     }
     if (moveRight)
     {
        // Turn the vehicle direction to the right
        transform.Rotate(Vector3.up, 90 * smoothRotation * Time.deltaTime);
        // Go in the direction the vehicle is facing
        if (localVel.z > 0)
        {
            rb.velocity = transform.forward * rb.velocity.magnitude;
        }
        else 
        {
            rb.velocity = -transform.forward * rb.velocity.magnitude;
        }
     }
    }
  
  public void MoveLeft ()
  {
    moveLeft = true;
  }
  public void StopMovingLeft ()
  {
    moveLeft = false;
  }
  public void MoveRight ()
  {
    moveRight = true; 
  }
  public void StopMovingRight ()
  {
    moveRight = false;
  }
  public void MoveForward ()
  {
    moveForward = true;
    //OnCollisionStay stops when a rigidbody sleeps Wake rigidbody when asks OnCollisionStay to run
    rbGo.WakeUp();
  }
  public void StopMovingForward ()
  {
    moveForward = false;
  }
  public void MoveBackward ()
  {
    moveBackward = true;
    //OnCollisionStay stops when a rigidbody sleeps Wake rigidbody when asks OnCollisionStay to run
    rbGo.WakeUp();
  }
  public void StopMovingBackward ()
  {
    moveBackward = false;
  }
  

}
