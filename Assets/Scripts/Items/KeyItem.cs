using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, Iinteracttable
{
    public ItemsSO item;
    public int qtyItems;

    public void intreact()
    {
        PlayerInventory.Instance.AddItems(item.nameID, qtyItems);
        Debug.Log(qtyItems);
        Destroy(gameObject);
    }
}
