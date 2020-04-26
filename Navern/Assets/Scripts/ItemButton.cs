using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {
    // Elements
    public Image itemImage;
    public Text amountText;
    public int buttonValue;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // Process the logic when press an item button in the inventory.
    public void Press() {
        if (GameManager.selfReference.itemsInInventory[buttonValue] != "") {
            GameplayMenu.selfReference.SelectItem(GameManager.selfReference.GetItemDetails(GameManager.selfReference.itemsInInventory[buttonValue]));
        }
    }
}
