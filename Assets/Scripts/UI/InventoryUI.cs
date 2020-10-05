using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI> {
    
    public GameObject inventoryUIItemPrefab;
    

    public void UpdateInventoryUI(){
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach(Transform child in children){
            if(child != transform){
                Destroy(child.gameObject);
            }
        }

        foreach(Item itemType in PlayerInventory.instance.inventoryItems){
            InventoryItem tempItem = GlobalFunctions.instance.itemDictionary[itemType];
            GameObject newInventoryItem = Instantiate(inventoryUIItemPrefab, transform);
            newInventoryItem.GetComponentInChildren<Image>().sprite = tempItem.sprite;
            newInventoryItem.GetComponentInChildren<Text>().text = tempItem.itemName;
        }
    }
}
