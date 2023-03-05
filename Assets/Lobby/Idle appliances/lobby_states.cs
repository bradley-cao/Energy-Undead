using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class lobby_states : MonoBehaviour
{
    // idle appliances start at random sprite costumes
    int sink_state = -1;
    int fridge_state = -1;
    
    public SpriteRenderer sink_renderer;
    public SpriteRenderer fridge_renderer;
    public Sprite[] sinks;
    public Sprite[] fridges;

    // minigames are in hardcoded order for demo purposes
    int window_state = -1;
    int wall_state = 0;
    int thermo_state = 1;
    public SpriteRenderer window_renderer;
    public SpriteRenderer wall_renderer;
    public SpriteRenderer thermo_renderer;
    public Sprite[] windows;
    public Sprite[] walls;
    public Sprite[] thermos;

    // Start is called before the first frame update
    void Start()
    {
        // saves appliance states into PlayerPrefs for simple access throughout scenes
        if (PlayerPrefs.HasKey("sink_state"))
        {
            sink_state = PlayerPrefs.GetInt("sink_state");
        }
        if(PlayerPrefs.HasKey("fridge_state"))
        {
            fridge_state = PlayerPrefs.GetInt("fridge_state");
        }
        if(PlayerPrefs.HasKey("window_state"))
        {
            window_state = PlayerPrefs.GetInt("window_state");
        }
        if(PlayerPrefs.HasKey("wall_state"))
        {
            wall_state = PlayerPrefs.GetInt("wall_state");
        }
        if(PlayerPrefs.HasKey("thermo_state"))
        {
            thermo_state = PlayerPrefs.GetInt("thermo_state");
        }

        // each time the lobby is loaded, all the appliances increment
        if(sink_state < 3)
        {
            sink_state += 1;

            if(sink_state < 0)
            {
                sink_renderer.sprite = sinks[0];
            }
            else
            {
                sink_renderer.sprite = sinks[sink_state];
            } 
        }
        if(fridge_state < 2)
        {
            fridge_state += 1;
            if(fridge_state < 0)
            {
                fridge_renderer.sprite = fridges[0];
            }
            else
            {
                fridge_renderer.sprite = fridges[fridge_state];
            }
        }

        if (window_state < 2)
        {
            window_state += 1;
            if (window_state < 0)
            {
                window_renderer.sprite = windows[0];
            }
            else
            {
                window_renderer.sprite = windows[window_state];
            }
        }
        if(wall_state < 2)
        {
            wall_state += 1;
            if (wall_state < 0)
            {
                wall_renderer.sprite = walls[0];
            }
            else
            {
                wall_renderer.sprite = walls[wall_state];
            }
        }
        if(thermo_state < 2)
        {
            thermo_state += 1;
            if (thermo_state < 0)
            {
                thermo_renderer.sprite = thermos[0];
            }
            else
            {
                thermo_renderer.sprite = thermos[thermo_state];
            }
        }

        PlayerPrefs.SetInt("sink_state", sink_state);
        PlayerPrefs.SetInt("fridge_state", fridge_state);
        PlayerPrefs.SetInt("window_state", window_state);
        PlayerPrefs.SetInt("wall_state", wall_state);
        PlayerPrefs.SetInt("thermo_state", thermo_state);
    }
}
