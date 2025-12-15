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
    public Vector2Int Pos;
    public bool IsPipe;         // pipe or container


    public void Init(Vector2Int pos, bool isPipe)
    {
        Pos = pos;
        IsPipe = isPipe;
    }
}
