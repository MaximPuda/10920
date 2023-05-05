using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _destroyOnCollision;
    [SerializeField] private GameObject _veiw;
    [SerializeField] private ParticleSystem _particles;

    private Collider2D _collider;

    private void Awake()
    { 
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.TryGetComponent<PlayerController>(out PlayerController player);

        if (player != null)
        {
            player.ApplyDamage();

            if (_destroyOnCollision)
                Die();
        }
    }

    public void Die()
    {
        _collider.enabled = false;
        
        if(_veiw != null)
            _veiw.SetActive(false);
        
        if (_particles != null)
            _particles.Play();

        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_particles.main.duration);
        Destroy(gameObject);
    }
}
