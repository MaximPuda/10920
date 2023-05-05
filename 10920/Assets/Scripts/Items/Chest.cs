using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private int _cost = 25;
    [SerializeField] private float _damageForce;
    [SerializeField] private ParticleSystem _openFX;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if (collision.relativeVelocity.magnitude > _damageForce)
            {
                player.AddCoins(_cost);
                Open();
            }
            else Shake();
        }
    }

    private void Open()
    {
        _animator.SetBool("Open", true);
        _openFX.Play();
    }

    private void Shake()
    {
        _animator.SetTrigger("Shake");
    }
}
