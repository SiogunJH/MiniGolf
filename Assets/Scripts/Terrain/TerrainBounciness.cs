using System.Collections.Generic;

namespace TerrainLib
{
    public static class TerrainBounciness
    {
        public static Dictionary<TerrainType, float> Dict = new()
    {
        { TerrainType.Wood, 0.6f },
        { TerrainType.Plastic, 0.5f },
        { TerrainType.Grass, 0.25f },
        { TerrainType.OutOfBound, 0.25f },
        { TerrainType.Hole, 0.1f },
        { TerrainType.Sand, 0.025f },
        { TerrainType.End, 0f }
    };
    }
}