using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory> {
    public List<Item> inventoryItems;


    public override void Awake(){
        base.Awake();
        if(inventoryItems == null){
            inventoryItems = new List<Item>();
        }
    }

    public void GiveItem(Item item){
        inventoryItems.Add(item);
    }

    public bool HasItem(Item item){
        return inventoryItems.Contains(item);
    }

    public void RemoveItem(Item item){
        if(inventoryItems.Contains(item) == false){
            Debug.LogError($"Inventory didn't contain item to remove: {item}");
            return;
        }
        inventoryItems.Remove(item);
    }
}
