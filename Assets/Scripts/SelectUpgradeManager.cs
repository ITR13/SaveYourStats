using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeCard[] _cards;
    public IEnumerator SelectCard()
    {
        var selected = -1;
        for (var i = 0; i < _cards.Length; i++)
        {
            _cards[i].OnClick = val => selected = val;
        }
        while (selected == -1) yield return null;
    }
}
