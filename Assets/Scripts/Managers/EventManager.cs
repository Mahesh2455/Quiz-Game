using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private static EventManager _instance;
    public static EventManager Instance => _instance;
    public Action OnGameCompleted;
    public Action OnCorrectAnswered;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }


}
