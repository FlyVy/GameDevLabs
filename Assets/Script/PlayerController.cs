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
    public float moveSpeed = 7f;
    public float jumpForce = 21f;
    public LayerMask jumpableGround;

    private enum MovementState {idle, running, jumping}

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Player start");
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*moveSpeed,rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            countScoreState = true; //check if Gomba is underneath   
            isGrounded = false;
        }
        if (Time.timeScale==1.0f)
        {
            anim.SetInteger("state", getState());
        ScoreUpdate();
        }
        

    }

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
    private int getState()
    {
        if (Math.Abs(rb.velocity.y)>.1f)
        {
            return (int)MovementState.jumping;
        }

        if (dirX>0f)
        {
            sprite.flipX = false;
            return (int)MovementState.running;
        } else if (dirX<0f)
        {
            sprite.flipX = true;
            return (int)MovementState.running;
        }
        
        return (int)MovementState.idle;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            countScoreState = false; // reset score state
            isGrounded = true;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0.0f;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            RestartLevel();
        }
     }

     private void RestartLevel()
     {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }


}
