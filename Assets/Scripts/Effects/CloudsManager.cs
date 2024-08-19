using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour {

    [SerializeField] private float timeBetweenEffects = 5f;
    [SerializeField] private GameObject cloudPrefab;

    public static CloudsManager Instance { get; private set; }

    private bool isActive = false;
    private float effectTimer = 0;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (isActive) {
            effectTimer += Time.deltaTime;
            if (effectTimer > timeBetweenEffects) {
                SpawnCloud();
                effectTimer = 0;
            }
        }
    }

    private void SpawnCloud() {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f) > 0 ? 1 : -1, 0);

        Vector3 screenLeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.3f, 0));
        Vector3 screenLeftTop = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0));

        Vector3 screenRightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.3f, 0));
        Vector3 screenRightTop = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));

        float height = Random.Range(screenLeftBottom.y, screenLeftTop.y);
        GameObject cloud;

        if (direction.x < 0) {
            cloud = Instantiate(cloudPrefab, new Vector3(screenRightBottom.x + 6f, height), Quaternion.identity);
        } else {
            cloud = Instantiate(cloudPrefab, new Vector3(screenLeftBottom.x - 6f, height), Quaternion.identity);
        }
        cloud.GetComponent<CloudEffect>().SetDirection(direction);
    }

    public void SetIsActive(bool active) {
        isActive = active;
    }
}
