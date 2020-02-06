using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    // Elements
    public Rigidbody2D rigidBody;
    public float movingSpeed = 6f;
    public Animator playerAnimator;

    public static PlayerControl selfReference;

    public string transitEntranceName;

    private Vector3 bottomLeftOfMap;
    private Vector3 topRightOfMap;

    private bool wasMovingHorizontal = false;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start() {
        if (selfReference == null) {
            selfReference = this;
        }
        else if (selfReference != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        // Check if the player is able to move or not
        if (canMove) {
            // Set the player's moving speed and prevent diagonal movements
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0) {
                if (wasMovingHorizontal) {
                    rigidBody.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * movingSpeed;
                }

                else {
                    rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * movingSpeed;
                }
            }

            else if (Input.GetAxisRaw("Horizontal") != 0) {
                rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * movingSpeed;
                wasMovingHorizontal = true;
            }

            else if (Input.GetAxisRaw("Vertical") != 0) {
                rigidBody.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * movingSpeed;
                wasMovingHorizontal = false;
            }

            else {
                rigidBody.velocity = Vector2.zero;
            }
        }

        else {
            rigidBody.velocity = Vector2.zero;
        }

        // Get the player to face the right direction
        playerAnimator.SetFloat("moveX", rigidBody.velocity.x);
        playerAnimator.SetFloat("moveY", rigidBody.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            if (canMove) {
                playerAnimator.SetFloat("lastX", Input.GetAxisRaw("Horizontal"));
                playerAnimator.SetFloat("lastY", Input.GetAxisRaw("Vertical"));
            }
        }

        // Keep the player inside the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftOfMap.x, topRightOfMap.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftOfMap.y, topRightOfMap.y),
                                         transform.position.z);
    }

    // Set the bounds for the space that the player can move
    public void SetBounds(Vector3 bottomLeftOfMap, Vector3 topRightOfMap) {
        this.bottomLeftOfMap = bottomLeftOfMap + new Vector3(0.5f, 0.5f, 0f);
        this.topRightOfMap = topRightOfMap - new Vector3(0.5f, 0.5f, 0f);
    }
}
