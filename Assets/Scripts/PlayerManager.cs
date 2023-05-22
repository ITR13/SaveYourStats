using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private PowerupsGrid _upgradeGrid;

    [SerializeField] private StatBar _hpBar, _manaBar;

    [SerializeField] private Upgrade[] _baseUpgrades;

    [SerializeField] private RectTransform _dmgParent;
    [SerializeField] private RiseAndGone _dmgPrefab;

    private List<Upgrade> _activeUpgrades = new List<Upgrade>();
    private List<Upgrade> _selectableUpgrades = new List<Upgrade>();
    public Stats Stats { get; private set; }
    private int _health, _mana, _damage, _defence;
    private int _defenceUp, _damageUp;

    public bool Dead => _health <= 0;

    public void Clear()
    {
        _activeUpgrades.Clear();
        _selectableUpgrades.Clear();

        _selectableUpgrades.AddRange(_baseUpgrades);

        _health = int.MaxValue;
        _mana = int.MaxValue;
        _damage = int.MaxValue;
        _defence = int.MaxValue;
        UpdateStats();
    }

    public void UpdateStats()
    {
        Stats = new Stats();
        _activeUpgrades.Clear();
        _activeUpgrades.AddRange(_selectableUpgrades);
        for (var i = 0; i < _activeUpgrades.Count; i++)
        {
            var upgrade = _activeUpgrades[i];

            upgrade.Execute(Stats);
            _activeUpgrades.AddRange(upgrade.Dependencies);

            if (_activeUpgrades.Count > 1000)
            {
                Debug.LogError($"Too many upgrades, possible recursive dependency");
                break;
            }
        }
        _activeUpgrades.Sort((a, b) => a.name.CompareTo(b.name));

        if (_health > Stats.MaxHp)
        {
            _health = Stats.MaxHp;
        }
        if (_mana > Stats.MaxMp)
        {
            _mana = Stats.MaxMp;
        }

        _hpBar.Clear(_health, Stats.MaxHp);
        _manaBar.Clear(_mana, Stats.MaxMp);
        _damage = Stats.Damage;
        _defence = Stats.Defence;

        _upgradeGrid.SetUpgrades(_activeUpgrades);
    }

    public void EndRound()
    {
        _damage = Stats.Damage;
    }

    public IEnumerator DamagePlayer(int damage)
    {
        var org = damage;
        damage = Mathf.Clamp(damage - _defence, 0, _health);
        _health -= damage;

        Debug.Log($"Player took {damage} ({org} - {_defence}) down to {_health} health");
        _hpBar.Target(_health);

        var dmg = Instantiate(_dmgPrefab, _dmgParent);
        dmg.StartCoroutine(dmg.Play($"{damage}"));

        yield return new WaitForSeconds(0.8f);

        if (_health <= 0) gameObject.SetActive(false);
    }

    public IEnumerator Attack(int enemy)
    {
        yield return StartCoroutine(_enemyManager.DamageEnemy(enemy, _damage));
    }

    public IEnumerator BreakingBlade(int enemy)
    {
        var cost = 5;
        var damage = Mathf.FloorToInt(_damage * Stats.HitSingleMultiplier);
        if (_mana > cost)
        {
            _mana -= 5;
            _manaBar.Target(_mana);
            yield return StartCoroutine(_enemyManager.DamageEnemy(enemy, damage));
        }
        else
        {
            yield return StartCoroutine(_enemyManager.DamageEnemy(enemy, 1));
        }
    }

    public IEnumerator SwirlingSlice()
    {
        var cost = Stats.HitAllCost;
        var damage = Mathf.FloorToInt(_damage * Stats.HitAllMultiplier);
        if (_mana > cost)
        {
            _mana -= 5;
            _manaBar.Target(_mana);
            yield return StartCoroutine(_enemyManager.DamageAllEnemies(damage));
        }
        else
        {
            yield return StartCoroutine(_enemyManager.DamageAllEnemies(1));
        }
    }

    public IEnumerator DrunkenDagger()
    {
        var cost = 5;
        var targets = Stats.HitRandomCount;
        var damage = Mathf.FloorToInt(_damage * Stats.HitRandomMultiplier);
        if (_mana > cost)
        {
            _mana -= 5;
            _manaBar.Target(_mana);
            for (var i = 0; i < targets; i++)
            {
                var target = _enemyManager.RandomTarget();
                yield return StartCoroutine(_enemyManager.DamageEnemy(target, damage));
            }
        }
        else
        {
            for (var i = 0; i < targets; i++)
            {
                var target = _enemyManager.RandomTarget();
                yield return StartCoroutine(_enemyManager.DamageEnemy(target, 1));
            }
        }
    }

    public Upgrade[] GetRandomUpgrades(int count)
    {
        var chosen = new List<Upgrade>();
        var indexes = new HashSet<int>();
        for (var i = 0; i < count && indexes.Count < _selectableUpgrades.Count; i++)
        {
            while (true)
            {
                var r = UnityEngine.Random.Range(0, _selectableUpgrades.Count);
                if (indexes.Contains(r)) continue;
                indexes.Add(r);
                chosen.Add(_selectableUpgrades[r]);
                break;
            }
        }

        return chosen.ToArray();
    }

    public void RemoveUpgrades(Upgrade upgrade)
    {
        if (!_activeUpgrades.Remove(upgrade))
        {
            return;
        }
        _selectableUpgrades.Remove(upgrade);
        _selectableUpgrades.AddRange(upgrade.Dependencies);
    }

    internal void DamageUp()
    {
        _damage *= 2;
        var dmg = Instantiate(_dmgPrefab, _dmgParent);
        dmg.StartCoroutine(dmg.Play($"Dmg {_damage}"));
    }

    internal void DefenceUp()
    {
        _defence *= 2;
        var dmg = Instantiate(_dmgPrefab, _dmgParent);
        dmg.StartCoroutine(dmg.Play($"Def {_defence}"));
    }
}
