using UnityEngine;


public enum MenuType{Escape, Inventory}

public class PlayerInput : MonoBehaviour {
    
    public GameObject escapeMenu;
    public GameObject tabMenu;

    public bool escapeActive  = false;
    public bool tabMenuActive = false;
    
    void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)){
            ToggleMenu(MenuType.Escape);
        }
        if(Input.GetKeyUp(KeyCode.Tab)){
            ToggleMenu(MenuType.Inventory);
        }
    }

    void ToggleMenu(MenuType menuType){
        if(menuType == MenuType.Escape){
            escapeActive = !escapeActive;
        }
        if(menuType == MenuType.Inventory){
            tabMenuActive = !tabMenuActive;
        }       
        Cursor.visible = escapeActive;
        switch(menuType){
            case MenuType.Escape:
                escapeMenu.SetActive(escapeActive);
                break;
            case MenuType.Inventory:
                tabMenu.SetActive(tabMenuActive);
                break;
        }
    }
}
