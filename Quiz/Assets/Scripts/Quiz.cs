using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Quiz : MonoBehaviour
{
    [SerializeField] private LevelGrid _level;

    private string _quizTarget;
    private List<string> _usedQuiz = new List<string>();

    public string QuizTarget => _quizTarget;

    public event UnityAction<string> QuizTaked;

    private void OnEnable()
    {
        _level.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _level.LevelStarted -= OnLevelStarted;
    }

    public string TakeQuiz(List<CardData> cardData)
    {
        int randomIndex = Random.Range(0, cardData.Count);
        _usedQuiz.Insert(0, cardData[randomIndex].Identifier);
        bool isQuizUsed;

        do
        {
            isQuizUsed = _usedQuiz.Skip(1).Any(quiz => quiz == cardData[randomIndex].Identifier);

            if (isQuizUsed == true)
                randomIndex = Random.Range(0, cardData.Count);
        }
        while (isQuizUsed);

        return cardData[randomIndex].Identifier;
    }

    private void OnLevelStarted()
    {
        _quizTarget = TakeQuiz(_level.UsedCards);
        QuizTaked?.Invoke(_quizTarget);
    }
}
