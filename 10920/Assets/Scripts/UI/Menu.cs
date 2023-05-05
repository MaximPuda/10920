using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : UIScreen
{
    public void OnPlayBTN()
    {
        GameManager.Instance.Run();
    }
}
