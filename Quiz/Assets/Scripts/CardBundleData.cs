using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Bundle Data", order = 51)]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private CardData[] _cardData;

    public CardData[] CardData => _cardData;

    public CardData GetRandomCardData()
    {
        int randomIndex = Random.Range(0, _cardData.Length);

        return _cardData[randomIndex]; 
    }
}
