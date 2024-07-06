using System;

/// <summary>
/// 坐标点类
/// </summary>
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// 计算点到原点的距离
    /// </summary>
    /// <returns></returns>
    public double DistanceToOrigin()
    {
        return Math.Sqrt(X * X + Y * Y);
    }
}