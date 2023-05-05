using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _maxTouchDistance;
    [SerializeField] private float _deadZone = 1f;

    public event UnityAction<Vector3> TouchInputEvent;

    private Touch _touch;
    private Vector2 _touchStart;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
                _touchStart = _touch.position;

            if (_touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = _touch.position - _touchStart;
                if (delta.magnitude >= _deadZone)
                    TouchInputEvent?.Invoke(Vector2.ClampMagnitude(delta, 1));
            }

            if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
                TouchInputEvent?.Invoke(Vector2.zero);
        }
    }
}
