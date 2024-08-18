using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBackground : MonoBehaviour {

    [SerializeField] private Color startColor = new Color(0f / 255f, 185f / 255f, 255f / 255f);
    [SerializeField] private Color endColor = new Color(9f / 255f, 0f / 255f, 51f / 255f);

    private SpriteRenderer spriteRenderer;

    private float maxHeight;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void Start() {
        maxHeight = LayerManager.Instance.GetMaxHeightForLayer(LayerManager.Layer.Space);
    }

    void Update() {
        float height = Player.Instance.GetCurrentHeight();

        Color newColor = Color.Lerp(startColor, endColor, Mathf.Clamp01(height / maxHeight));

        spriteRenderer.color = newColor;
    }
}