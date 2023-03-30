using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [Header("Android UI Elements")]
    [SerializeField] private QuizManager _quizManager;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private GameObject _eventSystem;


    [Header("VR UI Elements")]
    [SerializeField] private GameObject _gameScreenVR;
    [SerializeField] private GameObject _endScreenVR;
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
        _eventSystem.SetActive(false);
        _xrSetup.SetActive(true);
        _gameScreen = _gameScreenVR;
        _endScreen = _endScreenVR;
    }

    private void DisplayEndScreen()
    {
        _gameScreen.SetActive(false);
        _endScreen.SetActive(true);
    }

}
