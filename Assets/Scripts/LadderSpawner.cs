using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderSpawner : MonoBehaviour {

    [SerializeField] private Transform ladderPrefab;

    public static LadderSpawner Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        Vector3 screenMiddleTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));

        transform.position = new Vector3(screenMiddleTop.x, screenMiddleTop.y + 1f, transform.position.z);
    }

    public GameObject SpawnNewLadder() {
        float randomFactor = Random.Range(-5f, 5f);
        Vector2 spawnPosition = new Vector2(transform.position.x + randomFactor, transform.position.y);
        return Instantiate(ladderPrefab, spawnPosition, Quaternion.identity).gameObject;
    }
}
