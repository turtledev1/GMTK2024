using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour {

    [SerializeField] private float timeBetweenEffects = 10f;
    [SerializeField] private GameObject meteorPrefab;

    public static MeteorManager Instance { get; private set; }

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
                SpawnMeteor();
                effectTimer = 0;
            }
        }
    }

    private void SpawnMeteor() {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f) > 0 ? 1 : -1, Random.Range(-0.3f, 0.3f));
        Debug.Log(direction);

        Vector3 screenLeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0));
        Vector3 screenLeftTop = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0));

        Vector3 screenRightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0));
        Vector3 screenRightTop = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));

        float height = Random.Range(screenLeftBottom.y, screenLeftTop.y);
        GameObject meteor;

        if (direction.x < 0) {
            meteor = Instantiate(meteorPrefab, new Vector3(screenRightBottom.x + 20f, height), Quaternion.identity);
        } else {
            meteor = Instantiate(meteorPrefab, new Vector3(screenLeftBottom.x - 20f, height), Quaternion.identity);
        }
        meteor.GetComponent<Meteor>().SetDirection(direction);
    }

    public void SetIsActive(bool active) {
        isActive = active;
    }
}
