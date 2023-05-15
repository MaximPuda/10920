using UnityEngine;

public class Batisphere : MonoBehaviour
{
    [Header("DATA")]
    [SerializeField] private int _startLifes;
    [SerializeField] private float _maxOxygen; 
    [SerializeField] private float _verticalSpeed;

    public int StartLifes => _startLifes;
    public float MaxOxygen => _maxOxygen;
    public float VerticalSpeed => _verticalSpeed;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnDamage()
    {
        _anim.SetTrigger("Blink");
    }

    public void OnDie()
    {
        _anim.SetTrigger("Die");
    }
}
