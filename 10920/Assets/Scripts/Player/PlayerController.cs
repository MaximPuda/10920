using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : Controller
{
    private const float ANGULAR_DRAG_IN_MOVING = 10;

    [Header("MOVE")]
    [SerializeField] private Transform _mover;
    [SerializeField] private HingeJoint2D _hingeJoint;
    //[SerializeField] private float _verticalSpeed;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _changeSphereForce;
    [Space(5)]

    [Header("DATA")]
    //[SerializeField] private int _startLifes;
    //[SerializeField] private float _maxOxygen;
    [SerializeField] private float _oxygenRate;
    [Space(5)]

    [Header("FX")]
    [SerializeField] private GameObject _flashesFX;
    [SerializeField] private ParticleSystem _bublesFX;

    public float MaxOxygen => _activeSphere.MaxOxygen ;

    public event UnityAction<int> LifesChangeEvent;
    public event UnityAction<float> OxygenChangeEvent;
    public event UnityAction HaveDamageEvent;

    private PlayerInput _input;
    private Rigidbody2D _rb;
    private Animation _anim;
    private DistanceJoint2D _distanceJoint;

    private float _angularDragStandart;
    private Vector2 _direction;
    private Batisphere[] _spheres;
    private Batisphere _activeSphere;
    private int _activeSphereIndex;

    private int _currentLifes;
    private int _currentCoins;
    private float _currentOxygen;

    private bool _isRunnig;
    private bool _isBlocked;

    public override void Init()
    {
        base.Init();

        _input = GetComponent<PlayerInput>();
        _input.TouchInputEvent += SetDirection;

        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animation>();
        _distanceJoint = GetComponent<DistanceJoint2D>();

        _angularDragStandart = _rb.angularDrag;
        _currentCoins = 0;
        _isBlocked = false;

        _spheres = GetComponentsInChildren<Batisphere>();
        
        if (_spheres == null)
        {
            Debug.LogWarning("Batisphere is not assigned to the player!");
            return;
        }

        if(_spheres.Length > 1)
            foreach (var sphere in _spheres)
                sphere.gameObject.SetActive(false);

        _activeSphere = _spheres[0];
        _activeSphere.gameObject.SetActive(true);
        _activeSphereIndex = 0;

        ResetStartVelues();
    }

    public override void OnDisableController()
    {
        base.OnDisableController();

        _input.TouchInputEvent -= SetDirection;
    }

    private void ResetStartVelues()
    {
        _currentLifes = _activeSphere.StartLifes;
        _currentOxygen = _activeSphere.MaxOxygen;

        LifesChangeEvent?.Invoke(_currentLifes);
        OxygenChangeEvent?.Invoke(_currentOxygen);
    }

    public override void OnMenu()
    {
        base.OnMenu();

        _distanceJoint.enabled = false;

        LifesChangeEvent?.Invoke(_currentLifes);
        OxygenChangeEvent?.Invoke(_currentOxygen);

        _isBlocked = true;
        _isRunnig = false;
    }

    public override void OnRun()
    {
        base.OnRun();

        _distanceJoint.enabled = true;

        LifesChangeEvent?.Invoke(_currentLifes);
        OxygenChangeEvent?.Invoke(_currentOxygen);

        _isBlocked = false;
        _isRunnig = true;
    }

    public override void OnDie()
    {
        base.OnDie();
        
        _distanceJoint.enabled = false;
        _hingeJoint.enabled = false;

        _isBlocked = true;
        _isRunnig = false;
    }

    private void Update()
    {
        if (_isRunnig)
            ApplyOxygen();    
    }

    private void FixedUpdate()
    {
        if (!_isBlocked)
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
        if (!_isRunnig)
            return;

        _rb.AddForce(_direction * _horizontalSpeed);

        Vector2 _moverPos = new Vector2(0, _direction.y);
        if (_moverPos.magnitude > 0.01f)
        {
            _rb.angularDrag = ANGULAR_DRAG_IN_MOVING;
            _mover.Translate(_moverPos * _activeSphere.VerticalSpeed * Time.deltaTime);
        }
        else
        {
            _rb.angularDrag = _angularDragStandart;
        }    
    }

    private void ApplyOxygen()
    {
        if (!_isRunnig)
            return;

        float used = _oxygenRate * Time.deltaTime;
        _currentOxygen -= used;

        OxygenChangeEvent?.Invoke(_currentOxygen);

        if (_currentOxygen <= 0)
            Die();
    }

    public void ApplyDamage()
    {
        if (!_isRunnig)
            return;

        _currentLifes--;
        _activeSphere.OnDamage();

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

    public void AddCoins(int coins)
    {
        if (!_isRunnig)
            return;

        _currentCoins += coins;
    }

    public void AddOxygen(float oxygen)
    { 
        _currentOxygen += oxygen;

        if (_currentOxygen >= _activeSphere.MaxOxygen)
            _currentOxygen = _activeSphere.MaxOxygen;

        OxygenChangeEvent?.Invoke(_currentOxygen);
    }

    private void Die()
    {
        _activeSphere.OnDie();

        GameManager.Instance.Die();
    }

    public void SetNextSphere()
    {
        if (_activeSphereIndex + 1 < _spheres.Length)
        {
            _spheres[_activeSphereIndex].gameObject.SetActive(false);
            _activeSphereIndex++;
            _activeSphere = _spheres[_activeSphereIndex];
            _activeSphere.gameObject.SetActive(true);

            ResetStartVelues();

            _rb.AddForce(Vector3.up * _changeSphereForce);
            _bublesFX.Emit(30);
        }
    }

    public void SetPrevSpehre()
    {
        if (_activeSphereIndex - 1 >= 0)
        {
            _spheres[_activeSphereIndex].gameObject.SetActive(false);
            _activeSphereIndex--;
            _activeSphere = _spheres[_activeSphereIndex];
            _activeSphere.gameObject.SetActive(true);

            ResetStartVelues();

            _rb.AddForce(Vector3.up * _changeSphereForce);
            _bublesFX.Emit(30);
        }
    }

    private IEnumerator BlockCoroutine(float seconds, bool fx)
    {
        if (fx)
            _flashesFX.SetActive(true);
     
        _isBlocked = true;

        yield return new WaitForSeconds(seconds);

        _isBlocked = false;
        _flashesFX.SetActive(false);
    }
}
