using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("windows_complete", 0);
        PlayerPrefs.SetInt("walls_complete", 0);
        PlayerPrefs.SetInt("thermo_complete", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
