using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Controller
{
    [SerializeField] private float _runtSize = 8;
    [SerializeField] private Vector3 _runPosition;
    [SerializeField] private float _menuSize = 4;
    [SerializeField] private Vector3 _menuPosition;


    [SerializeField] private float _magnitude;
    [SerializeField] private float _duration;

    [SerializeField] private PlayerController _player;

    private Animator _animator;

    public override void Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();

        _player.HaveDamageEvent += Shake;
    }

    public override void OnMenu()
    {
        base.OnMenu();

        _animator.SetBool("Run", false);

    }

    public override void OnRun()
    {
        base.OnRun();

        _animator.SetBool("Run", true);
    }

    private void OnDisable()
    {
        _player.HaveDamageEvent -= Shake;
    }

    public void Shake()
    {
        _animator.SetTrigger("Shake");
        //StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.localPosition;

        float time = 0;

        while (time < _duration) 
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            var newPos = new Vector3(x, originalPos.y, originalPos.z);
            transform.localPosition = newPos;

            time += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
