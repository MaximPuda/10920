using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyPosition = 10;
    [SerializeField] private bool _destoyOnCollision = true;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.velocity = Vector3.up * _speed;
    }

    private void Update()
    {
        if (transform.position.y > _destroyPosition)
            Destroy(gameObject);

    }

    public void Die()
    {
        if (_destoyOnCollision)
            Destroy(gameObject);
    }
}
