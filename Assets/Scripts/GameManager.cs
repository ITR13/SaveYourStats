using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager _enemyManager;
    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private SelectManager _selectManager;
    [SerializeField]
    private SelectUpgradeManager _selectUpgradeManager;
    private Coroutine _mainLoop;

    private bool _choosing;

    public void Awake()
    {
        _playerManager.Clear();
        _mainLoop = StartCoroutine(MainLoop());
    }

    private void Update()
    {
        if (_mainLoop == null)
        {
            _mainLoop = StartCoroutine(MainLoop());
        }
    }

    private IEnumerator MainLoop()
    {
        Debug.Log("Started main loop");
        while (!_playerManager.Dead)
        {
            _selectManager.Clear();
            _selectUpgradeManager.gameObject.SetActive(false);
            if (!_choosing)
            {
                yield return StartCoroutine(RpgLoop());
            }
            else
            {
                yield return StartCoroutine(ChooseLoop());
            }
            _choosing = !_choosing;
        }
    }

    #region Rpg
    private IEnumerator RpgLoop()
    {
        _enemyManager.SpawnEnemies(0, 3);
        while (!_enemyManager.RoundOver && !_playerManager.Dead)
        {
            yield return StartCoroutine(PlayerTurn());
            yield return StartCoroutine(_enemyManager.EnemyTurn());
        }
    }

    private IEnumerator PlayerTurn()
    {
        var finished = false;
        while (!finished)
        {
            var selection1 = -1;
            _selectManager.SetText(val => { selection1 = val; Debug.Log(val); }, "Attack", "Skill", "Item");
            while (selection1 == -1) yield return null;

            switch (selection1)
            {
                case 0: // Attack
                    {
                        int? target = null;
                        yield return StartCoroutine(_enemyManager.SelectEnemy(val => target = val));
                        if (target != null)
                        {
                            finished = true;
                            yield return StartCoroutine(_playerManager.Attack(target.Value));
                        }
                    }
                    break;
                case 1: // Skill
                    while (!finished)
                    {
                        var selection2 = -1;
                        _selectManager.SetText(val => selection2 = val, "Breaking Blade", "Swirling Slice", "Drunken Dagger", "Cancel");
                        while (selection2 == -1) yield return null;
                        switch (selection2)
                        {
                            case 0:
                                int? target = null;
                                yield return StartCoroutine(_enemyManager.SelectEnemy(val => target = val));
                                if (target != null)
                                {
                                    finished = true;
                                    yield return StartCoroutine(_playerManager.BreakingBlade(target.Value));
                                }
                                break;
                            case 1:
                                finished = true;
                                yield return StartCoroutine(_playerManager.SwirlingSlice());
                                break;
                            case 2:
                                finished = true;
                                yield return StartCoroutine(_playerManager.DrunkenDagger());
                                break;


                        }
                    }
                    break;
                case 2: // Item
                    break;
            }
        }
    }
    #endregion

    private IEnumerator ChooseLoop()
    {
        _selectUpgradeManager.gameObject.SetActive(true);
        yield return StartCoroutine(_selectUpgradeManager.SelectCard());
    }

}