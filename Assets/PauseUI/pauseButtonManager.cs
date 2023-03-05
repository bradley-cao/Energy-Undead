using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseManager : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public GameObject gameCanvas;
    public GameObject pauseCanvas;
    public GameObject pauseMenu;
	void Start () {
        pauseMenu.SetActive(true);
        gameCanvas.SetActive(true);
        // pauseCanvas.SetActive(false);
		Button btn = pauseButton.GetComponent<Button>();
		btn.onClick.AddListener(pauseClick);
	}

	void pauseClick(){
        Debug.Log("Paused");
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        
        Button btn2 = resumeButton.GetComponent<Button>();
        btn2.onClick.RemoveAllListeners();
        resumeClick();
	}

    void resumeClick(){
        gameCanvas.SetActive(true);
        pauseCanvas.SetActive(true);
        pauseMenu.SetActive(false);

        Button btn = pauseButton.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        pauseClick();
    }
}