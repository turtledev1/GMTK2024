using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI highscoreText;

    private void Start() {
        if (HighScore.HasHighScore()) {
            var highscore = HighScore.GetHighScore();

            highscoreText.text = Mathf.Round(highscore.distanceFromCenter * 10) + "ft from center\n"
                + "in " + Mathf.Round(highscore.timeToReachTheMoon) + "seconds\n"
                + "with " + highscore.livesLeft + " lives left";
        } else {
            gameObject.SetActive(false);
        }
    }
}
