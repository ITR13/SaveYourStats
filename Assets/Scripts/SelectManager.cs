using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private Button[] _courners;
    [SerializeField]
    private Button _center;


    public void Clear()
    {
        for (var i = 0; i < _courners.Length; i++)
        {
            _courners[i].onClick.RemoveAllListeners();
            _courners[i].gameObject.SetActive(false);
        }
        _center.onClick.RemoveAllListeners();
        _center.gameObject.SetActive(false);
    }

    public void SetText(Action<int> onSelect, params string[] buttonTexts)
    {
        Clear();
        var buttons = new List<Button>();
        for (var i = 0; i < buttonTexts.Length; i++)
        {
            buttons.Add(_courners[i]);
        }
        if (buttonTexts.Length % 2 == 1)
        {
            buttons[^1] = _center;
        }

        for (var i = 0; i < buttons.Count; i++)
        {
            var index = i;
            buttons[i].gameObject.SetActive(true);
            buttons[i].onClick.AddListener(() => onSelect.Invoke(index));
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = buttonTexts[i];
        }
    }
}
