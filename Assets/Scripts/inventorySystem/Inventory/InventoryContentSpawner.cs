using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContentSpawner : MonoBehaviour
{
    public List<PlayerItems> itemsList;

    public GameObject InventoryContentPrefabs;
    public Transform ContainerContent;

    [Header("Refence Database")]
    public ItemsDataBase itemDataBase;

    private void Start()
    {
        SpawnInventory();
    }

    public void SpawnInventory(object Args = null)
    {
        itemsList = PlayerInventory.Instance.GetInventoryItemsList();

        //clear old Button
        foreach(Transform child in ContainerContent) 
        { 
            Destroy(child.gameObject);
        }

        foreach (PlayerItems item in itemsList)
        {
            GameObject newbutton = Instantiate(InventoryContentPrefabs, ContainerContent);
            InventoryButton inventoryButton = newbutton.GetComponent<InventoryButton>();

            ItemsSO dataitem = itemDataBase.GetItemByID(item.itemID);

            inventoryButton.SetItem(dataitem);
            inventoryButton.UpdateImage(dataitem.spriteImage, item.qtyItems);
        }
    }

    private void OnEnable()
    {
        EventBus.RegisterEvent("UpdateInventory", SpawnInventory);
    }

    private void OnDisable()
    {
        EventBus.DeRegisterEvent("UpdateInventory", SpawnInventory);
    }
}
