using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTypewriter : MonoBehaviour {
    [SerializeField] private float delay = 0.1f;
    [SerializeField, TextArea(3, 10)] private string fullText;

    private string currentText;
    private TextMeshProUGUI text;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator ShowText() {
        for (int i = 0; i <= fullText.Length; i++) {
            currentText = fullText.Substring(0, i);
            text.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void StartShowing() {
        StartCoroutine(ShowText());
    }
}
