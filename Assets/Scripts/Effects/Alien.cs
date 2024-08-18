using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Alien : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxTiltAngle = 20f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float stayAtPositionMaxTime = 2f;
    [SerializeField] private float effectMaxTime = 4f;

    private Animator animator;
    private Volume postProcessingVolume;

    private Vector2 targetPosition;
    private float stayAtPositionTimer = 0;
    private float effectTimer = 0;
    private bool hasDoneFlare = false;

    private void Awake() {
        animator = GetComponent<Animator>();
        postProcessingVolume = GetComponent<Volume>();
    }

    private void Start() {
        SetRandomTargetPosition();
    }

    private void Update() {
        HandleMovement();

        if (hasDoneFlare) {
            effectTimer += Time.deltaTime;
            if (effectTimer > effectMaxTime) {
                GameInputManager.Instance.SetReversedControls(false);
                hasDoneFlare = false;
                if (postProcessingVolume.profile.TryGet(out WhiteBalance whiteBalance)) {
                    whiteBalance.temperature.value = 0;
                }
            }
        }
    }

    private void HandleMovement() {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {
            stayAtPositionTimer += Time.deltaTime;

            // Return to no rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);

            if (stayAtPositionTimer > stayAtPositionMaxTime) {
                stayAtPositionTimer = 0;
                SetRandomTargetPosition();
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector2 currentPosition = transform.position;
        Vector3 direction = (targetPosition - currentPosition).normalized;

        float tilt = direction.x * -maxTiltAngle;

        Quaternion targetRotation = Quaternion.Euler(0, 0, tilt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void SetRandomTargetPosition() {
        Vector2 screenLeftLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.10f, 0, 0));
        Vector2 screenLeftMiddle = Camera.main.ViewportToWorldPoint(new Vector3(0.40f, 0, 0));
        Vector2 screenMiddleRight = Camera.main.ViewportToWorldPoint(new Vector3(0.60f, 0, 0));
        Vector2 screenRightRight = Camera.main.ViewportToWorldPoint(new Vector3(0.80f, 0, 0));

        Vector2 screenBottomBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.30f, 0));
        Vector2 screenTopTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.90f, 0));

        bool left = Random.Range(-1f, 1f) > 0 ? true : false;
        float x;
        if (left) {
            x = Random.Range(screenLeftLeft.x, screenLeftMiddle.x);
        } else {
            x = Random.Range(screenMiddleRight.x, screenRightRight.x);
        }
        float y = Random.Range(screenBottomBottom.y, screenTopTop.y);

        targetPosition = new Vector2(x, y);
    }

    public void FlareAttack() {
        animator.SetTrigger("Flare");
        hasDoneFlare = true;
        effectTimer = 0;
        if (postProcessingVolume.profile.TryGet(out WhiteBalance whiteBalance)) {
            whiteBalance.temperature.value = -50f;
        }

        GameInputManager.Instance.SetReversedControls(true);
    }
}
