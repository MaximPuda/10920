using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dieFx;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            Die();
    }

    private void Die()
    {
        _renderer.enabled = false;
        _dieFx.Play();

        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_dieFx.main.duration + 1);
        Destroy(gameObject);
    }
}
