using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterFillSystem : MonoBehaviour
{
    [SerializeField] Tile2D tileSource;
    readonly MinHeap<Tile2D> queue = new();
    readonly HashSet<Vector2Int> visited = new();
    static readonly Vector2Int[] dirctions =
    {
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
    };


    void Start()
    {
                // Source at top
        var water = GetComponent<WaterFillSystem>();
        water.StartFill(BuildTiles2D(), new Vector2Int(3, 0));

    }
    public Dictionary<Vector2Int, Tile2D> BuildTiles2D()
    {
        var grid = new Dictionary<Vector2Int, Tile2D>();

        void Add(int x, int y)
        {
            var pos = new Vector2Int(x, y);
            var tile = Instantiate(tileSource, new Vector3(x, y, 0), Quaternion.identity, transform);
            tile.Init(pos, true);
            grid[pos] = tile;
        }
        List<Vector2Int> pipePositions = new()
        {
            new (3, 0),
            new (3, 1),
            new (2, 2),
            new (3, 2),
            new (4, 2),
            new (5, 2),
            new (1, 2),
            new (1, 3),
            new (0, 4),
            new (1, 4),
            new (2, 4),
            new (0, 5),
            new (1, 5),
            new (2, 5),
            new (0, 6),
            new (1, 6),
            new (2, 6),
            new (1, 7),
            new (1, 8),
            new (1, 9),
            new (1, 10),
            new (6, 2),
            new (6, 3),
            new (6, 4),
            new (6, 5),
            new (6, 6),
            new (6, 7),
            new (6, 8),
            new (6, 9),
            new (6, 10),
            new (2, 10),
            new (3, 10),
            new (4, 10),
            new (5, 10),
            new (4, 11),
            new (3, 12),
            new (4, 12),
            new (5, 12),
            new (3, 13),
            new (4, 13),
            new (5, 13),
            new (3, 14),
            new (4, 14),
            new (5, 14),
            new (7, 5),
            new (8, 5),
            new (9, 5),
            new (9, 6),
            new (9, 7),
            new (8, 8),
            new (9, 8),
            new (10,8),
            new (8, 9),
            new (9, 9),
            new (10, 9),
            new (8, 10),
            new (9, 10),
            new (10,10),
        };

        foreach (var item in pipePositions)
        {
            Add(item.x, -item.y);
        }

        return grid;
    }

    public void StartFill(Dictionary<Vector2Int, Tile2D> grid, Vector2Int source) => StartCoroutine(FillRoutine(grid, source));

    IEnumerator FillRoutine(Dictionary<Vector2Int, Tile2D> grid, Vector2Int source)
    {
        if (!grid.ContainsKey(source))
            yield break;

        queue.Push(grid[source], grid[source].Pos.y);

        while (queue.Count > 0)
        {
            Tile2D tile = queue.Pop();
            if (visited.Contains(tile.Pos))
                continue;

            visited.Add(tile.Pos);
            tile.State = WaterState.Pouring;
            yield return new WaitForSeconds(0.1f);

            // 🔽 Gravity-aware expansion
            foreach (var d in dirctions)
            {
                Vector2Int nPos = tile.Pos + d;
                if (!grid.ContainsKey(nPos)) continue;

                Tile2D n = grid[nPos];
                if (!n.IsPipe || visited.Contains(nPos)) continue;
                    queue.Push(n, n.Pos.y);
            }
        }
    }
}