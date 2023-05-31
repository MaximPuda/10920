using UnityEngine;

public class Menu : UIScreen
{
    [SerializeField] private UICounter _lifes;
    [SerializeField] private UICounter _oxygen;

    public void OnPlayBTN() => GameManager.Instance.Run();

    public void SetLifes(int lifes) => _lifes.SetCounter(lifes);

    public void SetOxygen(int oxygen) => _oxygen.SetCounter(oxygen);
}
