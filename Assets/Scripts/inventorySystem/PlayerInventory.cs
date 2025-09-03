using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* <summary>
    Represents an item in the player's inventory with an ID and quantity.
    </summary>*/
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

/*<summary>
     Manages the player's inventory, allowing adding, removing, saving, and loading items.
     Implements IDataPersistence for saving/loading game data.
</summary>*/
public class PlayerInventory : MonoBehaviour, IDataPersistence
{
    [SerializeField] List<PlayerItems> invetoryList = new List<PlayerItems>();
    public ItemsDataBase itemdatabase;
    private int count;

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
        ItemsSO data = itemdatabase.GetItemByID(nameID);
        int maxStack = Mathf.Max(1, data.MaxStack);

        //Check all item in PlayerItems that can stackAble and Try to add to existing stacks that are not full
        foreach (var stack in invetoryList)
        {
            if (stack.itemID == nameID && stack.qtyItems <= maxStack)
            {
                // Calculate how much space is left in this stack
                int spaceLeft = maxStack - stack.qtyItems;
                int addMount = Mathf.Min(spaceLeft, qtyItems);

                stack.qtyItems += addMount; 
                qtyItems -= addMount;

                if (qtyItems <= 0) break;
            }
        }

        int safety = 1000; // Safety counter to prevent infinite loops

        // Add new stacks if there are still items left to add
        while (qtyItems > 0 && safety-- > 0)
        {
            int addAmount = Mathf.Min(maxStack, qtyItems);
            if (addAmount <= 0) break; // stop if nothing to add

            PlayerItems newStack = new PlayerItems(nameID, addAmount);
            invetoryList.Add(newStack);

            qtyItems -= addAmount;
        }

        EventBus.OnTriggerEvent("UpdateHotbarUI", invetoryList);
        EventBus.OnTriggerEvent("UpdateInventory", invetoryList);
    }

    public void RemoveItems(string nameID, int qtyItems) 
    {
        PlayerItems item = FindItemByID(nameID);

        // Iterate backwards to safely remove items from the list
        for (int i = invetoryList.Count - 1; i >= 0 && qtyItems > 0; i--)
        {
            if (invetoryList[i].itemID == nameID)
            {
                if (invetoryList[i].qtyItems > qtyItems)
                {
                    // Reduce quantity in this stack
                    invetoryList[i].qtyItems -= qtyItems;
                    qtyItems = 0;
                }
                else
                {
                    // Remove entire stack and reduce qtyItems accordingly
                    qtyItems -= invetoryList[i].qtyItems;
                    invetoryList.RemoveAt(i);
                }
            }
        }

        EventBus.OnTriggerEvent("UpdateHotbarUI", invetoryList);
        EventBus.OnTriggerEvent("UpdateInventory", invetoryList);
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
        EventBus.OnTriggerEvent("UpdateHotbarUI", invetoryList); 
        EventBus.OnTriggerEvent("UpdateInventory", invetoryList);
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
