using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑模块_数据存储实体类
/// </summary>
public class BuildItem  {

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string posX;

    public string PosX
    {
        get { return posX; }
        set { posX = value; }
    }
    private string posY;

    public string PosY
    {
        get { return posY; }
        set { posY = value; }
    }
    private string posZ;

    public string PosZ
    {
        get { return posZ; }
        set { posZ = value; }
    }

    private string rotX;

    public string RotX
    {
        get { return rotX; }
        set { rotX = value; }
    }
    private string rotY;

    public string RotY
    {
        get { return rotY; }
        set { rotY = value; }
    }
    private string rotZ;

    public string RotZ
    {
        get { return rotZ; }
        set { rotZ = value; }
    }
    private string rotW;

    public string RotW
    {
        get { return rotW; }
        set { rotW = value; }
    }


    public BuildItem() { }
    public BuildItem(string name, string posX, string posY, string posZ, string rotX, string rotY, string rotZ, string rotW)
    {
        this.name = name;
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;

        this.rotX = rotX;
        this.rotY = rotY;
        this.rotZ = rotZ;
        this.rotW = rotW;
    }

}
