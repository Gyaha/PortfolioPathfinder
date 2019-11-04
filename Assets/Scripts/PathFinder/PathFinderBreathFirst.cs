using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderBreathFirst : PathFinder
{
    public PathFinderBreathFirst(PathManager pathManager) : base(pathManager) { }

    protected override void RunPathfinder()
    {
        bool[] searched = new bool[width * height];
        for (int i = 0; i < searched.Length; i++) searched[i] = false;
        searched[NodeIndex(nodeOrigen)] = true;

        Vector2Int[] cameFrom = new Vector2Int[width * height];

        List<Vector2Int> searchQueue = new List<Vector2Int>();
        searchQueue.Add(nodeOrigen);
        
        while (searchQueue.Count > 0)
        {
            Vector2Int nodeCurrent = searchQueue[0];
            searchQueue.RemoveAt(0);

            dots.Add(nodeCurrent);

            if (CompareNodes(nodeCurrent, nodeTarget))
            {
                path = ReconstructPath(cameFrom, nodeCurrent, nodeOrigen);
                return;
            }

            foreach (Vector2Int nodeNeighbor in GetNeighbors(nodeCurrent))
            {
                bool nodeSearched = searched[NodeIndex(nodeNeighbor)];

                if (nodeSearched == false)
                {
                    cameFrom[NodeIndex(nodeNeighbor)] = nodeCurrent;
                    searched[NodeIndex(nodeNeighbor)] = true;
                    searchQueue.Add(nodeNeighbor);
                }
            }
        }
    }
}
