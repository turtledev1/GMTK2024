using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour {

    private float initialStartX;

    private void Start() {
        initialStartX = transform.position.x;
    }

    private void Update() {
        Vector3 screenMiddleTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
        transform.position = new Vector3(initialStartX + screenMiddleTop.x, transform.position.y, transform.position.z);
    }
}
