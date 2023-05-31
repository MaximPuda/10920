using UnityEngine;

public class HUD : UIScreen
{
    public void OnPauseBTN()
    {
        Debug.Log("Pause Clicked!");
        GameManager.Instance.Pause();
    }
}
