using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingScore {
    public float distanceFromCenter;
    public float timeToReachTheMoon;
    public int livesLeft;

    public StackingScore(float distanceFromCenter, float timeToReachTheMoon, int livesLeft) {
        this.distanceFromCenter = distanceFromCenter;
        this.timeToReachTheMoon = timeToReachTheMoon;
        this.livesLeft = livesLeft;
    }
}
