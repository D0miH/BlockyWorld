using LibNoise.Generator;
using LibNoise;
using UnityEngine;

public class TerrainGenerator {
    // the perlin noise generator
    public Perlin perlin;

    // the maximum height of the terrain
    private float maxTerrainHeight;
    // the scale of the perlin noise. Higher value = smoother ; Lower value = sharper
    private float scale = 80f;

    public TerrainGenerator(float maxTerrainHeight) {
        // create the perlin generator
        perlin = new Perlin();

        this.maxTerrainHeight = maxTerrainHeight;
    }

    public float GetTerrainHeight(Vector3 position) {
        double perlinValue = perlin.GetValue(position.x / scale, 0, position.z / scale);

        return (float)perlinValue * (maxTerrainHeight);
    }
}
