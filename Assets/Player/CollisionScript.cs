using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.rigidbody);
        if(Input.GetKey(KeyCode.E))
        {
            string current_scene = collision.rigidbody.GetComponent<Text>().text;
            PlayerPrefs.SetString("last_scene", current_scene);

            if(current_scene == "stack" || current_scene == "windowpane" || current_scene == "thermostat")
            {
                SceneManager.LoadScene(current_scene);
            }
            else
            {
                if(current_scene == "fridge")
                {
                    PlayerPrefs.SetInt("fridge_state", -1);
                }
                else if(current_scene == "sink")
                {
                    PlayerPrefs.SetInt("sink_state", -1);
                }
            }
        }
    }
}
