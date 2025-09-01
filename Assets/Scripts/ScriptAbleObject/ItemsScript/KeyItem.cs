using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, Iinteracttable
{
    public ItemsSO item;
    public int qtyItems;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
       _sr.sprite = item.spriteImage;
    }

    public void intreact()
    {
        PlayerInventory.Instance.AddItems(item.nameID, qtyItems);
        Destroy(gameObject);
    }
}
