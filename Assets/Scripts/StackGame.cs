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
    public bool gameRunning = false;
    public Vector3 leftBound;
    public Vector3 rightBound;
    private float speed = 5F;
    public string direction = "right";
    private float start = 0;
    Vector3 velocity;
    void Start()
    {
        gameRunning = true;
        start = Time.time;
        newblock = Instantiate(blockPrefab, new Vector3(0, -250, 0), Quaternion.identity);
        newblock.transform.SetParent(Canvas.transform, false);
        Debug.Log(newblock.transform.position);
        // make block continuouslly bounce left and right
    }

    void Update()
    {
        RectTransform blockTransform = newblock.GetComponent<RectTransform>();
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

        // on spacebar press, create new block, set it as child of canvas, and set it to the position of the old block, and stop old block
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //instantiate new block above old block
            var height = blockTransform.rect.height;
            Vector3 newPos = new Vector3(0, blockTransform.anchoredPosition.y+height, 0);
            newblock = Instantiate(blockPrefab, newPos, Quaternion.identity);
            newblock.transform.SetParent(Canvas.transform, false);
        }
    }
}