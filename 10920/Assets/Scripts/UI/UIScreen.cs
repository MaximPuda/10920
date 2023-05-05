using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIScreen : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void Show()
    {
        _animator.SetBool("Show", true);
    }

    public virtual void Hide()
    {
        _animator.SetBool("Show", false);
    }
}
