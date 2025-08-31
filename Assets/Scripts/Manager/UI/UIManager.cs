using System.Collections.Generic;
using TMPro;
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

    public void UpdateHotbarInvetroy(object param = null)
    {
        if ((hotbarImg == null && hotbarQtyItem == null) || (hotbarImg.Length == 0 || hotbarQtyItem.Length == 0)) return;

        // Clear all slots first
        for (int i = 0; i < hotbars.Length; i++)
        {
            hotbarImg[i].sprite = null;
            hotbarImg[i].enabled = false;

            hotbarQtyItem[i].text = "";
            hotbarQtyItem[i].enabled = false;
        }

        //Fill the hotbar with the player item
        List<PlayerItems> inventory = PlayerInventory.Instance.GetInventoryItemsList();
        int count = Mathf.Min(hotbars.Length, inventory.Count);

        for (int i = 0; i < count; i++)
        {
            PlayerItems item = inventory[i];

            if (item == null) continue;

            ItemsSO data = itemDataBase.GetItemByID(item.itemID);
            if (data == null) continue;

            Sprite itemSprite = data.spriteImage;
            int qty = item.qtyItems;

            hotbarImg[i].sprite = itemSprite;
            hotbarImg[i].enabled = true;
            hotbarQtyItem[i].text = qty.ToString();
            hotbarQtyItem[i].enabled = true;      //  re-enable
            Debug.Log($"Hotbar {i}: {item.itemID} x{qty}");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnloadedScene;

        EventBus.RegisterEvent("UpdateInventoryUI", UpdateHotbarInvetroy);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnloadedScene;

        EventBus.DeRegisterEvent("UpdateInventoryUI", UpdateHotbarInvetroy);
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

            Transform itemImg = hotbars[i].transform.Find("ImageHotbar");
            if (itemImg != null)
                hotbarImg[i] = itemImg.GetComponent<Image>();

            hotbarQtyItem[i] = hotbars[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

}
