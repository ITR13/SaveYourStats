using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public int Damage;

    public Action OnClick { private get; set; }

    [SerializeField]
    private GameObject _highlight;

    public void OnMouseDown()
    {
        OnClick?.Invoke();
    }

    public void OnMouseOver()
    {
        _highlight.SetActive(true);
    }

    public void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
