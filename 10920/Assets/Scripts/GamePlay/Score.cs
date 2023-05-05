using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private int _distance = 10920;
    [SerializeField] private int _minutesToEnd = 180;

    [SerializeField] private TextMeshProUGUI _depthLabel;

    public event UnityAction StartEvent;
    public event UnityAction EndEvent;

    private float _speed;
    private float _currentDepth;

    private void Start()
    {
        _depthLabel.text = "0";
        _speed = _distance / (_minutesToEnd * 60);
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Run)
            return;

        _currentDepth += _speed * Time.deltaTime;
        UpdateScore((int) _currentDepth);

        if (_currentDepth > _distance) { EndEvent?.Invoke(); }
    }

    private void UpdateScore(int depth)
    {
        _depthLabel.text = depth.ToString("### ###");
    }
}
