using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEffect : MonoBehaviour {

    [SerializeField] private Vector2 movSpeedMinMax = new Vector2(2, 4);
    [SerializeField] private Vector2 sizeMinMax = new Vector2(1, 2);
    [SerializeField] private Transform sprite;

    private Vector2 direction = Vector2.right;
    private float movSpeed;
    private bool willBeDestroyed = false;
    private float offscreenPosition;

    private void Awake() {
        movSpeed = Random.Range(movSpeedMinMax.x, movSpeedMinMax.y);
    }

    private void Update() {
        float newX = transform.position.x + direction.x * movSpeed * Time.deltaTime;
        transform.position = new Vector2(newX, transform.position.y);

        if (willBeDestroyed) {
            return;
        }

        if (IsCloudOffScreen()) {
            willBeDestroyed = true;
            Destroy(gameObject, 2f);
        }
    }

    private bool IsCloudOffScreen() {
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

            sprite.localScale = new Vector2(-sprite.localScale.x, sprite.localScale.y);

            offscreenPosition = transform.position.x - 100;
        } else {
            // Spawned at the left and go to the right
            offscreenPosition = transform.position.x + 100;
        }
        float size = Random.Range(sizeMinMax.x, sizeMinMax.y);
        sprite.localScale *= size;
    }
}
