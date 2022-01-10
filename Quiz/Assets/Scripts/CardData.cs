using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardData
{
    [SerializeField] private string _identifier;
    [SerializeField] private Sprite _icon;

    public string Identifier => _identifier;
    public Sprite Icon => _icon;
}
