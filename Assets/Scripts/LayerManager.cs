using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {
    public static LayerManager Instance { get; private set; }

    public event EventHandler OnLayerChanged;

    public enum Layer {
        Ground,
        LowerSky,
        Sky,
        Atmosphere,
        Space,
        Moon,
    }

    private const int groundMaxHeight = 10;
    private const int lowerSkyMaxHeight = 35;
    private const int skyMaxHeight = 60;
    private const int atmosphereMaxHeight = 75;
    private const int spaceMaxHeight = 100;

    private Layer layer;

    private void Awake() {
        Instance = this;
        layer = Layer.Ground;
    }

    private void Update() {
        int height = GameManager.Instance.GetCurrentHeight();
        CheckAndChangeLayer(height);
    }

    private void ChangeLayer(Layer newLayer) {
        if (layer != newLayer) {
            layer = newLayer;
            OnLayerChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void CheckAndChangeLayer(int height) {
        if (height <= groundMaxHeight) {
            ChangeLayer(Layer.Ground);
        } else if (height <= lowerSkyMaxHeight) {
            ChangeLayer(Layer.LowerSky);
        } else if (height <= skyMaxHeight) {
            ChangeLayer(Layer.Sky);
        } else if (height <= atmosphereMaxHeight) {
            ChangeLayer(Layer.Atmosphere);
        } else if (height <= spaceMaxHeight) {
            ChangeLayer(Layer.Space);
        } else {
            ChangeLayer(Layer.Moon);
        }
    }

    public Layer GetCurrentLayer() {
        return layer;
    }

    public int GetMaxHeightForLayer(Layer layer) {
        if (layer == Layer.Ground) {
            return groundMaxHeight;
        }
        if (layer == Layer.LowerSky) {
            return lowerSkyMaxHeight;
        }
        if (layer == Layer.Sky) {
            return skyMaxHeight;
        }
        if (layer == Layer.Atmosphere) {
            return atmosphereMaxHeight;
        }
        if (layer == Layer.Space) {
            return spaceMaxHeight;
        }
        return spaceMaxHeight + 1000;
    }
}
