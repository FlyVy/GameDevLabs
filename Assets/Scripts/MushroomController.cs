using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float speed;
    public float maxSpeed = 2;
    private Vector2 currentDirection = new Vector2(1, 0);
    private Vector2 currentPosition;
    private bool hit =  false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.up  *  10, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        Vector2 movement = currentDirection;
        if (rigidBody.velocity.magnitude < maxSpeed)
                rigidBody.AddForce(movement * speed, ForceMode2D.Impulse);
    }

    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit){
            speed = 0;
            rigidBody.velocity = Vector2.zero;
            hit = true;
        }
        if (col.gameObject.CompareTag("Pipe")){
            currentDirection *= -1;
        }
    }
    void  OnBecameInvisible(){
        Destroy(gameObject);	
    }
    // Update is called once per frame
    void Update()
    {
    }
}
