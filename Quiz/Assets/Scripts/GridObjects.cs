using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridObjects : MonoBehaviour
{
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private Card _card;

    private GridLayoutGroup _layout;
    private LevelData _currentLvl;

    private void Awake()
    {
        _layout = GetComponent<GridLayoutGroup>();

        _layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(BounceEffect());
    }

    private void OnEnable()
    {
        Fill(1);
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        StopCoroutine(BounceEffect());
    }

    private void Fill(int lvlIndex)
    {
        if (_levels.Length < lvlIndex)
            return;

        _currentLvl = _levels[lvlIndex];
        _layout.constraintCount = _currentLvl.RowsCount;

        var dataSet = _currentLvl.GetRandomDataSet();

        for (int i = 0; i < (_currentLvl.RowsCount * _currentLvl.ColumnsCount); i++)
        {
            var cardData = dataSet.GetRandomCardData();
            var card = Instantiate(_card, transform);

            card.Init(cardData.Icon, cardData.Identifier);
        }
    }

    IEnumerator BounceEffect()
    {
        yield return new WaitForSeconds(0.3f);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(2.7f, 0.2f));
        sequence.Append(transform.DOScale(2f, 0.15f));
        sequence.Append(transform.DOScale(2.5f, 0.15f));
    }
}
