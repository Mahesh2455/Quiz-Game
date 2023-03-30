using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    public int Score => _score;


    private void Start() 
    {
        EventManager.Instance.OnCorrectAnswered += () => _score++;
    }

    private void OnDisable() 
    {
        EventManager.Instance.OnCorrectAnswered -= () => _score++;
    }

}
