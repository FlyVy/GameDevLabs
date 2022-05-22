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

    void ComputeVelocity(){
      velocity = new Vector2(-1*maxOffset / enemyPatroltime, 0);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Mathf.Abs(rb.position.x - startX) > maxOffset)
        {// move gomba
            startX = transform.position.x;
            velocity *= -1;
        }
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

}
