using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
    public bool Walkable;
    public Vector3 WorldPosition;
    public int GridX;
    public int GridY;

    public int GCost;
    public int HCost;
    public AStarNode Parent; 

    public AStarNode(bool _Walkable, Vector3 _WorldPos, int _GridX, int _GridY)
    {
        Walkable = _Walkable;
        WorldPosition = _WorldPos;
        GridX = _GridX;
        GridY = _GridY;
    }

    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }
}
