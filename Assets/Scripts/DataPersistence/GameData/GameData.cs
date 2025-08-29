using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public List<PlayerItems> inventoryItems;

    //the game starts with when there's are no data to be load 
    public GameData()
    {
        inventoryItems = new List<PlayerItems>();
    }
}
