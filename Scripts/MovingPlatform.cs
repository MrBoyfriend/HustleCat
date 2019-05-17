using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    private Transform initialStartMarker;
    private Transform initialEndMarker;

    public bool moveRight;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (moveRight == true)
        {
            MoveRight();
        } else if(moveRight == false)
        {
            MoveLeft();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Left")
        {
            moveRight = true;
        }

        if (other.tag == "Right")
        {
            moveRight = false;
        }
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
