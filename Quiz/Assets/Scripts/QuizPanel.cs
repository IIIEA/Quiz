using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class QuizPanel : MonoBehaviour
{
    [SerializeField] private Quiz _quiz;
    
    private TMP_Text _tmpText;
    private CanvasGroup _canvas;

    private const string _quizText = "Choose ";

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        _tmpText = GetComponentInChildren<TMP_Text>();

        FadeOut();
    }

    private void OnEnable()
    {
        _quiz.QuizTaked += OnQuizChanged;
    }

    private void OnDisable()
    {
        _quiz.QuizTaked -= OnQuizChanged;
    }

    private void FadeOut()
    {
        _canvas.DOFade(1, 2);
    }

    private void OnQuizChanged(string currentQuiz)
    {
        _tmpText.text = _quizText + currentQuiz;
    }
}
