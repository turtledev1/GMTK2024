using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenLadderFalling : MonoBehaviour {
    [SerializeField] private float fallingSpeed;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y - fallingSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }
}
