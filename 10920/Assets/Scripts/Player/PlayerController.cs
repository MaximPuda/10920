using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : Controller
{
    private const float ANGULAR_DRAG_IN_MOVING = 10;

    [Header("MOVE")]
    [SerializeField] private Transform _mover;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _horizontalSpeed;
    [Space(5)]

    [Header("DATA")]
    [SerializeField] private int _startLifes;
    [SerializeField] private float _maxOxygen;
    [SerializeField] private float _oxygenRate;
    [Space(5)]

    [Header("FX")]
    [SerializeField] private GameObject _flashesFX;

    public float MaxOxygen => _maxOxygen;

    public event UnityAction<int> LifesChangeEvent;
    public event UnityAction<float> OxygenChangeEvent;
    public event UnityAction HaveDamageEvent;

    private PlayerInput _input;
    private Rigidbody2D _rb;
    private float _angularDragStandart;
    private Vector2 _direction;

    private int _currentLifes;
    private int _currentCoins;
    private float _currentOxygen;

    private bool _blocked;

    public override void Init()
    {
        base.Init();

        _input = GetComponent<PlayerInput>();
        _input.TouchInputEvent += SetDirection;

        _rb = GetComponent<Rigidbody2D>();

        _angularDragStandart = _rb.angularDrag;
        _currentLifes = _startLifes;
        _currentCoins = 0;
        _blocked = false;
        _currentOxygen = _maxOxygen;
    }

    public override void OnDisableController()
    {
        base.OnDisableController();

        _input.TouchInputEvent -= SetDirection;
    }

    public override void OnMenu()
    {
        base.OnMenu();

        _blocked = true;
    }

    public override void OnRun()
    {
        base.OnRun();

        _blocked = false;
    }

    public override void OnDie()
    {
        base.OnDie();

        _blocked = true;
    }

    private void Update()
    {
        //ApplyOxygen();    
    }

    private void FixedUpdate()
    {
        if (!_blocked)
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
        }
        else
        {
            _rb.angularDrag = _angularDragStandart;
        }    
    }

    private void ApplyOxygen()
    {
        float used = _oxygenRate * Time.deltaTime;
        _currentOxygen -= used;

        OxygenChangeEvent?.Invoke(_currentOxygen);

        if (_currentOxygen <= 0)
            Die();
    }

    public void ApplyDamage()
    {
        _currentLifes--;

        LifesChangeEvent?.Invoke(_currentLifes);
        HaveDamageEvent?.Invoke();

        if (_currentLifes <= 0)
        {
            Die();
        }
    }

    public void ApplyForce(Vector2 force)
    {
        Block(0.5f, false);
        _rb.AddForce(force);
    }

    public void Block(float seconds, bool fx)
    {
        StartCoroutine(BlockCoroutine(seconds, fx));
    }

    public void AddCoins(int coins) => _currentCoins += coins;

    public void AddOxygen(float oxygen)
    { 
        _currentOxygen += oxygen;

        if (_currentOxygen >= _maxOxygen)
            _currentOxygen = _maxOxygen;

        OxygenChangeEvent?.Invoke(_currentOxygen);
    }

    private void Die()
    {
        GameManager.Instance.Die();
    }

    private IEnumerator BlockCoroutine(float seconds, bool fx)
    {
        if (fx)
            _flashesFX.SetActive(true);
     
        _blocked = true;

        yield return new WaitForSeconds(seconds);

        _blocked = false;
        _flashesFX.SetActive(false);
    }
}
