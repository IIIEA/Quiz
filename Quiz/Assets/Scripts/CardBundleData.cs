using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Bundle Data", order = 51)]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private List<CardData> _cardData;

    public List<CardData> CardData => _cardData;
}
