using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _magnitude;
    [SerializeField] private float _duration;

    [SerializeField] private PlayerController _player;

    private void OnEnable()
    {
        _player.HaveDamageEvent += Shake; 
    }

    private void OnDisable()
    {
        _player.HaveDamageEvent -= Shake;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.localPosition;

        float time = 0;

        while (time < _duration) 
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            var newPos = new Vector3(x, originalPos.y, originalPos.z);
            transform.localPosition = newPos;

            time += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
