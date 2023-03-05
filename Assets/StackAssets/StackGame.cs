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
    private GameObject previousBlock;
    private int stackHeight = 0;
    private int winHeight = 20;
    void Start()
    {
        gameRunning = true;
        start = Time.time;
        newblock = Instantiate(blockPrefab, new Vector3(0, -225, 0), Quaternion.identity);
        newblock.transform.SetParent(Canvas.transform, false);
        // make block continuouslly bounce left and right
    }

    void Update()
    {
        if (stackHeight == winHeight)
        {
            Debug.Log("You Win!");
            gameRunning = false;
            enabled = false;
            newblock.SetActive(false);
            // SceneManager.LoadScene("Win");
        }
        RectTransform blockTransform = newblock.GetComponent<RectTransform>();
        var square = blockTransform.GetChild(0).GetComponent<RectTransform>();
        var leftX = blockTransform.localPosition.x - blockTransform.rect.width/2;
        var rightX = blockTransform.localPosition.x + blockTransform.rect.width/2;
        var width = blockTransform.rect.width;
        var center = (leftX + rightX)/2;
        float current = Time.time;
        float speedBuffer = (current-start)*0.05f; // increase speed by 0.1 every second
        speed = 5 + speedBuffer;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stackHeight > 0) 
            {
                var prevWidth = square.localScale.x * 40;
                var prevLeftX = previousBlock.GetComponent<RectTransform>().localPosition.x - prevWidth/2;
                var prevRightX = previousBlock.GetComponent<RectTransform>().localPosition.x + prevWidth/2;
                var prevCenter = previousBlock.GetComponent<RectTransform>().localPosition.x;
                // Debug.Log("prevLeftX: " + prevLeftX + " prevRightX: " + prevRightX + " prevWidth: " + prevWidth + " prevCenter: " + prevCenter);
                var currWidth = square.localScale.x * 40;
                var currLeftX = blockTransform.localPosition.x - currWidth/2;
                var currRightX = blockTransform.localPosition.x + currWidth/2;
                if (currLeftX > prevRightX || currRightX < prevLeftX)
                {
                    Debug.Log("You Lose");
                    gameRunning = false;
                    enabled = false;
                    newblock.SetActive(false);
                    SceneManager.LoadScene("StackGameLose");
                    // SceneManager.LoadScene("GameOver");
                }
                else if (currLeftX < prevLeftX)
                {
                    var overlap = prevLeftX - currLeftX;
                    var newWidth = currWidth - overlap;
                    var newLeftX = prevLeftX;
                    // Debug.Log("overlap: " + overlap + " newWidth: " + newWidth + " newLeftX: " + newLeftX + " currRightX: " + currRightX);
                    currLeftX = newLeftX;
                    currWidth = newWidth;
                }
                else if (currRightX > prevRightX)
                {
                    var overlap = currRightX - prevRightX;
                    var newWidth = currWidth - overlap;
                    var newRightX = prevRightX;
                    // Debug.Log("overlap: " + overlap + " newWidth: " + newWidth + " newRightX: " + newRightX + " currLeftX: " + currLeftX);
                    currRightX = newRightX;
                    currWidth = newWidth;
                }
                leftX = currLeftX;
                rightX = currRightX;
                center = (leftX + rightX)/2;
                width = currWidth;
                // Debug.Log("currLeftX: " + leftX + " currRightX: " + rightX + " newWidth: " + width + " center: " + center);
                blockTransform.localPosition = new Vector3(center, blockTransform.localPosition.y, 0);
                square.localScale = new Vector3(width/40, 10, 0);
            }
            
            if (gameRunning) 
            {
                var height = blockTransform.rect.height;
                Vector3 newPos;
                if (direction == "right") {
                    newPos = new Vector3(width/2, blockTransform.localPosition.y+height, 0);
                }
                else
                {
                    newPos = new Vector3(-1*width/2, blockTransform.localPosition.y+height, 0);
                }
                
                previousBlock = newblock;
                newblock = Instantiate(newblock, newPos, Quaternion.identity);            
                newblock.transform.SetParent(Canvas.transform, false);
                square = blockTransform.GetChild(0).GetComponent<RectTransform>();
                square.localScale = new Vector3(width/40, 10, 0);
                stackHeight++;
            }
        }
    }
}