using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _index;

    [SerializeField] private TextMeshProUGUI _title, _description;
    [SerializeField] private Image _image;

    public Action<int> OnClick { private get; set; }

    private void Awake()
    {
        _button.onClick.AddListener(Click);
    }

    public void Click()
    {
        OnClick?.Invoke(_index);
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        _title.text = upgrade.Text;
        _description.text = upgrade.Description;
        _image.sprite = upgrade.Image;
    }
}
