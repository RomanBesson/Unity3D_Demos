using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2Launcher : MonoBehaviour
{
    void Start()
    {
        Point[] points = {
            new Point(1, 2),
            new Point(3, 4),
            new Point(-1, -1),
            new Point(0, 0),
            new Point(5, 5)
        };

        int k = 3;
        List<Point> closestPoints = KClosestPoints.FindClosestPoints(points, k);

        Debug.Log("距离原点最近的k个点如下：");
        foreach (Point point in closestPoints)
        {
            Debug.Log($"({point.X}, {point.Y})");
        }
    }

}
