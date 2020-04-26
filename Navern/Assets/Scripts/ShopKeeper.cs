using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour {
    // Elements
    private bool canOpen;

    public string[] itemsForSell = new string[40];

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Open the shop menu when talk to the shopkeeper.
        if (canOpen && Input.GetButtonDown("Yes Button") && PlayerControl.selfReference.canMove && !Shop.selfReference.shopMenu.activeInHierarchy) {
            Shop.selfReference.ItemsForSale = itemsForSell;

            Shop.selfReference.OpenShop();
        }

        // Close the shop when press "No Button"
        if (Input.GetButtonDown("No Button") && Shop.selfReference.shopMenu.activeInHierarchy) {
            Shop.selfReference.CloseShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            canOpen = false;
        }
    }
}
