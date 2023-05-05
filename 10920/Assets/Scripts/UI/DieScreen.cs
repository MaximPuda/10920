using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScreen : UIScreen
{
    public void OnRestartButton()
    {
        GameManager.Instance.Restart();
    }
}
