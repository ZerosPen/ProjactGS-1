using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeItem
{
    key,
    potion,
    weapon,
    food
}

[CreateAssetMenu(menuName = ("ItemSO"), fileName = "NewItemSO")]
public class ItemsSO : ScriptableObject
{
    public string nameID;
    public TypeItem typeItem;
    public string Desc;
    public int MaxStack;
    public Sprite spriteImage;
}
