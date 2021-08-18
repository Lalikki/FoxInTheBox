using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatformRight : MonoBehaviour
{
    
    float dirX, moveSpeed = 1f;
    bool moveRight = true; 
    float startPos;
    // Update is called once per frame
    void Start()
    {   
        startPos = transform.position.x;
    }
    //Move platform to right from startposition and left after moved 1 frame
    void Update()
    {           
             if (transform.position.x > startPos + 1f)
                 moveRight = false;
                 
             if(transform.position.x < startPos + -1f)
                 moveRight = true;
                 
            //right
            if (moveRight)
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            //left
            else
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);         
    } 
}
