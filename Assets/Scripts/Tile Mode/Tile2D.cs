using UnityEngine;
public enum WaterState
{
    Empty,
    Pouring,
    HalfFilled,
    Filled,
}

public class Tile2D : MonoBehaviour
{
    WaterState state = WaterState.Empty;
    public WaterState State
    {
        get => state;
        set
        {
            if (state == value) return;
            state = value;
            switch (state)
            {
                case WaterState.Empty:
                    spriteRenderer.color = Color.white;
                    break;
                case WaterState.Pouring:
                    spriteRenderer.color = Color.cyan;
                    break;
                case WaterState.HalfFilled:
                    spriteRenderer.color = Color.blue;
                    break;
                case WaterState.Filled:
                    spriteRenderer.color = Color.blue * 0.5f;
                    break;
            }
        }
    }

    public SpriteRenderer spriteRenderer;
    public Vector2Int pos;
    public Tile2D left, right, up, down, source;
    public override string ToString() => $"Tile2D({pos.x}, {pos.y} State: {state})";
}
