using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RiseAndGone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _speed, _time;

    public IEnumerator Play(string text)
    {
        _textMesh.text = text;
        var color = Color.black;
        _textMesh.color = color;

        for (var t = 0f; t < _time; t += Time.deltaTime)
        {
            color.a = Mathf.Clamp01((_time - t) * 3);
            _textMesh.color = color;
            yield return null;
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }

        Destroy(gameObject);
    }
}
