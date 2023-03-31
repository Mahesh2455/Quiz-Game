using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class HandPresence : MonoBehaviour
{
    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    private float triggerValue, gripValue;
    [SerializeField] GameObject handModelPrefab;
    private Animator handAnimator;
    private GameObject spawnedHandModel;

    private void Start() 
    {
        TryInitialize();
    }

    private void Update() 
    {
        if(!targetDevice.isValid)
            TryInitialize();
        else
            UpdateHandAnimation();
    }

    private void TryInitialize()
    {
        //Try to get the list of input devices HMD and controllers
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);//Get the deveices with char like left controller

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    private void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }

    }
}
