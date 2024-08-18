using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltitudeMeter : MonoBehaviour {

    [SerializeField] private Image background;
    [SerializeField] private Image scale;

    void Update() {
        scale.material.mainTextureOffset = new Vector2(0, Player.Instance.GetCurrentHeight());
    }
}
