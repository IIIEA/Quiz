using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GridLayoutGroup), typeof (Quiz))]
public class Level : MonoBehaviour
{
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private Card _template;
    [SerializeField] private RestartButton _restartButton;
    [SerializeField] private Image _backgroundFade;
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private ParticleSystem _particles;

    private Quiz _quiz;
    private GridLayoutGroup _layout;
    private LevelData _currentLvl;
    private int _currentLvlIndex = 0;
    private List<CardData> _usedCards = new List<CardData>();
    private Card[] _cards;

    public event UnityAction LevelStarted;
    public List<CardData> UsedCards => _usedCards;

    private void Awake()
    {
        _quiz = GetComponent<Quiz>();
        _layout = GetComponent<GridLayoutGroup>();

        _layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
    }

    private void Start()
    {
        LevelStarted?.Invoke();
        transform.localScale = Vector3.zero;
        StartCoroutine(BounceEffect());
    }

    private void OnEnable()
    {
        Fill(_currentLvlIndex);

        _cards = GetComponentsInChildren<Card>();

        foreach (var card in _cards)
        {
            card.CorrectCard += OnCorrectCard;
        }

        _layout.constraintCount = _currentLvl.RowsCount;

        _restartButton.Restarted += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        foreach (var card in _cards)
        {
            card.CorrectCard -= OnCorrectCard;
        }

        StopCoroutine(BounceEffect());

        _restartButton.Restarted -= OnRestartButtonClick;
    }

    private void OnCorrectCard(Transform transform)
    {
        _currentLvlIndex++;
        _particles.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _particles.Play();

        if (_currentLvlIndex < _levels.Length)
        {
            StartCoroutine(StartNewLvl());
        }
        else
        {
            _backgroundFade.gameObject.SetActive(true);
            _backgroundFade.DOFade(1f, 1f);
            StopCoroutine(StartNewLvl());
        }
    }

    private void Fill(int lvlIndex)
    {
        if (_levels.Length < lvlIndex)
            return;

        _currentLvl = _levels[lvlIndex];
        
        var cardBundle = _currentLvl.GetRandomCardBundle();

        foreach (var cardData in cardBundle.CardData)
        {
            _usedCards.Add(cardData);
        }

        int randomIndex;

        for (int i = 0; i < (_currentLvl.RowsCount * _currentLvl.ColumnsCount); i++)
        {
            randomIndex = Random.Range(0, _usedCards.Count);

            var card = Instantiate(_template, transform);
            card.Init(_usedCards[randomIndex].Icon, _usedCards[randomIndex].Identifier);

            _usedCards.RemoveAt(randomIndex);
        }

        _usedCards = cardBundle.CardData.Except(_usedCards).ToList();
    }

    private void OnRestartButtonClick()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator StartNewLvl()
    {
        foreach (var card in _cards)
        {
            card.SetInteractable(false);
        }

        yield return new WaitForSeconds(0.4f);

        DOTween.KillAll();

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        _usedCards.Clear();

        Fill(_currentLvlIndex);
        _cards = GetComponentsInChildren<Card>();
        _layout.constraintCount = _currentLvl.RowsCount;

        foreach (var card in _cards)
        {
            card.CorrectCard += OnCorrectCard;
        }

        LevelStarted?.Invoke();
    }

    private IEnumerator Restart()
    {
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.DOFade(1f, 1f);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }

    private IEnumerator BounceEffect()
    {
        yield return new WaitForSeconds(0.3f);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(2.7f, 0.2f));
        sequence.Append(transform.DOScale(2f, 0.15f));
        sequence.Append(transform.DOScale(2.2f, 0.15f));
    }
}
