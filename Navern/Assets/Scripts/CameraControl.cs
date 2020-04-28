using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraControl : MonoBehaviour {
    // Elements
    public Transform target;

    [Header("Camera Elements")]
    public Tilemap map;
    private Vector3 bottomLeftOfMap;
    private Vector3 topRightOfMap;
    private float halfHeightOfCamera;
    private float halfWidthOfCamera;

    [Header("Music Elements")]
    public int musicCode;
    public bool musicPlayed;

    // Start is called before the first frame update
    void Start() {
        // Set the target to the player
        if (PlayerControl.selfReference) {
            target = FindObjectOfType<PlayerControl>().transform;
        }

        // Set the values for the bounds of the map
        halfHeightOfCamera = Camera.main.orthographicSize;
        halfWidthOfCamera = halfHeightOfCamera * Camera.main.aspect;

        bottomLeftOfMap = map.localBounds.min + new Vector3(halfWidthOfCamera, halfHeightOfCamera, 0f);
        topRightOfMap = map.localBounds.max - new Vector3(halfWidthOfCamera, halfHeightOfCamera, 0f);

        // Set the bounds for the space that the player can move
        PlayerControl.selfReference.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    // Update is called once per frame
    void Update() {

    }

    // LateUpdate is called once per frame after Update is called
    void LateUpdate() {
        // Make the camera follow the target
        if (target == null) {
            target = PlayerControl.selfReference.transform;
        }

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keep the camera always in the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftOfMap.x, topRightOfMap.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftOfMap.y, topRightOfMap.y),
                                         transform.position.z);

        // Play the music when loading into a new scene.
        if (!musicPlayed) {
            musicPlayed = true;
            AudioManager.selfReference.PlayMusic(musicCode);
        }
    }
}
