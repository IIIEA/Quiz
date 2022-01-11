using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSystem))]
public class Card : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private Quiz _quiz;
    private Button _button;
    private string _identifier;
    private ParticleSystem _particles;

    public event UnityAction CorrectCard;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        _quiz = GetComponentInParent<Quiz>();
        _button = GetComponentInChildren<Button>();
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
            _particles.Play();

            CorrectCard?.Invoke();
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