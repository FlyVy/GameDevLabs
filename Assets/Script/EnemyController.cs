using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float startX;
    private float maxOffset = 8.0f;
    private float enemyPatroltime = 4.0f;
    private Vector2 velocity;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log("Enemy start");
        rb = GetComponent<Rigidbody2D>();
        // get the starting position
        startX = transform.position.x;
        ComputeVelocity();
    }

    // Move goomba in a straight line in a direction
    void ComputeVelocity(){
      velocity = new Vector2(-1*maxOffset / enemyPatroltime, 0);
    }

    // Update goomba velocity and reverse when hit maximum displacement
    void Update()
    {      
        // check if offset have exceeded maximum limit
        // if yes, move the starting position to current position 
        // and reverse goomba velocity
        if (Mathf.Abs(rb.position.x - startX) > maxOffset)
        {
            startX = transform.position.x;
            velocity *= -1;
        }
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

}
