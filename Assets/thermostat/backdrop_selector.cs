using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backdrop_selsector : MonoBehaviour
{
    bool coroutine_running = false;

    public bool can_change_scene = false;

    int temp_threshold;

    Image backdrop;
    TextMeshProUGUI tempText;

    // Different sprites for different room temperatures
    Sprite cold;
    Sprite norm;
    Sprite warm;

    RadialSlider radial_script;

    // Start is called before the first frame update
    void Start()
    {
        // Random setting of temperature threshold where the room is perfect
        temp_threshold = (int) (Random.value * 20 + 60);

        // Initializes sprites for each of the temperature states
        cold = LoadNewSprite("Assets/thermostat/cold.png");
        norm = LoadNewSprite("Assets/thermostat/norm.png");
        warm = LoadNewSprite("Assets/thermostat/warm.png");

        // Initializes backdrop checker and sets the backdrop to the cold room
        backdrop = GameObject.Find("backdrop").GetComponent<Image>();
        backdrop.sprite = cold;

        // Initializes variable that checks the current temperature
        tempText = GetComponentInChildren<TextMeshProUGUI>();

        radial_script = GameObject.Find("Dial").GetComponent<RadialSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        int cur_temp = System.Int32.Parse(tempText.text);

        if (cur_temp == temp_threshold)
        {
            radial_script.isOnTarget = true;
            backdrop.sprite = norm;

            if (radial_script.gameWon && !coroutine_running)
            {
                StartCoroutine("winGame");
            }

        }
        else
        {
            GameObject.Find("Dial").GetComponent<RadialSlider>().isOnTarget = false;
            if (cur_temp < temp_threshold)
            {
                backdrop.sprite = cold;
            }
            else
            {
                backdrop.sprite = warm;
            }
        }
    }

    IEnumerator winGame()
    {
        coroutine_running = true;
        float wait_time = 0.25f;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(wait_time);
            tempText.color = Color.green;
            yield return new WaitForSeconds(wait_time);
            tempText.color = Color.white;
        }
        tempText.color = Color.green;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("dummy");
        coroutine_running = false;
    }

    //Methods I'm borrowing to load my sprites

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Sprite NewSprite;
        Texture2D SpriteTexture = LoadTexture(FilePath);
        NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
}
