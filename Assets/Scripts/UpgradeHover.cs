using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _overlay;

    [SerializeField] private TextMeshProUGUI _text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _overlay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _overlay.SetActive(false);
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        _icon.sprite = upgrade.Icon;
        _overlay.SetActive(false);
        _text.text = upgrade.name;
    }
}
