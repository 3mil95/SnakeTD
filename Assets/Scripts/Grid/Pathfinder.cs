using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    private Queue<PathNode> queue;
    private Dictionary<string, bool> visited;

    private PathNode addNextToQueue(PathNode node) {
        Vector2Int[] alts = new Vector2Int[]{ new Vector2Int(1,0), new Vector2Int(-1,0), new Vector2Int(0,1), new Vector2Int(0,-1)};

        foreach (Vector2Int alt in alts) {
            Vector2Int newIndex = node.index + alt;
            if (!GameGrid.grid.isGridCellFree(newIndex) || visited.ContainsKey(newIndex.x.ToString() + "," + newIndex.y.ToString())) {
                continue;
            }

            PathNode nextNode = new PathNode(node, newIndex);

            if (newIndex == new Vector2Int(0,0)) {
                return nextNode;
            }

            visited.Add(newIndex.x.ToString() + "," + newIndex.y.ToString(), true);
            queue.Enqueue(nextNode);
        }

        return null;
    }

    public List<Vector3> FindPath(Vector3 pos) {
        queue = new Queue<PathNode>();
        visited = new Dictionary<string, bool>();

        PathNode startNode = new PathNode(null, GameGrid.grid.WorldToGrid(pos));        
        PathNode endNode = addNextToQueue(startNode);

        while (endNode == null && queue.Count != 0) {
            PathNode node = queue.Dequeue();
            endNode = addNextToQueue(node);
        }

        if (endNode == null) return new List<Vector3>();
        DebugPath(endNode);

        return getPath(endNode);
    }

    private List<Vector3> getPath(PathNode endNode) {
        List<Vector3> path = new List<Vector3>();

        while (endNode.parent.parent != null) {
            path.Add(GameGrid.grid.GridToWorld(endNode.index, -1));
            endNode = endNode.parent;
        }

        if (path.Count == 0) {
            path.Add(GameGrid.grid.GridToWorld(new Vector2Int(0,0), -1));
        }

        return path;
    }


    private void DebugPath(PathNode endNode) {
        float cellSize = GameGrid.grid.celSize;

        while (endNode.parent != null) {

            Vector3 start = new Vector3(endNode.index.x * cellSize , endNode.index.y * cellSize, -3);
            Vector3 end = new Vector3(endNode.parent.index.x * cellSize, endNode.parent.index.y * cellSize, -3);

            Debug.DrawLine(start, end, Color.blue, 1);

            endNode = endNode.parent;
        }

    }
}


public class PathNode {

    public PathNode parent { get; private set;}
    public Vector2Int index { get; private set;}

    public PathNode(PathNode _parent, Vector2Int _index) {
        parent = _parent;
        index = _index;
    }
}

