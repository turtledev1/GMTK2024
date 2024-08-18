using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ladder : MonoBehaviour {
    [SerializeField] private float fallingSpeed;
    [SerializeField] private float controlSpeed;
    [SerializeField] private bool shouldShowNumber = false;
    [SerializeField] private GameObject numberObject;

    private Rigidbody2D rb;
    private bool willBeDestroyed = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        numberObject.SetActive(shouldShowNumber);
    }

    private void Start() {
        numberObject.GetComponent<TextMeshPro>().text = GameManager.Instance.GetCurrentHeight().ToString();
    }

    private void FixedUpdate() {
        Vector2 movement = GameInputManager.Instance.GetMovementVectorNormalized();
        Vector2 wind = WindManager.Instance.GetWind();

        float x = transform.position.x + movement.x * controlSpeed * Time.fixedDeltaTime + wind.x * Time.fixedDeltaTime;
        Vector2 newPosition = new Vector2(x, transform.position.y - fallingSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (willBeDestroyed) {
            return;
        }

        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (IsLadderOffScreen(viewportPosition)) {
            GameManager.Instance.LoseLadder();
            willBeDestroyed = true;
            Destroy(gameObject, 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Make sure collision is below
        ContactPoint2D[] contacts = new ContactPoint2D[2];
        collision.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts) {
            if (contact.normal == Vector2.up) {
                GameManager.Instance.AddLadderPiece(gameObject);
                rb.isKinematic = true;
                transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
                Destroy(this);
                break;
            }
        }
    }

    private bool IsLadderOffScreen(Vector3 viewportPosition) {
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0;
    }
}
