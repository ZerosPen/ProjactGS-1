using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private ItemsSO item;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI qtyItems;

    public void SetItem(ItemsSO newitem)
    {
        item = newitem;
    }

    public void UpdateDetailUI()
    {
        ItemDetailHandller.Instance.UpdateUIDetailItem(item);
    }

    public void UpdateImage(Sprite imageItem, int qty)
    {
        itemImage.sprite = imageItem;
        qtyItems.text = qty.ToString();
    }
}
