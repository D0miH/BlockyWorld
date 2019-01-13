using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTerrain : MonoBehaviour {

    // the prefab for instantiating chunks
    public GameObject chunkPrefab;

    // the terrain generator
    TerrainGenerator terrainGenerator;

    // size of the terrain in chunks
    int terrainWidth = 20;
    int terrainHeight = 10;

    // dictionary to save all the chunks with their world position as key
    Dictionary<Vector3, Chunk> chunks = new Dictionary<Vector3, Chunk>();

    void Start() {
        for (int x = -terrainWidth; x <= terrainWidth; x++) {
            for (int z = -terrainWidth; z <= terrainWidth; z++) {
                for (int y = -terrainHeight; y <= terrainHeight; y++) {
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
    public void SetBlock(Vector3 blockPos, Block block) {
        Chunk chunk = GetChunk(blockPos);

        if (chunk != null) {
            // if the chunk was found, set the block using chunk coordinates
            chunk.SetBlock(blockPos - chunk.chunkPos, block);
            chunk.updateMesh = true;

            UpdateAdjacentChunks(blockPos, chunk.chunkPos);
        }
    }

    /// <summary>
    /// Gets a block at a given position and returns it. 
    /// If there was no block found at the given position, an air block is returned.
    /// </summary>
    /// <param name="blockPos">The position of the block in world coordinates</param>
    /// <returns>The block at the given position</returns>
    public Block GetBlock(Vector3 blockPos) {
        // get the chunk in which the block is located
        Chunk chunk = GetChunk(blockPos);

        // if the chunk was not found, return an air block
        if (chunk == null) {
            return new AirBlock(blockPos);
        }

        // else get the block from the chunk
        return chunk.GetBlock(blockPos - chunk.chunkPos);
    }

    /// <summary>
    /// Gets the chunk which contains the given position. 
    /// If there was no chunk found, the function returns null.
    /// </summary>
    /// <param name="pos">The given position</param>
    /// <returns>The chunk which the given position is located in</returns>
    Chunk GetChunk(Vector3 pos) {
        // round to the next chunk position
        float xPos = Mathf.FloorToInt(pos.x / Chunk.chunkSize) * Chunk.chunkSize;
        float yPos = Mathf.FloorToInt(pos.y / Chunk.chunkSize) * Chunk.chunkSize;
        float zPos = Mathf.FloorToInt(pos.z / Chunk.chunkSize) * Chunk.chunkSize;
        Vector3 chunkPos = new Vector3(xPos, yPos, zPos);

        // try to get the value
        Chunk resultChunk = null;
        chunks.TryGetValue(chunkPos, out resultChunk);

        return resultChunk;
    }

    /// <summary>
    /// Checks whether the adjacent chunks need to be updated when changing the block at the given position.
    /// </summary>
    /// <param name="blockPos">The given position of the altered block</param>
    /// <param name="chunkPos">The position of the chunk in which the block is located</param>
    void UpdateAdjacentChunks(Vector3 blockPos, Vector3 chunkPos) {
        Vector3 chunkBlockPos = blockPos - chunkPos;

        // east chunk and west chunk
        if (chunkBlockPos.x == 0) {
            Chunk chunk = GetChunk(new Vector3(blockPos.x - 1, blockPos.y, blockPos.z));
            if (chunk != null) {
                chunk.updateMesh = true;
            }
        }
        if (chunkBlockPos.x == Chunk.chunkSize - 1) {
            Chunk chunk = GetChunk(new Vector3(blockPos.x + 1, blockPos.y, blockPos.z));
            if (chunk != null) {
                chunk.updateMesh = true;
            }
        }
        // lower and upper chunk
        if (chunkBlockPos.y == 0) {
            Chunk chunk = GetChunk(new Vector3(blockPos.x, blockPos.y - 1, blockPos.z));
            if (chunk != null) {
                chunk.updateMesh = true;
            }
        }
        if (chunkBlockPos.y == Chunk.chunkSize - 1) {
            Chunk chunk = GetChunk(new Vector3(blockPos.x, blockPos.y + 1, blockPos.z));
            if (chunk != null) {
                chunk.updateMesh = true;
            }
        }
        // south and north chunk
        if (chunkBlockPos.z == 0) {
            Chunk chunk = GetChunk(new Vector3(blockPos.x, blockPos.y, blockPos.z - 1));
            if (chunk != null) {
                chunk.updateMesh = true;
            }
        }
        if (chunkBlockPos.z == Chunk.chunkSize - 1) {
            Chunk chunk = GetChunk(new Vector3(blockPos.x, blockPos.y, blockPos.z + 1));
            if (chunk != null) {
                chunk.updateMesh = true;
            }
        }

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
        // set the reference to the block terrain
        chunk.blockTerrain = this;

        chunks.Add(chunkPos, chunk);

        terrainGenerator = new TerrainGenerator();
        terrainGenerator.GenerateChunk(ref chunk);
    }
}
