using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize;
    public float NodeRadius;
    AStarNode[,] Grid;

    private float NodeDiameter;
    int GridSizeX, GridSizeY;

    private void Start()
    {
        NodeDiameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x / NodeDiameter);
        GridSizeY = Mathf.RoundToInt(GridWorldSize.y / NodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        Grid = new AStarNode[GridSizeX, GridSizeY];
        Vector3 WorldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2;

        for (int x = 0; x < GridSizeX; x ++)
        {
            for (int y = 0; y < GridSizeY; y ++)
            {
                Vector3 WorldPoint = WorldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.up * (y * NodeDiameter + NodeRadius);
                bool Walkable = !(Physics.CheckSphere(WorldPoint, NodeRadius,UnwalkableMask));
                Grid[x, y] = new AStarNode(Walkable, WorldPoint, x , y);
            }
        }
    }

    public List<AStarNode> GetNeighbours (AStarNode Node)
    {
        List<AStarNode> Neighbours = new List<AStarNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)   //Do not add the current node to neighbouring nodes.
                
                        continue;

                    int CheckX = Node.GridX + x;
                    int CheckY = Node.GridY + y;

                    if (CheckX >= 0 && CheckX < GridSizeX && CheckY >= 0 && CheckY < GridSizeY)
                    {
                        Neighbours.Add(Grid[CheckX, CheckY]);
                    }
                
            }
        }

        return Neighbours;
    }

    public AStarNode NodeFromWorldPoint(Vector3 WorldPosition)
    {
        float PercentX = (WorldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float PercentY = (WorldPosition.y + GridWorldSize.y / 2) / GridWorldSize.y;
        PercentX = Mathf.Clamp01(PercentX);                                             //If enemy is somehow outside the map, this will clamp the value such that errors aren't thrown.
        PercentY = Mathf.Clamp01(PercentY);

        int X = Mathf.RoundToInt((GridSizeX - 1) * PercentX);
        int Y = Mathf.RoundToInt((GridSizeY - 1) * PercentY);

        return Grid[X, Y];

    }
    public List<AStarNode> Path;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, GridWorldSize.y, 1));
        if (Grid != null)
        {
            foreach (AStarNode n in Grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                if (Path != null)
                {
                    if(Path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (NodeDiameter - 0.1f));
            }
        }
    }
}
