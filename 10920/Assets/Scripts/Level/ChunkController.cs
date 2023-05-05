using System.Collections.Generic;
using UnityEngine;

public class ChunkController : Controller
{
    [SerializeField] private Chunk[] _chunks;
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private int _amonut;
    [SerializeField] private float _chunkSpeed = 2f;
    [SerializeField] private float _destroyDistance = 10f;

    private Chunk _lastChunk;
    private List<Chunk> _activeChunks = new();

    private bool _isRun;

    public override void Init()
    {
        base.Init();

        if (_startChunk != null)
        {
            _lastChunk = FindObjectOfType<Chunk>();
            _activeChunks.Add(_lastChunk);
        }
    }

    public override void OnMenu()
    {
        base.OnMenu();
        _isRun = false;
    }

    public override void OnRun()
    {
        base.OnRun();

        _isRun = true;
    }

    public override void OnDie()
    {
        base.OnDie();

        _isRun = false;
    }

    private void Update()
    {
        if (_isRun)
        {
            CheckCount();
            MoveChunks();
            CheckDistance();
        }
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
        if (_lastChunk != null)
            newChunk.transform.position = _lastChunk.End.position;
        else newChunk.transform.position = Vector3.zero;

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
