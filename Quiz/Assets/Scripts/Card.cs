using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    private Quiz _quiz;
    private string _identifier;

    public event UnityAction<Transform> CorrectCard;

    private void Awake()
    {

        _quiz = GetComponentInParent<Quiz>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }

    public void Init(Sprite icon, string identifier)
    {
        _icon.sprite = icon;
        _identifier = identifier;
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
    }

    private void OnClickButton()
    {
        if(_identifier == _quiz.QuizTarget)
        {
            BounceEffect(_icon.transform);

            CorrectCard?.Invoke(gameObject.transform);
        }
        else
        {
            _icon.transform.DOShakePosition(1f, strength: new Vector3(5, 0, 0), vibrato: 8, randomness: 1);
        }
    }

    private void BounceEffect(Transform transform)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1), 0.2f));
        sequence.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.15f));
        sequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.15f));
    }
}