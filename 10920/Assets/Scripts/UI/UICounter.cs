using UnityEngine;

public class UICounter : MonoBehaviour
{
    [SerializeField] protected GameObject[] Items;

    private void ResetCounter()
    {
        foreach (var item in Items)
            item.SetActive(false);
    }

    virtual public void SetCounter(int count)
    {
        ResetCounter();

        for (int i = 0; i < count; i++)
            Items[i].SetActive(true);
    }
}
