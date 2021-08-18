using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformLeft : MonoBehaviour
{
    float dirX, moveSpeed = 1f;
    bool moveLeft = true;
    float startPos;
    //Get startposition for x.axis
    void Start()
    {
        startPos = transform.position.x;
    }
    //Move platform to left from startposition and right after moved 1 frame
    void Update()
    {
        if (transform.position.x < startPos + -1f)
                 moveLeft = false;
                 
             if(transform.position.x > startPos + 1f)
                 moveLeft = true;
            
            //Right
            if (moveLeft)
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            //Left
            else
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
    }
}
