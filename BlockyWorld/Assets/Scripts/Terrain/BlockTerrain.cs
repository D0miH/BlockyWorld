using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTerrain : MonoBehaviour {

    // the prefab for instantiating chunks
    public GameObject chunkPrefab;

    // size of the terrain in chunks
    int terrainWidth = 10;
    int terrainHeight = 2;

    // dictionary to save all the chunks with their world position as key
    Dictionary<Vector3, Chunk> chunks = new Dictionary<Vector3, Chunk>();

    void Start() {
        for (int x = 0; x < terrainWidth; x++) {
            for (int z = 0; z < terrainWidth; z++) {
                for (int y = 0; y < terrainHeight; y++) {
                    CreateChunk(new Vector3(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize));
                }
            }
        }
    }

    /// <summary>
    /// Sets the given block to the given position.
    /// </summary>
    /// <param name="blockPos">The given position in world coordinates</param>
    /// <param name="block">The given block</param>
    void SetBlock(Vector3 blockPos, Block block) {
        Chunk chunk = GetChunk(blockPos);

        if (chunk != null) {
            // if the chunk was found, set the block using chunk coordinates
            chunk.SetBlock(blockPos - chunk.chunkPos, block);
        }
    }

    /// <summary>
    /// Gets the chunk which contains the given position. 
    /// If there was no chunk found, the function returns null.
    /// </summary>
    /// <param name="pos">The given position</param>
    /// <returns>The chunk which the given position is located in</returns>
    Chunk GetChunk(Vector3 pos) {
        // round to the next chunk position
        float xPos = (pos.x / Chunk.chunkSize) * Chunk.chunkSize;
        float yPos = (pos.y / Chunk.chunkSize) * Chunk.chunkSize;
        float zPos = (pos.z / Chunk.chunkSize) * Chunk.chunkSize;
        Vector3 chunkPos = new Vector3(xPos, yPos, zPos);

        // try to get the value
        Chunk resultChunk = null;
        chunks.TryGetValue(chunkPos, out resultChunk);

        return resultChunk;
    }

    /// <summary>
    /// Creates a chunk at the given position.
    /// </summary>
    /// <param name="chunkPos"></param>
    void CreateChunk(Vector3 chunkPos) {
        // instantiate the chunk and add it as a child to the block terrain
        GameObject newChunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity);
        newChunk.transform.parent = transform;

        // get the chunk component
        Chunk chunk = newChunk.GetComponent<Chunk>();
        // set the position of the chunk
        chunk.chunkPos = chunkPos;

        chunks.Add(chunkPos, chunk);

        for (int x = 0; x < Chunk.chunkSize; x++) {
            for (int z = 0; z < Chunk.chunkSize; z++) {
                for (int y = 0; y < Chunk.chunkSize; y++) {
                    chunk.blocks[x, y, z] = new GrassBlock(new Vector3(x, y, z));
                }
            }
        }
    }
}
