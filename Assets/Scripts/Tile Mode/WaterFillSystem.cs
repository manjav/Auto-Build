using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterFillSystem : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Tile2D tileSource;

    void Start()
    {
        var grid = BuildTiles2D();
        StartCoroutine(Pour(null, grid[new(3, 0)]));
    }

    public Dictionary<Vector2Int, Tile2D> BuildTiles2D()
    {
        var grid = new Dictionary<Vector2Int, Tile2D>();

        void Add(int x, int y)
        {
            var tile = Instantiate(tileSource, new Vector3(x - 3, y, 0), Quaternion.identity, transform);
            tile.pos = new Vector2Int(x, y);
            grid[tile.pos] = tile;
        }
        List<Vector3Int> pipePositions = new()
        {
            new (3, 0),
            new (3, 1),
            new (2, 2),
            new (3, 2),
            new (4, 2),
            new (5, 2),
            new (1, 2),
            new (1, 3),
            new (0, 4, 1),
            new (1, 4),
            new (2, 4, 1),
            new (0, 5, 1),
            new (1, 5),
            new (2, 5, 1),
            new (0, 6, 1),
            new (1, 6),
            new (2, 6, 1),
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
            new (3, 12, 1),
            new (4, 12),
            new (5, 12, 1),
            new (3, 13, 1),
            new (4, 13),
            new (5, 13, 1),
            new (3, 14, 1),
            new (4, 14),
            new (5, 14, 1),
            new (7, 5),
            new (8, 5),
            new (9, 5),
            new (9, 6),
            new (9, 7),
            new (8, 8,1),
            new (9, 8),
            new (10,8, 1),
            new (8, 9, 1),
            new (9, 9),
            new (10, 9, 1),
            new (8, 10, 1),
            new (9, 10),
            new (10,10, 1),
        };

        foreach (var item in pipePositions)
        {
            Add(item.x, -item.y);
        }

        foreach (var item in grid)
        {
            var p = item.Value.pos;
            p.x -= 1;
            item.Value.left = grid.ContainsKey(p) ? grid[p] : null;
            p.x += 2;
            item.Value.right = grid.ContainsKey(p) ? grid[p] : null;
            p.x -= 1; p.y += 1;
            item.Value.up = grid.ContainsKey(p) ? grid[p] : null;
            p.y -= 2;
            item.Value.down = grid.ContainsKey(p) ? grid[p] : null;
        }

        return grid;
    }

    bool IsWallOrFilled(Tile2D source, Tile2D side)
    {
        return side == null || side.State == WaterState.Filled || source.source == side;
    }

    IEnumerator Pour(Tile2D source, Tile2D tile)
    {
        if (tile == null || tile.State != WaterState.Empty)
            yield break;

        tile.source = source;
        tile.State = WaterState.Pouring;
        yield return new WaitForSeconds(speed);
        if (!IsWallOrFilled(tile, tile.down))
        {
            yield return StartCoroutine(Pour(tile, tile.down));
        }
        else
        {
            StartCoroutine(Pour(tile, tile.left));
            yield return StartCoroutine(Pour(tile, tile.right));
            if (IsWallOrFilled(tile, tile.left) && IsWallOrFilled(tile, tile.right))
            {
                yield return StartCoroutine(Fill(tile));
            }
        }

        yield return null;
    }

    IEnumerator Fill(Tile2D tile, bool returning = false)
    {
        if (tile == null || tile.State == WaterState.Filled)
            yield break;

        tile.State = WaterState.Filled;
        yield return new WaitForSeconds(speed);
        if (IsWallOrFilled(tile, tile.down))
        {
            StartCoroutine(Fill(tile.left));
            yield return StartCoroutine(Fill(tile.right));
            if (IsWallOrFilled(tile, tile.left) && IsWallOrFilled(tile, tile.right))
            {
                yield return StartCoroutine(Fill(tile.up, true));
            }
        }
        else
        {
            yield return StartCoroutine(Pour(tile, tile.down));
        }
        yield break;
    }
}