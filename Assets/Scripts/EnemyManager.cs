using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager _player;
    [SerializeField]
    private SelectManager _select;

    [SerializeField]
    private Transform[] _spawningPositions;
    [SerializeField]
    private Transform[] _dmgPositions;

    [SerializeField]
    private Enemy[] _enemyPrefabs;

    [SerializeField]
    private RiseAndGone _damagePrefab;

    public bool RoundOver => _enemies.Count <= 0;

    private List<(Enemy, Transform)> _enemies = new List<(Enemy, Transform)>();

    private int _lastClickedEnemy;

    public void Clear()
    {
        _enemies.Clear();
        for (var i = 0; i < _spawningPositions.Length; i++)
        {
            if (_spawningPositions[i].childCount > 0)
            {
                Destroy(_spawningPositions[i].GetChild(0).gameObject);
            }
        }
    }

    public void SpawnEnemies(int difficulty, int count)
    {
        Clear();
        difficulty = Mathf.Clamp(difficulty, 0, _enemyPrefabs.Length - 1);
        count = Mathf.Clamp(count, 1, _spawningPositions.Length - 1);

        for (var i = 0; i < count; i++)
        {
            var index = i;
            var enemy = Instantiate(_enemyPrefabs[difficulty], _spawningPositions[i]);
            _enemies.Add((enemy, _dmgPositions[i]));
            _enemies[^1].Item1.OnClick = () => _lastClickedEnemy = index;
        }
    }

    public IEnumerator DamageAllEnemies(int damage)
    {
        var originalCount = _enemies.Count;
        for (var i = 0; i < originalCount; i++)
        {
            yield return StartCoroutine(DamageEnemy(i - originalCount + _enemies.Count, damage));
        }
    }

    public IEnumerator DamageEnemy(int index, int damage)
    {
        if (_enemies.Count <= 0) yield break;
        index = Mathf.Clamp(index, 0, _enemies.Count - 1);

        var enemy = _enemies[index].Item1;
        damage = Mathf.Clamp(damage, 0, enemy.Health);
        enemy.Health -= damage;
        Debug.Log($"{enemy.transform.parent.gameObject.name} took {damage} damage down to {enemy.Health} health");

        var dmg = Instantiate(_damagePrefab, _enemies[index].Item2);
        dmg.StartCoroutine(dmg.Play($"{damage}"));
        yield return new WaitForSeconds(0.4f);

        if (enemy.Health > 0)
        {
            yield break;
        }
        enemy.gameObject.SetActive(false);
        _enemies.RemoveAt(index);
    }

    public int RandomTarget()
    {
        if (_enemies.Count <= 1) return 0;
        return UnityEngine.Random.Range(0, _enemies.Count);
    }

    public IEnumerator EnemyTurn()
    {
        for (var i = 0; i < _enemies.Count; i++)
        {
            var enemy = _enemies[i];
            yield return StartCoroutine(_player.DamagePlayer(enemy.Item1.Damage));
        }
    }

    public IEnumerator SelectEnemy(Action<int> target)
    {
        var cancel = false;
        _select.SetText(_ => cancel = true, "Cancel");
        _lastClickedEnemy = -1;

        while (_lastClickedEnemy == -1 && !cancel)
        {
            yield return null;
        }
        target?.Invoke(_lastClickedEnemy);
    }
}
