using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<QuestionData> _questionDataList = new List<QuestionData>();

    [Header("Android UI Elements")] 
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private List<TextMeshProUGUI> _answerTextList = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    [Header("VR UI Elements")] 
    [SerializeField] private TextMeshProUGUI _questionTextVR;
    [SerializeField] private List<TextMeshProUGUI> _answerTextListVR = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI _debugTextVR;
    [SerializeField] private TextMeshProUGUI _finalScoreTextVR;
    private int _currentQuestionIndex;
    [SerializeField] private ScoreManager _scoreManager;

    
    private void Awake() 
    {
        SwitchPlatform.OnVRConnected+= SwitchVRUI;
    }

    void Start()
    {
        DisplayQuestion();
    }

    private void OnDisable() 
    {
        SwitchPlatform.OnVRConnected-= SwitchVRUI;
    }

    private void DisplayQuestion()
    {
        _questionText.text = _questionDataList[_currentQuestionIndex].GetQuestion();
        for (int i = 0; i < _answerTextList.Count; i++)
        {
            _answerTextList[i].text = _questionDataList[_currentQuestionIndex].GetAnswer(i);
        }
    }

    public void UIEVENT_OnAnswerButtonClicked(int _answerNo)
    {
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
            Debug.Log("Game completed");
            int finalScore = Mathf.RoundToInt( ((float)_scoreManager.Score/_questionDataList.Count) * 100);
            _finalScoreText.text = $"Your final score is {finalScore} %";
        }
           
    }

    private void SwitchVRUI()
    {
       _questionText = _questionTextVR;
       _answerTextList = _answerTextListVR;
       _debugText = _debugTextVR;
       _finalScoreText = _finalScoreTextVR;
    }
}
