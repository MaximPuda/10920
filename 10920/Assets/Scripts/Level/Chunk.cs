using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform _end;

    public Transform End => _end;

    public void Move(float speed)
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
