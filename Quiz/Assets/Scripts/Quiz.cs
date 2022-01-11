using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quiz : MonoBehaviour
{
    [SerializeField] private GridObjects _grid;

    private string _quizTarget;

    public string QuizTarget => _quizTarget;

    public event UnityAction<string> QuizTaked;

    private void OnEnable()
    {
        _grid.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _grid.LevelStarted -= OnLevelStarted;
    }

    public string TakeQuiz(List<CardData> cardData)
    {
        int randomIndex = Random.Range(0, cardData.Count);
        
        return cardData[randomIndex].Identifier;
    }

    private void OnLevelStarted()
    {
        _quizTarget = TakeQuiz(_grid.UsedCards);
        QuizTaked?.Invoke(_quizTarget);
    }
}
