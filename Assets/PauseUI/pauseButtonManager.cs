using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseManager : MonoBehaviour
{
    public Button pauseButton;
	void Start () {
		Button btn = pauseButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
	}
}