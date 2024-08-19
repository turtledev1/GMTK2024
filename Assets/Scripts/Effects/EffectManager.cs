using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public static EffectManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LayerManager.Instance.OnLayerChanged += LayerManager_OnLayerChanged;

        ApplyEffect(LayerManager.Instance.GetCurrentLayer());
    }

    private void LayerManager_OnLayerChanged(object sender, System.EventArgs e) {
        ApplyEffect(LayerManager.Instance.GetCurrentLayer());
    }

    private void ApplyEffect(LayerManager.Layer currentLayer) {
        switch (currentLayer) {
            case LayerManager.Layer.Ground:
                WindManager.Instance.SetIsActive(false);
                CloudsManager.Instance.SetIsActive(false);
                PlanesManager.Instance.SetIsActive(false);
                AlienManager.Instance.SetIsActive(false);
                MeteorManager.Instance.SetIsActive(false);
                break;
            case LayerManager.Layer.LowerSky:
                WindManager.Instance.SetIsActive(true);
                CloudsManager.Instance.SetIsActive(false);
                PlanesManager.Instance.SetIsActive(false);
                AlienManager.Instance.SetIsActive(false);
                MeteorManager.Instance.SetIsActive(false);
                break;
            case LayerManager.Layer.Sky:
                WindManager.Instance.SetIsActive(true);
                CloudsManager.Instance.SetIsActive(true);
                PlanesManager.Instance.SetIsActive(false);
                AlienManager.Instance.SetIsActive(false);
                MeteorManager.Instance.SetIsActive(false);
                break;
            case LayerManager.Layer.Atmosphere:
                WindManager.Instance.SetIsActive(true);
                CloudsManager.Instance.SetIsActive(true);
                PlanesManager.Instance.SetIsActive(true);
                AlienManager.Instance.SetIsActive(false);
                MeteorManager.Instance.SetIsActive(false);
                break;
            case LayerManager.Layer.Space:
                WindManager.Instance.SetIsActive(false);
                CloudsManager.Instance.SetIsActive(false);
                PlanesManager.Instance.SetIsActive(false);
                AlienManager.Instance.SetIsActive(true);
                MeteorManager.Instance.SetIsActive(true);
                break;
            case LayerManager.Layer.Moon:
                WindManager.Instance.SetIsActive(false);
                CloudsManager.Instance.SetIsActive(false);
                PlanesManager.Instance.SetIsActive(false);
                AlienManager.Instance.SetIsActive(false);
                MeteorManager.Instance.SetIsActive(false);
                break;
        }
    }
}
