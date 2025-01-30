using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTest : MonoBehaviour
{
    public GameObject startUIparent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Trigger states") == "Trigger released")
        {
            startUIparent.SetActive(!startUIparent.activeSelf);
        }
    }
}
