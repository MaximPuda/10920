using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _maxX;
    [SerializeField] private float _seconds;

    private float _time;
    
    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _seconds)
        {
            Spawn();
            _time = 0;
        }
    }

    private void Spawn()
    {
        float xPos = Random.Range(-_maxX, _maxX);
        Vector3 newPos = new Vector3(xPos, transform.position.y, 0);

        var newObj = Instantiate(_prefab);
        newObj.transform.position = newPos;
    }
}
