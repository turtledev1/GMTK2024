using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindIndicatorUI : MonoBehaviour {

    [SerializeField] private Transform compass;
    [SerializeField] private Transform arrow;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private float rotationSpeed = 5f;

    private Animator animator;

    private float targetRotation;

    private void Awake() {
        // TODO
        // animator = GetComponent<Animator>();
    }

    private void Start() {
        WindManager.Instance.OnWindActivated += WindManager_OnWindActivated;
        WindManager.Instance.OnWindDeactivated += WindManager_OnWindDeactivated;
        compass.gameObject.SetActive(false);
    }

    private void WindManager_OnWindDeactivated(object sender, System.EventArgs e) {
        compass.gameObject.SetActive(false);
    }

    private void WindManager_OnWindActivated(object sender, System.EventArgs e) {
        // TODO
        // animator.SetTrigger("Appear");
        compass.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        Vector2 wind = WindManager.Instance.GetWind();
        speedText.text = Mathf.Abs(wind.x * 10).ToString("0") + " mph";
        if (wind.x > 0) {
            targetRotation = -90;
        } else if (wind.x < 0) {
            targetRotation = 90;
        }

        arrow.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arrow.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime));
    }
}
