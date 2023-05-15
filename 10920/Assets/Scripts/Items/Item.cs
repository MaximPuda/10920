using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
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
            InteractWithPlayer(player);
    }

    public virtual void InteractWithPlayer(PlayerController player) { }

    protected void Die()
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
