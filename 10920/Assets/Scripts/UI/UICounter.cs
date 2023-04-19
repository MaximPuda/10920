using UnityEngine;

public class UICounter : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;

    private void ResetCounter()
    {
        foreach (var item in _items)
            item.SetActive(false);
    }

    public void SetCounter(int count)
    {
        ResetCounter();

        for (int i = 0; i < count; i++)
            _items[i].SetActive(true);
    }
}
