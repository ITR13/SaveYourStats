using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _index;
    public Action<int> OnClick { private get; set; }

    private void Awake()
    {
        _button.onClick.AddListener(Click);
    }

    public void Click()
    {
        OnClick?.Invoke(_index);
    }
}
