using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private UpgradeCard[] _cards;

    public IEnumerator SelectCard()
    {
        var selected = -1;
        for (var i = 0; i < _cards.Length; i++)
        {
            _cards[i].gameObject.SetActive(false);
            _cards[i].OnClick = val => selected = val;
        }
        var upgrades = _playerManager.GetRandomUpgrades(3);
        for (var i = 0; i < upgrades.Length; i++)
        {
            _cards[i].SetUpgrade(upgrades[i]);
            _cards[i].gameObject.SetActive(true);
        }

        while (selected == -1) yield return null;

        for (var i = 0; i < upgrades.Length; i++)
        {
            if (i == selected) continue;
            _playerManager.RemoveUpgrades(upgrades[i]);
        }
        _playerManager.UpdateStats();
    }
}
