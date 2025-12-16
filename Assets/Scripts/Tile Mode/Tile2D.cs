using UnityEngine;
public enum WaterState
{
    Empty,
    Pouring,
    Filled
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
                case WaterState.Filled:
                    spriteRenderer.color = Color.blue;
                    break;
            }
        }
    }

    public SpriteRenderer spriteRenderer;
    public Vector2Int pos;
    // public bool isPipe;         // pipe or container
    public Tile2D left, right, up, down, source;
}
