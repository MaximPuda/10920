using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class DeepBombTriger : MonoBehaviour
{
    public float Radius => _collider.radius;

    public event UnityAction<PlayerController> TriggerEnterEvent;

    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.TryGetComponent<PlayerController>(out PlayerController player);

        if (player != null)
            TriggerEnterEvent?.Invoke(player);
    }
}
