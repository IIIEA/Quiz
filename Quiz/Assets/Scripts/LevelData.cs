using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<CardBundleData> _cardBundleData;
    [SerializeField] private int _rowsCount;
    [SerializeField] private int _columnsCount;

    public int RowsCount => _rowsCount;
    public int ColumnsCount => _columnsCount;

    public CardBundleData GetRandomCardBundle()
    {
        int randomIndex = Random.Range(0, _cardBundleData.Count);

        return _cardBundleData[randomIndex];
    }
}
