
using UnityEngine;
using System.Collections;
using System;

public class DeepBomb : MonoBehaviour
{
    [SerializeField] private float _force = 5000f;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private GameObject _veiw;
    [SerializeField] private GameObject _outline;
    [SerializeField] private ParticleSystem _particles;

    private Collider2D _collider;
    private DeepBombTriger _trigger;
    private Animator _animator;
    private PlayerController _player;
    
    private float _timer;
    private bool _disable;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _trigger = GetComponentInChildren<DeepBombTriger>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _trigger.TriggerEnterEvent += Activate;
    }

    private void OnDisable()
    {
        _trigger.TriggerEnterEvent -= Activate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.TryGetComponent<PlayerController>(out PlayerController player);

        if (player != null)
        {
            _player = player;
            BlowUp();
        }
    }

    private void Activate(PlayerController player)
    {
        _player = player;
        _animator.SetTrigger("Active");
    }

    public void BlowUp()
    {
        if (_disable)
            return;

        _disable = true;

        Vector2 delta = _player.transform.position - transform.position;
        float x = delta.x > 0 ? 1f : -1f;
        Vector2 dir = new Vector2(x, 0f);
        Vector2 force = dir * _force;
        
        Die();

        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, _trigger.Radius);
        foreach (Collider2D collision in overlaps)
        { 
            if(collision.TryGetComponent<PlayerController>(out PlayerController player))
                player.ApplyForce(force);
        }
    }

    public void Die()
    {
        _collider.enabled = false;

        if (_veiw != null)
            _veiw.SetActive(false);

        if (_outline != null)
            _outline.SetActive(false);
        
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
