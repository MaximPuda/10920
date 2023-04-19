using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private const float ANGULAR_DRAG_IN_MOVING = 10;

    [Header("MOVE")]
    [SerializeField] private Transform _mover;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _horizontalSpeed;
    [Space(5)]

    [Header("DATA")]
    [SerializeField] private int _startLifes;
    [SerializeField] private int _oxygen;
    [Space(5)]

    [Header("FX")]
    [SerializeField] private ParticleSystem _bulb;

    public event UnityAction<int> LifesChangeEvent;
    public event UnityAction HaveDamageEvent;

    private PlayerInput _input;
    private Rigidbody2D _rb;
    private float _angularDragStandart;
    private Vector2 _direction;

    private int _currrentLifes;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _angularDragStandart = _rb.angularDrag;
        _currrentLifes = _startLifes;
    }

    private void OnEnable()
    {
        _input.TouchInputEvent += SetDirection;
    }

    private void OnDisable()
    {
        _input.TouchInputEvent -= SetDirection;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction == null)
            _direction = Vector2.zero;
        else
            _direction = direction;
    }

    private void Move()
    {
        _rb.AddForce(_direction * _horizontalSpeed);

        Vector2 _moverPos = new Vector2(0, _direction.y);
        if (_moverPos.magnitude > 0.01f)
        {
            _rb.angularDrag = ANGULAR_DRAG_IN_MOVING;
            _mover.Translate(_moverPos * _verticalSpeed * Time.deltaTime);
            _bulb.Play();
        }
        else
        {
            _rb.angularDrag = _angularDragStandart;
            _bulb.Stop();
        }    
    }

    public void ApplyDamage()
    {
        _currrentLifes--;

        LifesChangeEvent?.Invoke(_currrentLifes);
        HaveDamageEvent?.Invoke();

        if (_currrentLifes <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }
}
