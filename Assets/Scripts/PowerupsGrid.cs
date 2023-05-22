using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsGrid : MonoBehaviour
{
    private List<UpgradeHover> _hovers = new List<UpgradeHover>();
    [SerializeField]
    private UpgradeHover _prefab;

    public void SetUpgrades(List<Upgrade> upgrades)
    {
        for (var i = 0; i < upgrades.Count; i++)
        {
            if (i >= _hovers.Count)
            {
                _hovers.Add(Instantiate(_prefab, transform));
            }

            _hovers[i].SetUpgrade(upgrades[i]);
        }

        if (upgrades.Count < _hovers.Count)
        {
            for (var i = upgrades.Count; i < _hovers.Count; i++)
            {
                Destroy(_hovers[i].gameObject);
            }
            _hovers.RemoveRange(upgrades.Count, _hovers.Count - upgrades.Count);
        }
    }
}
