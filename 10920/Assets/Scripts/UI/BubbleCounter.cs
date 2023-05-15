using UnityEngine;

public class BubbleCounter : MonoBehaviour
{

    [SerializeField] protected GameObject[] Items;

    private void ResetCounter()
    {
        foreach (var item in Items)
        {
            item.SetActive(false);
            item.transform.localScale = Vector3.one;
        }
    }

    public  void SetCounter(float count)
    {
        ResetCounter();

        for (int i = 0; i < count; i++)
            Items[i].SetActive(true);

        float value =  count % 1;
        int index = (int)count;

        if (index > Items.Length - 1)
            index = Items.Length - 1;

        Items[index].transform.localScale = Vector3.one * value;
    }
}
