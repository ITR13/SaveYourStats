using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxMana;
    [SerializeField] private int _baseDamage;

    [SerializeField] private StatBar _hpBar, _manaBar;

    private int _health, _mana, _damage;

    public bool Dead => _health <= 0;

    public void Clear()
    {
        _hpBar.Clear(_maxHealth, _maxHealth);
        _manaBar.Clear(_maxMana, _maxMana);

        _health = _maxHealth;
        _mana = _maxMana;
        _damage = _baseDamage;
    }

    public IEnumerator DamagePlayer(int damage)
    {
        damage = Mathf.Clamp(damage, 0, _health);
        _health -= damage;

        Debug.Log($"Player took {damage} down to {_health} health");
        _hpBar.Target(_health);

        // TODO: GAMEOVER
        yield break;
    }

    public IEnumerator Attack(int enemy)
    {
        yield return StartCoroutine(_enemyManager.DamageEnemy(enemy, _damage));
    }

    public IEnumerator BreakingBlade(int enemy)
    {
        var cost = 5;
        if (_mana > cost)
        {
            _mana -= 5;
            yield return StartCoroutine(_enemyManager.DamageEnemy(enemy, _damage * 3));
        }
        else
        {
            yield return StartCoroutine(_enemyManager.DamageEnemy(enemy, 1));
        }
    }

    public IEnumerator SwirlingSlice()
    {
        var cost = 5;
        if (_mana > cost)
        {
            _mana -= 5;
            yield return StartCoroutine(_enemyManager.DamageAllEnemies(_damage));
        }
        else
        {
            yield return StartCoroutine(_enemyManager.DamageAllEnemies(1));
        }
    }

    public IEnumerator DrunkenDagger()
    {
        var cost = 10;
        var targets = 2;
        if (_mana > cost)
        {
            _mana -= 5;
            for (var i = 0; i < targets; i++)
            {
                var target = _enemyManager.RandomTarget();
                yield return StartCoroutine(_enemyManager.DamageEnemy(target, _damage));
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
}
