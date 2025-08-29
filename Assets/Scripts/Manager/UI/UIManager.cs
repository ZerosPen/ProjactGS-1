using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("status UI & Game")]
    public bool isPanelOpen;

    [Header("Refence Hotbars")]
    public GameObject uiHotbar;
    public GameObject[] hotbars;
    public Image[] hotbarImg;
    public TextMeshProUGUI[] hotbarQtyItem;

    [Header("Refence Database")]
    public ItemsDataBase itemDataBase;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
        }
        UpdateHotbarInvetroy();
    }

    public void UpdateHotbarInvetroy()
    {
        if ((hotbarImg == null && hotbarQtyItem == null) || (hotbarImg.Length == 0 || hotbarQtyItem.Length == 0)) return;

        foreach (var slot in hotbarImg)
        {
            slot.sprite = null;
            slot.enabled = false;
        }

        foreach (var slot in hotbarQtyItem)
        {
            slot.text = "";
            slot.enabled = false;
        }

        //Fill the hotbar with the player item
        List<PlayerItems> inventory = PlayerInventory.Instance.GetInventoryItemsList();

        for (int i = 0; i < hotbars.Length; i++)
        {
            PlayerItems item = inventory[i];
            if (item == null) continue;

            ItemsSO data = itemDataBase.GetItemByID(item.itemID);

            // TODO: Replace with your own item icon lookup
            Sprite itemSprite = data.spriteImage;
            int qty = item.qtyItems;

            hotbarImg[i].sprite = itemSprite;
            hotbarQtyItem[i].text = qty.ToString();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnloadedScene;

        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.inventoryUpdate.AddListener(UpdateHotbarInvetroy);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnloadedScene;

        if (PlayerInventory.Instance != null)
            PlayerInventory.Instance.inventoryUpdate.RemoveListener(UpdateHotbarInvetroy);
    }

    private void OnloadedScene(Scene scene, LoadSceneMode mode)
    {
        //Find the parent of HotBarUI
        Transform hotbarUI = GameObject.FindWithTag("HotbarUI")?.transform;
        if (hotbarUI == null) return;


        //Find all The Hotbars 
        int count = hotbarUI.childCount;

        //Resize array to match the hotbar in game
        hotbars = new GameObject[count];
        hotbarImg = new Image[count];
        hotbarQtyItem =  new TextMeshProUGUI[count];

        for (int i = 0; i < count; i++)
        {
            // Take children in hierarchy order
            Transform child = hotbarUI.GetChild(i);
            hotbars[i] = child.gameObject; // Store child GameObject
            hotbarImg[i] = hotbars[i].GetComponent<Image>();
            hotbarQtyItem[i] = hotbars[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

}
