using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Script for input keyboard on UI canvas. Needs to be hooked up through UI buttons OnClick -> IDKeyboard -> ClickKey with correct number/letter
/// </summary>
/// 
public class IDKeyboard : MonoBehaviour
{

    public TMPro.TMP_InputField inputField;
    private string tempCurString;

    private float startTime = 0f;
    public GameObject ParentGameobject;
    public Transform cameraTransform;
    private bool isStart = false;

    public GameObject experimentalConsole;

    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        if (Time.time - startTime > 5f && !isStart)
        {
            ParentGameobject.SetActive(true);
            ParentGameobject.transform.eulerAngles = new Vector3(0f, cameraTransform.eulerAngles.y, 0f);
            isStart = true;
        }
    }
    public void ClickKey(string keyClicked)
    {
        // save current text into variable
       
            tempCurString =  inputField.text;
        
       
 
        // add current text to new string
        string tempNewString = tempCurString + keyClicked;

        // set input field text to tempNewString
        inputField.text = tempNewString;
        Debug.Log("inputField = " + inputField.text);
    }

    public void ClickBackspace()
    {
        string tempGetString = inputField.text;

        if (tempGetString.Length > 0)
        {
            string tempString = tempGetString.Substring(0, tempGetString.Length - 1);

            inputField.text = tempString;
        }
    
    }
    public void ClickSubmit()
    {
       
        PlayerPrefs.SetString("participantID", inputField.text);
        experimentalConsole.SetActive(true);
        ParentGameobject.SetActive(false);
        enabled = false;

    }


}
