using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HighScore {
    public const string PLAYER_PREFS_DISTANCE_FROM_CENTER = "DistanceFromCenter";
    public const string PLAYER_PREFS_TIME_TO_REACH_MOON = "TimeToReachMoon";
    public const string PLAYER_PREFS_LIVES_LEFT = "LivesLeft";

    public static StackingScore GetHighScore() {
        var distanceFromCenter = PlayerPrefs.GetFloat(PLAYER_PREFS_DISTANCE_FROM_CENTER, 200000);
        var timeToReachTheMoon = PlayerPrefs.GetFloat(PLAYER_PREFS_TIME_TO_REACH_MOON, 200000);
        var livesLeft = PlayerPrefs.GetInt(PLAYER_PREFS_LIVES_LEFT, 0);

        return new StackingScore(distanceFromCenter, timeToReachTheMoon, livesLeft);
    }

    public static void SaveHighScoreIfNecessary(StackingScore highScore) {
        Debug.Log(highScore);
        if (HasHighScore()) {
            var score = GetHighScore();
            if (highScore.distanceFromCenter < score.distanceFromCenter) {
                PlayerPrefs.SetFloat(PLAYER_PREFS_DISTANCE_FROM_CENTER, highScore.distanceFromCenter);
                PlayerPrefs.SetFloat(PLAYER_PREFS_TIME_TO_REACH_MOON, highScore.timeToReachTheMoon);
                PlayerPrefs.SetInt(PLAYER_PREFS_LIVES_LEFT, highScore.livesLeft);
            }
        } else {
            PlayerPrefs.SetFloat(PLAYER_PREFS_DISTANCE_FROM_CENTER, highScore.distanceFromCenter);
            PlayerPrefs.SetFloat(PLAYER_PREFS_TIME_TO_REACH_MOON, highScore.timeToReachTheMoon);
            PlayerPrefs.SetInt(PLAYER_PREFS_LIVES_LEFT, highScore.livesLeft);
        }
    }

    public static bool HasHighScore() {
        Debug.Log(PlayerPrefs.HasKey(PLAYER_PREFS_DISTANCE_FROM_CENTER));
        return PlayerPrefs.HasKey(PLAYER_PREFS_DISTANCE_FROM_CENTER);
    }
}
