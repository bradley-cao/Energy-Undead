using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float maxSpeed = 2;
    public Animator animator;

    // current_dir refers to the most recent movement direction, for character sprite. 0 is unmoving, 1 is right, clockwise (2 = down, etc.)
    int current_dir;
    // directions represented as -1, 0, 1 so they can be added to vectors
    int left_right;
    int up_down;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Good morning");
        transform.position = new Vector2(1,1); // XXX finish this
    }

    // Update is called once per frame
    void Update()
    {
        current_dir = 0; // 0 = not moving
        // up/down
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            up_down = -1; // Unity uses left-handed coords with (0,0) at bottom left of screen
            current_dir = 2;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            up_down = 1;
            current_dir = 4;
        }
        else
        {
            up_down = 0;
        }

        // left/right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            left_right = -1;
            current_dir = 3;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            left_right = 1;
            current_dir = 1;
        }
        else
        {
            left_right = 0;
            // doesn't change the last direction if no keys are pressed
        }

        // updates movement
        transform.Translate(new Vector2(left_right, up_down) * Time.deltaTime * maxSpeed);

        // updates sprite appearance to reflect correct direction
        animator.SetInteger("current_dir", current_dir);
    }
}
