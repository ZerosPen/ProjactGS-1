using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailHandller : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;

    public static ItemDetailHandller Instance;

    private void Awake()
    {
        Instance = this;
    }

    public  void UpdateUIDetailItem(ItemsSO item)
    {
        itemImage.sprite = item.spriteImage;
        itemName.text = item.name;
        itemDesc.text = item.Desc;
    }

}
