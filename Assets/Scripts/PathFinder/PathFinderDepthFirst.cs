using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderDepthFirst : PathFinder
{
    public PathFinderDepthFirst(PathManager pathManager) : base(pathManager) { }

    protected override void RunPathfinder()
    {
        bool[] searched = new bool[width * height];
        for (int i = 0; i < searched.Length; i++) searched[i] = false;

        Vector2Int[] cameFrom = new Vector2Int[width * height];

        List<Vector2Int> searchQueue = new List<Vector2Int>();
        searchQueue.Insert(0, nodeOrigen);

        while (searchQueue.Count > 0)
        {
            Vector2Int nodeCurrent = searchQueue[0];
            searchQueue.RemoveAt(0);

            if (CompareNodes(nodeCurrent, nodeTarget))
            {
                path = ReconstructPath(cameFrom, nodeCurrent, nodeOrigen);
                return;
            }

            bool nodeSearched = searched[NodeIndex(nodeCurrent)];
            if (nodeSearched == false)
            {
                dots.Add(nodeCurrent);

                searched[NodeIndex(nodeCurrent)] = true;
                foreach (Vector2Int nodeNeighbor in GetNeighborsSimple(nodeCurrent))
                {
                    if (searched[NodeIndex(nodeNeighbor)] == false)
                    {
                        cameFrom[NodeIndex(nodeNeighbor)] = nodeCurrent;
                    }

                    searchQueue.Insert(0, nodeNeighbor);
                }
            }
        }
    }

}
