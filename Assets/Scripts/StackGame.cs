using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StackGame : MonoBehaviour 
{
    public GameObject blockPrefab;
    public GameObject Canvas;

    GameObject newblock;
    public Vector3 leftBound;
    public Vector3 rightBound;
    private float speed = 5F;
    public string direction = "right";
    private float start = 0;
    Vector3 velocity;
    void Start()
    {
        start = Time.time;
        newblock = Instantiate(blockPrefab, new Vector3(0, -250, 0), Quaternion.identity);
        newblock.transform.SetParent(Canvas.transform, false);
        // make block continuouslly bounce left and right
    }

    void Update()
    {
        float current = Time.time;
        float speedBuffer = (current-start)*0.05f; // increase speed by 0.1 every second
        speed = 5 + speedBuffer;
        // make block continuouslly bounce left and right
        if (newblock.transform.position.x < leftBound.x)
        {
            direction = "right";
        }
        else if (newblock.transform.position.x > rightBound.x)
        {
            direction = "left";
        }
        if (direction == "right")
        {
            newblock.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (direction == "left")
        {
            newblock.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        }
    }
}