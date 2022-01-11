using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class QuizPanel : MonoBehaviour
{
    [SerializeField] private Quiz _quiz;
    [SerializeField] private TMP_Text _tmpText;

    private CanvasGroup _canvas;

    private const string QuizText = "Choose ";

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();

        OnGameStarted();
    }

    private void OnEnable()
    {
        _quiz.QuizTaked += OnQuizChanged;
    }

    private void OnDisable()
    {
        _quiz.QuizTaked -= OnQuizChanged;
    }

    private void OnGameStarted()
    {
        _canvas.alpha = 0;
        _canvas.DOFade(1, 2);
    }

    private void OnQuizChanged(string currentQuiz)
    {
        _tmpText.text = QuizText + currentQuiz;
    }
}
