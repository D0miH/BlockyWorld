using LibNoise.Generator;
using LibNoise;
using UnityEngine;

public class TerrainGenerator {
    // the perlin noise generator
    public Perlin perlin;

    private float stoneBaseHeight = -20f;    // the minimum height for the stone layer
    private float stoneBaseScale = 150f;    // the scale of the perlin noise. Higher value = smoother ; Lower value = sharper 
    private float stoneHeightDiff = 5f;      // maximum difference between peaks and valleys 

    private float dirtBaseScale = 80f;
    private float dirtHeightDiff = 30f;

    private float grassBaseScale = 80f;
    private float grassHeightDiff = 5f;

    public TerrainGenerator() {
        // create the perlin generator
        perlin = new Perlin();
    }

    public void GenerateChunk(ref Chunk chunk) {
        for (int x = 0; x < Chunk.chunkSize; x++) {
            for (int z = 0; z < Chunk.chunkSize; z++) {
                GenerateChunkColumn(x, z, ref chunk);
            }
        }
    }

    public void GenerateChunkColumn(int x, int z, ref Chunk chunk) {

        int stoneHeight = Mathf.FloorToInt(stoneBaseHeight) + GetNoise(x + chunk.chunkPos.x, 0, z + chunk.chunkPos.z, stoneBaseScale, stoneHeightDiff);

        int dirtHeight = stoneHeight + GetNoise(x + chunk.chunkPos.x, 0, z + chunk.chunkPos.z, dirtBaseScale, dirtHeightDiff);

        int grassheight = dirtHeight + GetNoise(x + chunk.chunkPos.x, 0, z + chunk.chunkPos.z, grassBaseScale, grassHeightDiff);

        for (int y = 0; y < Chunk.chunkSize; y++) {
            // get the block position in the chunk and the block height in world coordinates
            Vector3 blockPos = new Vector3(x, y, z);
            float blockHeight = blockPos.y + chunk.chunkPos.y;

            if (blockHeight <= stoneHeight) {
                chunk.SetBlock(blockPos, new StoneBlock(blockPos));
            } else if (blockHeight <= dirtHeight && blockHeight > stoneHeight) {
                chunk.SetBlock(blockPos, new DirtBlock(blockPos));
            } else if (blockHeight <= grassheight && blockHeight > dirtHeight) {
                chunk.SetBlock(blockPos, new GrassBlock(blockPos));
            } else {
                chunk.SetBlock(blockPos, new AirBlock(blockPos));
            }
        }
    }

    private int GetNoise(float x, float y, float z, float scale, float maxValue) {
        float perlinValue = (float)perlin.GetValue(x / scale, 0, z / scale);

        return Mathf.FloorToInt((perlinValue + 1f) * maxValue / 2f);
    }
}
