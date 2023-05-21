using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _text;

    private float _value;
    private int _target, _max;
    private bool _dirty;

    public void Clear(int value, int maxValue)
    {
        _value = _target = value;
        _max = maxValue;
        _bar.fillAmount = value / maxValue;
        _dirty = false;
    }

    public void Target(int target)
    {
        _target = target;
        _dirty = true;
    }

    private void Update()
    {
        if (!_dirty) return;
        _value = Mathf.MoveTowards(_value, _target, Time.deltaTime * 100);
        _bar.fillAmount = _value / _max;
        _text.text = Mathf.FloorToInt(_value).ToString(CultureInfo.InvariantCulture);

        _dirty &= _value != _target;
    }
}
