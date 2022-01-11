using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quiz : MonoBehaviour
{
    [SerializeField] private GridObjects _grid;

    private string _quizTarget;
    private List<string> _usedQuiz = new List<string>();
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
        _usedQuiz.Add(cardData[randomIndex].Identifier);

        foreach (var quiz in _usedQuiz)
        {
            while(quiz == cardData[randomIndex].Identifier)
            {
                randomIndex = Random.Range(0, cardData.Count);
            }
        }

        return cardData[randomIndex].Identifier;
    }

    private void OnLevelStarted()
    {
        _quizTarget = TakeQuiz(_grid.UsedCards);
        QuizTaked?.Invoke(_quizTarget);
    }
}
