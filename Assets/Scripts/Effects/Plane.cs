using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Plane : MonoBehaviour {

    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private Transform sprite;

    private CinemachineBasicMultiChannelPerlin noise;

    private Vector2 direction = Vector2.right;
    private bool willBeDestroyed = false;
    private float offscreenPosition;

    private void Start() {
        var virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 1.5f;
        noise.m_FrequencyGain = 1.5f;
    }

    // Update is called once per frame
    private void Update() {
        float newX = transform.position.x + direction.x * moveSpeed * Time.deltaTime;
        transform.position = new Vector2(newX, transform.position.y);

        if (willBeDestroyed) {
            return;
        }

        if (IsPlaneOffScreen()) {
            willBeDestroyed = true;
            Destroy(gameObject, 2f);
        }
    }

    private bool IsPlaneOffScreen() {
        if (direction.x > 0) {
            return transform.position.x >= offscreenPosition;
        } else {
            return transform.position.x <= offscreenPosition;
        }
    }

    private void OnDestroy() {
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }

    public void SetDirection(Vector2 newDirection) {
        direction = newDirection;
        if (direction.x < 0) {
            // Spawned at the right and go to the left

            sprite.localScale = new Vector2(-sprite.localScale.x, sprite.localScale.y);

            offscreenPosition = transform.position.x - 45;
        } else {
            // Spawned at the left and go to the right
            offscreenPosition = transform.position.x + 45;
        }
    }
}
