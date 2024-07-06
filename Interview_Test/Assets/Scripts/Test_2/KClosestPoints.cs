using System.Collections;
using System.Collections.Generic;
using System;

public class KClosestPoints
{
    /// <summary>
    /// 计算距离原点最近的K个点
    /// </summary>
    /// <param name="points"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static List<Point> FindClosestPoints(Point[] points, int k)
    {
        // 如果点的数量小于或等于K，直接返回所有点
        if (points.Length <= k)
        {
            return new List<Point>(points);
        }

        // 初始化一个列表来存储距离原点最近的K个点
        List<Point> closestPoints = new List<Point>();

        // 选择排序算法找到距离原点最近的K个点
        for (int i = 0; i < k; i++)
        {
            // 假设当前点是最近的点
            int closestIndex = i;
            for (int j = i + 1; j < points.Length; j++)
            {
                // 如果找到一个更近的点，则更新最近点的索引
                if (points[j].DistanceToOrigin() < points[closestIndex].DistanceToOrigin())
                {
                    closestIndex = j;
                }
            }

            // 将找到的最近点添加到列表中
            closestPoints.Add(points[closestIndex]);

            // 交换当前点和找到的最近点的位置
            Point temp = points[i];
            points[i] = points[closestIndex];
            points[closestIndex] = temp;
        }

        return closestPoints;
    }
}