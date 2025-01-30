using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class pointing : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] XRController controller;

    private int stage = 0;
 

   

    void Start()
    {

        PlayerPrefs.SetString("Trigger states", "Null");

    }

    // Update is called once per frame
    void Update()
    {

       


        controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);

        Debug.Log("trigger states = " + PlayerPrefs.GetString("Trigger states"));
        Debug.Log("pointing trigger stage = " + stage);
        Debug.Log("triggerValue = " + triggerValue);
        if (triggerValue > 0.6f && stage == 0)
        {
            PlayerPrefs.SetString("Trigger states", "Trigger pressed");
            stage++;
        }
        else if (triggerValue < 0.1f && stage == 1)
        {
                PlayerPrefs.SetString("Trigger states", "Trigger released");
            stage = 0;
            
        }
        Debug.Log("trigger states = " + PlayerPrefs.GetString("Trigger states"));





    }
    
  

   
}
