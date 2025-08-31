using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerItems
{
    public string itemID;
    public int qtyItems;

    public PlayerItems(string itemID, int qty)
    {
        this.itemID = itemID;
        this.qtyItems = qty;
    }
}

public class PlayerInventory : MonoBehaviour, IDataPersistence
{
    [SerializeField] List<PlayerItems> invetoryList = new List<PlayerItems>();

    public static PlayerInventory Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }

    public void AddItems(string nameID, int qtyItems)
    {
        PlayerItems item = FindItemByID(nameID);
        if (item != null)
        {
            item.qtyItems += qtyItems;
            return;
        }
        else
        {
            item = new PlayerItems(nameID, qtyItems);
            invetoryList.Add(item);
        }
        EventBus.OnTriggerEvent("UpdateInventoryUI", invetoryList);
    }

    public void RemoveItems(string nameID, int qtyItems) 
    {
        PlayerItems item = FindItemByID(nameID);
        if (item != null)
        {
            if (item.qtyItems > qtyItems)
            {
                item.qtyItems -= qtyItems;
                return;
            }
        }
        else
        {
            invetoryList.Remove(item);
        }
        EventBus.OnTriggerEvent("UpdateInventoryUI", invetoryList);
    }

    public List<PlayerItems> GetInventoryItemsList()
    {
        return invetoryList;
    }

    public PlayerItems FindItemByID(string nameID)
    {
        return invetoryList.Find(item => item.itemID == nameID);
    }

    public void LoadData(GameData data)
    {
        // Clear old data so we don't duplicate
        invetoryList.Clear();

        // Load from saved data directly
        foreach (var savedItem in data.inventoryItems)
        {
            invetoryList.Add(new PlayerItems(savedItem.itemID, savedItem.qtyItems));
        }

        // Trigger UI refresh ONCE after everything is loaded
        EventBus.OnTriggerEvent("UpdateInventoryUI", invetoryList);
    }

    public void SaveData(ref GameData data)
    {
        // Clear old data so we don't duplicate
        data.inventoryItems.Clear();

        // Save current inventory into GameData
        foreach (var item in invetoryList)
        {
            data.inventoryItems.Add(new PlayerItems(item.itemID, item.qtyItems));
        }
    }

}
