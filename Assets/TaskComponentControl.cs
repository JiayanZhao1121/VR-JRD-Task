using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskComponentControl : MonoBehaviour
{
    public GameObject ground;
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        ground.SetActive(true);
        rightHand.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>().enabled = false;
    }
}
