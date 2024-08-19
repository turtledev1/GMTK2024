using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBackground : MonoBehaviour {

    [SerializeField] private Color startColor = new Color(0f / 255f, 185f / 255f, 255f / 255f);
    [SerializeField] private Color endColor = new Color(9f / 255f, 0f / 255f, 51f / 255f);
    [SerializeField] private MeshRenderer stars;

    private Material starsMaterial;

    private SpriteRenderer spriteRenderer;

    private float maxHeight;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void Start() {
        maxHeight = LayerManager.Instance.GetMaxHeightForLayer(LayerManager.Layer.Space);
        List<Material> materials = new List<Material>();
        stars.GetMaterials(materials);
        starsMaterial = materials[0];
    }

    void Update() {
        float height = Player.Instance.GetCurrentHeight();

        Color newColor = Color.Lerp(startColor, endColor, Mathf.Clamp01(height / maxHeight));

        spriteRenderer.color = newColor;

        float adjustedHeight = Mathf.Clamp(height - 60, 0, maxHeight - 60);
        starsMaterial.SetFloat("_Opacity", Mathf.Clamp01(adjustedHeight / (maxHeight - 60)));
    }
}