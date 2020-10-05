using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : Singleton<PlayerInventory> {
    public List<Item> inventoryItems;



    public UnityEvent pickupEvent;
    


    public override void Awake(){
        base.Awake();
        if(inventoryItems == null){
            inventoryItems = new List<Item>();
        }
    }

    public void GiveItem(Item item){
        pickupEvent.Invoke();
        inventoryItems.Add(item);
        InventoryUI.instance.UpdateInventoryUI();
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
        InventoryUI.instance.UpdateInventoryUI();
    }
}
