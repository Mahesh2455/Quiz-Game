using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    
    [Header("Android UI Elements")]
    [SerializeField] private QuizManager _quizManager;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private InputSystemUIInputModule _inputUIAndroid;


    [Header("VR UI Elements")]
    [SerializeField] private GameObject _xrSetup;

    private static GameManager _instance;
    public static GameManager Instance => _instance;


    private void Awake() 
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        SwitchPlatform.OnVRConnected+= SwitchVRUI;
       
    }
    private void Start() 
    {
        EventManager.Instance.OnGameCompleted += DisplayEndScreen;
        
        _gameScreen.SetActive(true);
        _endScreen.SetActive(false);
    }

    private void OnDisable() 
    {
        EventManager.Instance.OnGameCompleted -= DisplayEndScreen;
        SwitchPlatform.OnVRConnected -= SwitchVRUI;
    }

    private void SwitchVRUI()
    {
        _inputUIAndroid.enabled = false;
        _xrSetup.SetActive(true);
        ChangeUIPosition(_gameScreen);
        ChangeUIPosition(_endScreen);
    }

    private void DisplayEndScreen()
    {
        _gameScreen.SetActive(false);
        _endScreen.SetActive(true);
    }

    private void ChangeUIPosition(GameObject _screen)
    {
        _screen.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        _screen.GetComponent<RectTransform>().localPosition = new Vector3(0,1,2);
        _screen.GetComponent<RectTransform>().localScale = new Vector3(0.001f,0.001f,0.001f);
    }



}
