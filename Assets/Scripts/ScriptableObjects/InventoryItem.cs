using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject {
    public Sprite sprite;
    public string itemName;
}
