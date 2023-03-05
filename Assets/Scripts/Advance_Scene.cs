using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Advance_Scene : MonoBehaviour
{
    public Sprite[] cutscenes;
    int current_cutscene = 0;
    public SpriteRenderer cutscene_renderer;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("stink");
            current_cutscene++;
            if (current_cutscene > 2) { SceneManager.LoadScene("LobbyScene"); }
            else { cutscene_renderer.sprite = cutscenes[current_cutscene]; }
        }
    }
}
