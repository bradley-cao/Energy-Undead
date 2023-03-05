using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobby_states : MonoBehaviour
{
    int sink_state = -1;
    int fridge_state = -1;
    // Sprite fridge = 
    public SpriteRenderer sink_renderer;
    public SpriteRenderer fridge_renderer;
    public Sprite[] sinks;
    public Sprite[] fridges;

    // Start is called before the first frame update
    void Start()
    {
        // each time the lobby is loaded, all the appliances increment
        if (sink_state < 3)
        {
            sink_state += 1;
            sink_renderer.sprite = sinks[sink_state];
        }
        if(fridge_state < 2)
        {
            fridge_state += 1;
            fridge_renderer.sprite = fridges[fridge_state];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
