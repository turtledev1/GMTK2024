using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour {

    [SerializeField] private float timeBetweenEffects = 10f;
    [SerializeField] private GameObject alienPrefab;

    public static AlienManager Instance { get; private set; }

    private bool isActive = false;
    private float effectTimer = 0;
    private Alien alien;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (isActive) {
            effectTimer += Time.deltaTime;
            if (effectTimer > timeBetweenEffects) {
                alien.FlareAttack();
                effectTimer = 0;
            }
        }
    }

    public void SetIsActive(bool active) {
        isActive = active;
        if (isActive) {
            alien = Instantiate(alienPrefab, new Vector3(), Quaternion.identity).GetComponent<Alien>();
        } else {
            if (alien != null) {
                Destroy(alien);
                alien = null;
            }
        }
    }
}
