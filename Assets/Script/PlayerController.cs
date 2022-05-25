using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    
    private float dirX;
    private float dirY;
    private bool isGrounded = true;
    private float moveSpeed = 2f;
    private float jumpForce = 21f;
    private float maxSpeed = 7f;
    public int state=0;

    private enum MovementState {idle, running, jumping,skidding}

    // Start is called before the first frame update
    private void Start()
    {   
        Application.targetFrameRate = 30;   
        Debug.Log("Player start");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update the physics
    void FixedUpdate()
    {      
        dirX = Input.GetAxis("Horizontal");
        if (Mathf.Abs(dirX) > 0){
            if (Math.Abs(rb.velocity.x)<maxSpeed){
                rb.velocity += new Vector2(dirX*moveSpeed,0);
                Debug.Log(rb.velocity);
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded && rb.velocity.y<0.1f)
        {
            rb.velocity += new Vector2(0,jumpForce);
            countScoreState = true; //check if Gomba is underneath   
            isGrounded = false;
        }
    }
    // Update the animations (not needed for checkoff1)
    private void Update()
    {   
        if (Time.timeScale==1.0f)
        {   
            state = getState();
            anim.SetInteger("state", state);
            ScoreUpdate();
        }
    }

    // Update the score for checkoff1
    private void ScoreUpdate()
    {
        if (!isGrounded && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
            }
      }
    }
    
    // Manage and update states
    private int getState()
    {
        if (Math.Abs(rb.velocity.y)>.1f)
        {
            return (int)MovementState.jumping;
        }

        // Development for skidding animation
        // Need to make it smoother
        /* if (rb.velocity.x>2f){
            sprite.flipX = true;
            return (int)MovementState.skidding;
        } else if(rb.velocity.x<-2f){
            sprite.flipX = false;
            return (int)MovementState.skidding;
        } */

        if (rb.velocity.x>.1f)
        {
            sprite.flipX = false;
            return (int)MovementState.running;
        } else if (rb.velocity.x<-.1f)
        {
            sprite.flipX = true;
            return (int)MovementState.running;
        }
        
        return (int)MovementState.idle;
    }

    // Check for collision with ground
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            countScoreState = false; // reset score state
            isGrounded = true;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Check for collision with goomba
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0.0f;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            RestartLevel();
        }
     }

    // Restart scene
     private void RestartLevel()
     {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }


}
