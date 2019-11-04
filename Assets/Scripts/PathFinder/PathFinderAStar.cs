using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PathFinderAStar : PathFinder
{
    public PathFinderAStar(PathManager pathManager) : base(pathManager) { }

    protected override void RunPathfinder()
    {
        float[] gScore = new float[width * height];
        for (int i = 0; i < gScore.Length; i++) gScore[i] = float.PositiveInfinity;
        gScore[NodeIndex(nodeOrigen)] = 0;

        float[] fScore = new float[width * height];
        for (int i = 0; i < fScore.Length; i++) fScore[i] = float.PositiveInfinity;
        fScore[NodeIndex(nodeOrigen)] = Distance(nodeOrigen, nodeTarget);

        Vector2Int[] cameFrom = new Vector2Int[width * height];

        List<Vector2Int> openSet = new List<Vector2Int>();
        openSet.Add(nodeOrigen);

        while (openSet.Count > 0)
        {
            openSet = openSet.OrderBy(node => fScore[NodeIndex(node)]).ToList();

            Vector2Int nodeCurrent = openSet[0];
            openSet.RemoveAt(0);

            dots.Add(nodeCurrent);

            if (CompareNodes(nodeCurrent, nodeTarget))
            {
                path = ReconstructPath(cameFrom, nodeCurrent, nodeOrigen);
                return;
            }

            foreach (Vector2Int nodeNeighbor in GetNeighbors(nodeCurrent))
            {
                float nodeGScore = gScore[NodeIndex(nodeCurrent)] + Distance(nodeCurrent, nodeNeighbor);

                if (nodeGScore < gScore[NodeIndex(nodeNeighbor)])
                {
                    cameFrom[NodeIndex(nodeNeighbor)] = nodeCurrent;
                    gScore[NodeIndex(nodeNeighbor)] = nodeGScore;
                    fScore[NodeIndex(nodeNeighbor)] = nodeGScore + Distance(nodeNeighbor, nodeTarget);

                    if (openSet.Contains(nodeNeighbor) == false)
                    {
                        openSet.Add(nodeNeighbor);
                    }
                }
            }
        }
    }

}
