using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<QuestionData> _questionDataList = new List<QuestionData>();
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private List<GameObject> _answerTextList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    private int _currentQuestionIndex;
    [SerializeField] private ScoreManager _scoreManager;

    
    private void Awake() 
    {
        //SwitchPlatform.OnVRConnected+= SwitchVRUI;
    }

    void Start()
    {
        DisplayQuestion();
    }

    private void OnDisable() 
    {
        //SwitchPlatform.OnVRConnected-= SwitchVRUI;
    }

    private void DisplayQuestion()
    {
        _questionText.text = _questionDataList[_currentQuestionIndex].GetQuestion();
        for (int i = 0; i < _answerTextList.Count; i++)
        {
            _answerTextList[i].GetComponentInChildren<TextMeshProUGUI>().text = _questionDataList[_currentQuestionIndex].GetAnswer(i);
        }
         foreach(GameObject answersObj in _answerTextList)
        {
            answersObj.GetComponent<Button>().enabled = true;
        }
    }

    public void UIEVENT_OnAnswerButtonClicked(int _answerNo)
    {
        foreach(GameObject answersObj in _answerTextList)
        {
            answersObj.GetComponent<Button>().enabled = false;
        }
        VerifyAnswer(_answerNo);
    }

    private void VerifyAnswer(int _answerNo)
    {
        if (_questionDataList[_currentQuestionIndex].GetCorrectAnswerIndex() == _answerNo)
        {
            _debugText.text = "Correct answer";
            EventManager.Instance.OnCorrectAnswered?.Invoke();
        }
        else
            _debugText.text = "Wrong answer";
        _currentQuestionIndex++;
        StartCoroutine(WaitBeforeDisplayingNextQuestion());
        
    }

    private IEnumerator WaitBeforeDisplayingNextQuestion()
    {
        yield return new WaitForSeconds(2);
         _debugText.text = " ";
        if (_currentQuestionIndex < _questionDataList.Count)
            DisplayQuestion();
        else
        {
            EventManager.Instance.OnGameCompleted?.Invoke();
            int finalScore = Mathf.RoundToInt( ((float)_scoreManager.Score/_questionDataList.Count) * 100);
            _finalScoreText.text = $"Your final score is {finalScore} %";
        }
           
    }
}
