using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour 
{
    public ItemsSO[] items;

    public ItemsSO GetItemByID(string itemID)
    {
        foreach (var item in items)
        {
            if (item.nameID == itemID)
            {
                return item;
            }
        }
        return null;
    }

}
