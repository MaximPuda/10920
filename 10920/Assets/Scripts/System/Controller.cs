using UnityEngine;

public class Controller : MonoBehaviour
{
    private GameManager _gm;

    private void OnDisable()
    {
        _gm.MenuEvent -= OnMenu;
        _gm.RunEvent -= OnRun;
        _gm.DieEvent -= OnDie;
        _gm.WinEvent -= OnWin;
        _gm.PauseEvent -= OnPause;

        OnDisableController();
    }

    public virtual void Init() 
    {
        _gm = GameManager.Instance;

        _gm.MenuEvent += OnMenu;
        _gm.RunEvent += OnRun;
        _gm.DieEvent += OnDie;
        _gm.WinEvent += OnWin;
        _gm.PauseEvent += OnPause;
    }

    public virtual void OnEnableController() { }

    public virtual void OnDisableController() { }

    public virtual void OnMenu() { }

    public virtual void OnRun() { }

    public virtual void OnDie() { }

    public virtual void OnWin() { }

    public virtual void OnPause() { }
}
