using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ItemSO"), fileName = "NewItemSO")]
public class ItemsSO : ScriptableObject
{
    public string nameID;
    public string Desc;
    public Sprite spriteImage;
}
