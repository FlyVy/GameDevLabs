using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform player;

    // Update is called once per frame
    private void Update()
    {   
        if (transform.position.x-player.position.x<.1f){
            transform.position = new Vector3(player.position.x, transform.position.y, -10);
        }

    }
}
