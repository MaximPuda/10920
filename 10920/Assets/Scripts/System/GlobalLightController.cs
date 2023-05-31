using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightController : Controller
{
    private Animator _animator;
    private PlayerController _player;

    public override void Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();

        _player.LowOxygenEvent += Blink;
    }

    private void Blink(bool active)
    {
        _animator.SetBool("Blink", active);
        Debug.Log("Blink is " + active);
    }

    public override void OnDie()
    {
        base.OnDie();

        _animator.SetBool("Blink", false);
    }

    public override void OnWin()
    {
        base.OnWin();
        
        _animator.SetBool("Blink", false);
    }
}
