using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour {

    [SerializeField] private Vector2 windSpeedMinMax = new Vector2(0.2f, 3f);
    [SerializeField] private Vector2 windChangeRateMinMax = new Vector2(1f, 5f);
    [SerializeField] private float windDirectionChangeSpeed = 1f;

    public static WindManager Instance { get; private set; }

    public event EventHandler OnWindActivated;
    public event EventHandler OnWindDeactivated;

    private bool isActive = false;
    private float windTimer = 0;
    private Vector2 currentWind = Vector2.zero;
    private float targetWindDirection;
    private float currentWindDirection;
    private float currentWindForce;
    private float windChangeRate;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        windChangeRate = UnityEngine.Random.Range(windChangeRateMinMax.x, windChangeRateMinMax.y);
    }

    void Update() {
        if (isActive) {
            ApplyWind();
        } else {
            currentWind = Vector2.zero;
        }
    }

    private void ApplyWind() {
        currentWindDirection = Mathf.Lerp(currentWindDirection, targetWindDirection, Time.deltaTime * windDirectionChangeSpeed);
        currentWind = new Vector2(currentWindForce * currentWindDirection, 0);

        windTimer += Time.deltaTime;
        if (windTimer > windChangeRate) {
            RandomizeWind();
            windTimer = 0;
        }
    }

    private void RandomizeWind() {
        targetWindDirection = UnityEngine.Random.Range(-1f, 1f) > 0 ? 1f : -1f;
        currentWindForce = UnityEngine.Random.Range(windSpeedMinMax.x, windSpeedMinMax.y);

        windChangeRate = UnityEngine.Random.Range(windChangeRateMinMax.x, windChangeRateMinMax.y);
    }

    public Vector2 GetWind() {
        return currentWind;
    }

    public void SetIsActive(bool active) {
        isActive = active;
        if (isActive) {
            OnWindActivated?.Invoke(this, EventArgs.Empty);
        } else {
            OnWindDeactivated?.Invoke(this, EventArgs.Empty);
        }
    }
}
