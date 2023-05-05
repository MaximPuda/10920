using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Controller[] _controllers;

    public static GameManager Instance;
    
    public GameState State {  get; private set; }

    public event UnityAction MenuEvent;
    public event UnityAction RunEvent;
    public event UnityAction DieEvent;
    public event UnityAction WinEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);

        foreach (var controller in _controllers)
        {
            if (controller != null)
                controller.Init();
            else
                Debug.LogWarning("One of controllers is null!");
        }
    }

    private void Start()
    {
        State = GameState.Menu;
        MenuEvent?.Invoke();
    }

    public void Run()
    {
        State = GameState.Run;
        RunEvent?.Invoke();
    }

    public void Die()
    {
        State = GameState.Die;
        DieEvent?.Invoke();
    }

    public void Win()
    {
        State = GameState.Win;
        WinEvent?.Invoke();
    }

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }
}
