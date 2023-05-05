using System;
using UnityEngine;

public class Unagi : MonoBehaviour
{
    [SerializeField] private float _secondsBlock = 2f;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Transform _directionPoint;
    [SerializeField] private ParticleSystem _particles;

    private Vector2 _direction;
    private UnagiTrigger _trigger;

    private bool _isActive;

    private void Awake()
    {
        _direction = (_directionPoint.position - transform.position).normalized;
        _trigger = GetComponentInChildren<UnagiTrigger>();
    }

    private void OnEnable()
    {
        _trigger.TriggerEvent += Activate;
    }

    private void OnDisable()
    {
        _trigger.TriggerEvent -= Activate;
    }

    private void Update()
    {
        if (_isActive)
            Move();

        if (transform.position.x <= -10 || 
            transform.position.x >= 10f ||
            transform.position.y >= 7f)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.TryGetComponent<PlayerController>(out PlayerController player);

        if (player != null)
        {
            _particles.Play();
            player.Block(_secondsBlock, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.TryGetComponent<PlayerController>(out PlayerController player);

        if (player != null)
        {
            _particles.Stop();
        }
    }

    private void Activate() => _isActive = true;

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
