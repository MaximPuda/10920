using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Chunk[] _chunks;
    [SerializeField] private int _amonut;
    [SerializeField] private float _chunkSpeed = 2f;
    [SerializeField] private float _destroyDistance = 10f;

    private Chunk _lastChunk;
    private List<Chunk> _activeChunks = new();

    private void Start()
    {
        _lastChunk = FindObjectOfType<Chunk>();
        _activeChunks.Add(_lastChunk);
    }

    private void Update()
    {
        CheckCount();
        MoveChunks();
        CheckDistance();
    }

    private void CheckCount()
    {
        if (transform.childCount < _amonut)
            Spawn();
    }

    private void CheckDistance()
    {
        if (_activeChunks.Count > 0)
        {
            foreach (var chunk in _activeChunks)
            {
                if (chunk.End.position.y >= _destroyDistance)
                {
                    _activeChunks.Remove(chunk);
                    chunk.Die();
                    break;
                }
            }
        }
    }

    public void Spawn()
    {
        if (_chunks == null) return;

        var prefab = GetRandomChunk();

        var newChunk = Instantiate(prefab, transform);
        newChunk.transform.position = _lastChunk.End.position;
        
        _lastChunk = newChunk;
        _activeChunks.Add(newChunk);
    }

    private Chunk GetRandomChunk()
    {
        int index = Random.Range(0, _chunks.Length);
        return _chunks[index];
    }

    private void MoveChunks()
    {
        if (_activeChunks.Count > 0)
        {
            foreach (var chunk in _activeChunks)
                chunk.Move(_chunkSpeed);
        }
    }
}
