using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AltitudeMeterUI : MonoBehaviour {

    [SerializeField] private Image background;
    [SerializeField] private Image scale;
    [SerializeField] private TextMeshProUGUI heightText;

    void Update() {
        float currentHeightTimes100 = Mathf.Round(Player.Instance.GetCurrentHeight() * 100);

        Vector2 offset = new Vector2(0, currentHeightTimes100 / 100f + 0.5f);
        scale.material.mainTextureOffset = offset;
        scale.material.SetTextureOffset("_NormalMap", offset);

        heightText.text = (currentHeightTimes100 / 10f).ToString("0") + "ft";
    }
}
