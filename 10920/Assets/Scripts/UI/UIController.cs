using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private UICounter _lifesCounter;

    private PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        _player.LifesChangeEvent += LifesUpdate;
    }

    private void OnDisable()
    {
        _player.LifesChangeEvent -= LifesUpdate;
    }

    private void LifesUpdate(int lifes)
    {
        _lifesCounter.SetCounter(lifes);
    }
}
