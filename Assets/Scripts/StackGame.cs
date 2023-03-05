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
        var leftX = blockTransform.localPosition.x - blockTransform.rect.width/2;
        var rightX = blockTransform.localPosition.x + blockTransform.rect.width/2;
        var width = blockTransform.rect.width;
        var center = (leftX + rightX)/2;
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
            if (stackHeight > 0) 
            {
                var prevWidth = previousBlock.GetComponent<RectTransform>().rect.width;
                var prevLeftX = previousBlock.GetComponent<RectTransform>().localPosition.x - prevWidth/2;
                var prevRightX = previousBlock.GetComponent<RectTransform>().localPosition.x + prevWidth/2;
                var currWidth = blockTransform.rect.width;
                var currLeftX = blockTransform.localPosition.x - currWidth/2;
                var currRightX = blockTransform.localPosition.x + currWidth/2;
                if (currLeftX > prevRightX || currRightX < prevLeftX)
                {
                    Debug.Log("Game Over");
                    gameRunning = false;
                    SceneManager.LoadScene("GameOver");
                }
                else if (currLeftX < prevLeftX)
                {
                    var overlap = prevLeftX - currLeftX;
                    var newWidth = currWidth - overlap;
                    var newLeftX = currLeftX + overlap/2;
                    Debug.Log("overlap: " + overlap + " newWidth: " + newWidth + " newLeftX: " + newLeftX + " currRightX: " + currRightX);
                    currLeftX = newLeftX;
                    currWidth = newWidth;
                }
                else if (currRightX > prevRightX)
                {
                    var overlap = currRightX - prevRightX;
                    var newWidth = currWidth - overlap;
                    var newRightX = currRightX - overlap/2;
                    Debug.Log("overlap: " + overlap + " newWidth: " + newWidth + " newRightX: " + newRightX + " currLeftX: " + currLeftX);
                    currRightX = newRightX;
                    currWidth = newWidth;
                }
                // leftX = currLeftX;
                // rightX = currRightX;
                // center = (leftX + rightX)/2;
                // width = currWidth;
                // Debug.Log("currLeftX: " + leftX + " currRightX: " + rightX + " newWidth: " + width + " center: " + center);
                // var square = blockTransform.GetChild(0).GetComponent<RectTransform>();
                // newblock.GetComponent<RectTransform>().localPosition = new Vector3(center, blockTransform.localPosition.y, 0);
                // square.anchoredPosition = new Vector3(center, square.anchoredPosition.y, 0);
                //change square scale to match new block width
                // square.sizeDelta = new Vector2(width, square.rect.height);
            }
            
            var height = blockTransform.rect.height;
            Vector3 newPos = new Vector3(0, blockTransform.localPosition.y+height, 0);
            
            previousBlock = newblock;
            newblock = Instantiate(blockPrefab, newPos, Quaternion.identity);            
            newblock.transform.SetParent(Canvas.transform, false);

            // set new block to width of overlap with old block

            stackHeight++;
        }
    }
}