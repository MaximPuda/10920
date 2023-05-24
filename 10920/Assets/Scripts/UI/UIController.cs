using UnityEngine;
using TMPro;
using System;

public class UIController : Controller
{ 
    [SerializeField] private UICounter _lifesCounter;
    [SerializeField] private BubbleCounter _oxygenCounter;
    [SerializeField] private UIScreen _hud;
    [SerializeField] private Menu _menu;
    [SerializeField] private UIScreen _die;
    [SerializeField] private UIScreen _win;

    private PlayerController _player;

    private float _maxOxygen;

    public override void Init()
    {
        base.Init();

        _player = FindObjectOfType<PlayerController>();
        _maxOxygen = _player.MaxOxygen;

        _player.LifesChangeEvent += LifesUpdate;
        _player.OxygenChangeEvent += OxygenUpdate;
    }

    public override void OnDisableController()
    {
        base.OnDisableController();

        _player.LifesChangeEvent -= LifesUpdate;
        _player.OxygenChangeEvent -= OxygenUpdate;
    }

    public override void OnMenu()
    {
        base.OnMenu();

        _die.Hide();
        _menu.Show();
    }

    public override void OnRun()
    {
        base.OnRun();

        _hud.Show();
        _menu.Hide();
    }

    public override void OnDie()
    {
        base.OnDie();

        _hud.Hide();
        _die.Show();
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    private void LifesUpdate(int lifes)
    {
        _lifesCounter.SetCounter(lifes);
        _menu.SetLifes(lifes);
    }

    private void OxygenUpdate(float oxygen) 
    {
        _oxygenCounter.SetCounter(oxygen);
        _menu.SetOxygen((int)oxygen);
    }
}
