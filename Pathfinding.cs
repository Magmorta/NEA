using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform Seeker, Target;

    AStarGrid Grid;

    private void Awake()
    {
        Grid = GetComponent<AStarGrid>();
    }

    private void Update()
    {
        FindPath(Seeker.position, Target.position);
    }

    void FindPath(Vector3 StartPosition, Vector3 TargetPosition)
    {
        AStarNode StartNode = Grid.NodeFromWorldPoint(StartPosition);
        AStarNode TargetNode = Grid.NodeFromWorldPoint(TargetPosition);

        List<AStarNode> OpenSet = new List<AStarNode>();            //Create the open list.
        HashSet<AStarNode> ClosedSet = new HashSet<AStarNode>();    //Create the closed list.

        OpenSet.Add(StartNode);

        while (OpenSet.Count > 0)
        {
            AStarNode CurrentNode = OpenSet[0];
            for (int i = 1; i <OpenSet.Count; i ++)
            {
                if (OpenSet[i].FCost < CurrentNode.FCost || OpenSet[i].FCost == CurrentNode.FCost && OpenSet[i].HCost < CurrentNode.HCost)
                {
                    CurrentNode = OpenSet[i];
                }
            }

            OpenSet.Remove(CurrentNode);
            ClosedSet.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                RetracePath(StartNode, TargetNode);
                return;
            }

            foreach (AStarNode Neighbour in Grid.GetNeighbours(CurrentNode))
            {
                if (!Neighbour.Walkable || ClosedSet.Contains(Neighbour))
                {
                    continue;
                }
                int NewMovementCostToNeighbour = CurrentNode.GCost + GetDistance(CurrentNode, Neighbour);
                if (NewMovementCostToNeighbour < Neighbour.GCost || !OpenSet.Contains(Neighbour))
                {
                    Neighbour.GCost = NewMovementCostToNeighbour;
                    Neighbour.HCost = GetDistance(Neighbour, TargetNode);
                    Neighbour.Parent = CurrentNode;
                    if (!OpenSet.Contains(Neighbour))
                    {
                        OpenSet.Add(Neighbour);
                    }
                }
            }
        }
    }
    void RetracePath(AStarNode StartNode, AStarNode EndNode)
    {
        List<AStarNode> Path = new List<AStarNode>();
        AStarNode CurrentNode = EndNode;

        while (CurrentNode != StartNode)    //Retraces the path backwards and adds it to a list.
        {
            Path.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        Path.Reverse();     //It is now the right way around.

        Grid.Path = Path;
    }

    int GetDistance(AStarNode NodeA, AStarNode NodeB)
    {
        int DstX = Mathf.Abs(NodeA.GridX - NodeB.GridX);
        int DstY = Mathf.Abs(NodeA.GridY - NodeB.GridY);

        if (DstX > DstY)
            return 14 * DstY + 10 * (DstX - DstY);
        return 14 * DstX + 10 * (DstY - DstX);
    }
}
