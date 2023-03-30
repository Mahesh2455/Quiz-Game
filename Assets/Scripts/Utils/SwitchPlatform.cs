using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System;

public class SwitchPlatform : MonoBehaviour
{
    
    public static Action OnVRConnected;


    // Start is called before the first frame update
    void Start()
    {
        CheckVR();

    }

    public void CheckVR()
    {
        List<UnityEngine.XR.InputDevice> inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        if (inputDevices.Count > 0)
        {
            //Debug.Log("vr is on ");
            OnVRConnected?.Invoke();
        }

        else
        {
            //Debug.Log("vr not connected ");
        }
    }

    // Update is called once per frame
    void Update()
    {
      

    }
}
