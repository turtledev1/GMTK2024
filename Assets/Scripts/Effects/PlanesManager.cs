using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesManager : MonoBehaviour {

    [SerializeField] private float timeBetweenEffects = 15f;
    [SerializeField] private GameObject planePrefab;

    public static PlanesManager Instance { get; private set; }

    private bool isActive = false;
    private float effectTimer = 0;

    private void Awake() {
        Instance = this;

        // Just to kickstart it faster
        effectTimer = timeBetweenEffects / 2;
    }

    private void Update() {
        if (isActive) {
            effectTimer += Time.deltaTime;
            if (effectTimer > timeBetweenEffects) {
                SpawnPlane();
                effectTimer = 0;
            }
        }
    }

    private void SpawnPlane() {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f) > 0 ? 1 : -1, 0);

        Vector3 screenLeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0));
        Vector3 screenLeftTop = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0));

        Vector3 screenRightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0));
        Vector3 screenRightTop = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));

        float height = Random.Range(screenLeftBottom.y, screenLeftTop.y);
        GameObject plane;

        if (direction.x < 0) {
            plane = Instantiate(planePrefab, new Vector3(screenRightBottom.x + 20f, height), Quaternion.identity);
        } else {
            plane = Instantiate(planePrefab, new Vector3(screenLeftBottom.x - 20f, height), Quaternion.identity);
        }
        plane.GetComponent<Plane>().SetDirection(direction);
    }

    public void SetIsActive(bool active) {
        isActive = active;
    }
}
