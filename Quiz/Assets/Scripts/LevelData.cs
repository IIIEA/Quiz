using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField] private CardBundleData[] _dataSet;
    [SerializeField] private int _rowsCount;
    [SerializeField] private int _columnsCount;

    public CardBundleData[] DataSet => _dataSet;
    public int RowsCount => _rowsCount;
    public int ColumnsCount => _columnsCount;

    public CardBundleData GetRandomDataSet()
    {
        int randomIndex = Random.Range(0, _dataSet.Length);

        return _dataSet[randomIndex];
    }
}
