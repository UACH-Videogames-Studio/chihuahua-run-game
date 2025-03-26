using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public static List<ObstaclesScript> allMovementScripts = new List<ObstaclesScript>();

    public static void SlowAllObstacles()
    {
        foreach (var obstacleScript in allMovementScripts)
        {
            if (obstacleScript != null)
            {
                obstacleScript.ApplySlowDown();
            }
        }
    }
}