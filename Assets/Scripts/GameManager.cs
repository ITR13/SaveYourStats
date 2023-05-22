using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private TextMeshProUGUI _roundText;

    private Coroutine _mainLoop;

    private bool _choosing;
    private int _rounds;

    private int _defenceUp = 5;
    private int _damageUp = 5;

    public void Awake()
    {
        _rounds = 0;
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
            _roundText.text = $"Survived {_rounds} waves";
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

        Debug.Log($"Survived {_rounds} waves");
        PlayerPrefs.SetInt("LastWaves", _rounds);
        if (PlayerPrefs.GetInt("MaxWaves", 0) < _rounds)
        {
            PlayerPrefs.SetInt("MaxWaves", _rounds);
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    #region Rpg
    private IEnumerator RpgLoop()
    {
        _enemyManager.SpawnEnemies(_playerManager.Stats.Difficulty, _playerManager.Stats.EnemyCount);
        while (!_enemyManager.RoundOver && !_playerManager.Dead)
        {
            yield return StartCoroutine(PlayerTurn());
            yield return StartCoroutine(_enemyManager.EnemyTurn());
        }
        if (!_playerManager.Dead)
        {
            _playerManager.EndRound();
        }
        _rounds++;
    }

    private IEnumerator PlayerTurn()
    {
        var finished = false;
        while (!finished)
        {
            var selection1 = -1;
            _selectManager.SetText(val => selection1 = val, "Attack", "Skill", "Item");
            while (selection1 == -1) yield return null;
            _selectManager.Clear();

            switch (selection1)
            {
                case 0: // Attack
                    {
                        var target = -1;
                        yield return StartCoroutine(_enemyManager.SelectEnemy(val => target = val));
                        if (target != -1)
                        {
                            finished = true;
                            yield return StartCoroutine(_playerManager.Attack(target));
                        }
                    }
                    break;
                case 1: // Skill
                    while (!finished)
                    {
                        var selection2 = -1;
                        _selectManager.SetText(val => selection2 = val, "Breaking Blade", "Swirling Slice", "Drunken Dagger", "Cancel");
                        while (selection2 == -1) yield return null;
                        _selectManager.Clear();
                        switch (selection2)
                        {
                            case 0:
                                var target = -1;
                                yield return StartCoroutine(_enemyManager.SelectEnemy(val => target = val));
                                if (target != -1)
                                {
                                    finished = true;
                                    yield return StartCoroutine(_playerManager.BreakingBlade(target));
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
                        if (selection1 == 3) break;
                    }
                    break;
                case 2: // Item
                    var exit = false;
                    while (!finished && !exit)
                    {
                        var selection2 = -1;
                        _selectManager.SetText(val => selection2 = val, $"Damage Up ({_damageUp})", $"Defence Up ({_defenceUp})", "Cancel");
                        while (selection2 == -1) yield return null;
                        _selectManager.Clear();
                        switch (selection2)
                        {
                            case 0:
                                if (_damageUp <= 0) break;
                                finished = true;
                                _damageUp--;
                                _playerManager.DamageUp();
                                yield return new WaitForSeconds(1f);
                                break;
                            case 1:
                                if (_defenceUp <= 0) break;
                                finished = true;
                                _defenceUp--;
                                _playerManager.DefenceUp();
                                yield return new WaitForSeconds(1f);
                                break;
                            case 2:
                                exit = true;
                                break;
                        }
                    }
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