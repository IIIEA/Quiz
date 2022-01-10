using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Image _icon;
    private string _identifier;

    private void Awake()
    {
        _icon = GetComponentInChildren<Image>();
    }

    public void Init(Sprite icon, string identifier)
    {
        _icon.sprite = icon;
        _identifier = identifier;
    }
}
