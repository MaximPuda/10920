using UnityEngine;
using UnityEngine.Events;

public class UnagiTrigger : MonoBehaviour
{
    public event UnityAction TriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.TryGetComponent<PlayerController>(out PlayerController player);

        if (player != null)
            TriggerEvent?.Invoke();
    }
}
