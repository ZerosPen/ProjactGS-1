using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private bool isPanelOpen;

    public void OpenInventory()
    {
        if (!isPanelOpen)
        {
            isPanelOpen = true;
            inventoryUI.SetActive(isPanelOpen);
        }
        else if (isPanelOpen)
        {
            isPanelOpen = false;
            inventoryUI.SetActive(isPanelOpen);
        }
    }
}
