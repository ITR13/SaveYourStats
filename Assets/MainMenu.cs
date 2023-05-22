using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AssetReference _gameScene;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private TextMeshProUGUI _score, _highscore;
    private AsyncOperationHandle<SceneInstance> _sceneOp;
    [SerializeField]
    private GameObject _loading, _mainMenu;

    private static bool played;

    private void Awake()
    {
        _loading.SetActive(false);
        _mainMenu.SetActive(true);
    }

    private void Start()
    {
        _sceneOp = _gameScene.LoadSceneAsync(LoadSceneMode.Single, false);
        var score = PlayerPrefs.GetInt("LastWaves", 0);
        var hs = PlayerPrefs.GetInt("MaxWaves", 0);

        if (hs >= 0)
        {
            _highscore.text = $"<b>Highscore:</b> {hs}";
        }
        if (played)
        {
            _score.text = $"<b>Score:</b> {score}";
        }
        played = true;
    }

    public void Play()
    {
        _loading.SetActive(true);
        _mainMenu.SetActive(false);
        StartCoroutine(DoPlay());
    }

    private IEnumerator DoPlay()
    {
        while (!_sceneOp.IsDone)
        {
            _loadingBar.fillAmount = _sceneOp.PercentComplete / 2;
            yield return null;
        }
        var lastOp = _sceneOp.Result.ActivateAsync();
        while (!lastOp.isDone)
        {
            _loadingBar.fillAmount = 0.5f + lastOp.progress / 2;
            yield return null;
        }
    }
}
