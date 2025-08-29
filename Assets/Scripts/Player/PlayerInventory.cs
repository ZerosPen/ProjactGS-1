using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PlayerItems
{
    public string itemID;
    public int Qtyitems;

    public PlayerItems(string itemID, int qty)
    {
        this.itemID = itemID;
        this.Qtyitems = qty;
    }
}

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<PlayerItems> invetoryList = new List<PlayerItems>();

    public static PlayerInventory Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItems(string nameID, int qtyItems)
    {
        PlayerItems item = FindItemByID(nameID);
        if (item != null)
        {
            item.Qtyitems += qtyItems;
            return;
        }
        item = new PlayerItems(nameID, qtyItems);
        invetoryList.Add(item);
    }

    public void RemoveItems(string nameID, int qtyItems) 
    {
        PlayerItems item = FindItemByID(nameID);
        if (item != null)
        {
            if (item.Qtyitems > qtyItems)
            {
                item.Qtyitems -= qtyItems;
                return;
            }
        }
        invetoryList.Remove(item);
    }

    PlayerItems FindItemByID(string nameID)
    {
        return invetoryList.Find(item => item.itemID == nameID);
    }
}
