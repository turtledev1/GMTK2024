using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Meteor : MonoBehaviour {

    [SerializeField] private Vector2 movSpeedMinMax = new Vector2(4, 8);
    [SerializeField] private Vector2 sizeMinMax = new Vector2(0.8f, 1.5f);
    [SerializeField] private Vector2 rotationSpeedMinMax = new Vector2(8f, 20f);
    [SerializeField] private Transform sprite;

    private Vector2 direction = Vector2.right;
    private bool willBeDestroyed = false;
    private float offscreenPosition;
    private float moveSpeed;
    private float rotationSpeed;

    private void Awake() {
        moveSpeed = Random.Range(movSpeedMinMax.x, movSpeedMinMax.y);
        rotationSpeed = Random.Range(rotationSpeedMinMax.x, rotationSpeedMinMax.y) * (Random.Range(-1f, 1f) > 0 ? 1 : -1);
        sprite.localScale = new Vector2((Random.Range(-1f, 1f) > 0 ? 1 : -1) * sprite.localScale.x, sprite.localScale.y);
    }

    private void Update() {
        float newX = transform.position.x + direction.x * moveSpeed * Time.deltaTime;
        float newY = transform.position.y + direction.y * moveSpeed * Time.deltaTime;
        transform.position = new Vector2(newX, newY);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (willBeDestroyed) {
            return;
        }

        if (IsMeteorOffScreen()) {
            willBeDestroyed = true;
            Destroy(gameObject, 2f);
        }
    }

    private bool IsMeteorOffScreen() {
        if (direction.x > 0) {
            return transform.position.x >= offscreenPosition;
        } else {
            return transform.position.x <= offscreenPosition;
        }
    }

    public void SetDirection(Vector2 newDirection) {
        direction = newDirection;
        if (direction.x < 0) {
            // Spawned at the right and go to the left
            offscreenPosition = transform.position.x - 45;
        } else {
            // Spawned at the left and go to the right
            offscreenPosition = transform.position.x + 45;
        }
        float size = Random.Range(sizeMinMax.x, sizeMinMax.y);
        sprite.localScale *= size;
    }
}
