using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour {

    // define the dimensions of the chunk
    public const int chunkSize = 3;
    // indicates whether the mesh should be updated
    public bool updateMesh = true;
    // the position of the chunk in world coordinates
    public Vector3 chunkPos;

    // the variables for the mesh components
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    // the array to store the blocks of the chunk
    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];

    // the mesh of this chunk
    ChunkMesh chunkMesh;

    // Start is called before the first frame update
    void Start() {
        // get the mesh components and assign them
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        chunkMesh = new ChunkMesh();
    }

    // Update is called once per frame
    void Update() {
        if (updateMesh) {
            UpdateChunkMesh();
            RenderChunkMesh();
            updateMesh = false;
        }
    }

    /// <summary>
    /// Returns the block at the given position. The position should be given in chunk coordinates.
    /// </summary>
    /// <param name="blockPos">The given block position in the chunk</param>
    public Block GetBlock(Vector3 blockPos) {
        // if the block is not in the range of the chunk, return an air block in order to render the outer blocks of the chunk
        if (!InsideChunk(blockPos)) {
            return new AirBlock(blockPos);
        }

        return blocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z];
    }

    /// <summary>
    /// Sets the given block at the given position.
    /// </summary>
    /// <param name="blockPos">The position in chunk coordinates</param>
    /// <param name="block">The given block</param>
    public void SetBlock(Vector3 blockPos, Block block) {
        if (!InsideChunk(blockPos)) {
            // if the position is not in the block just return
            return;
        }

        blocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = block;
    }

    /// <summary>
    /// Returns true if the given position is inside the chunk.
    /// </summary>
    /// <param name="pos">The position in chunk coordinates</param>
    /// <returns>Returns true if the position is inside the chunk</returns>
    bool InsideChunk(Vector3 pos) {
        return (pos.x >= 0 && pos.x < chunkSize && pos.y >= 0 && pos.y < chunkSize && pos.z >= 0 && pos.z < chunkSize);
    }

    void UpdateChunkMesh() {
        // iterate through the chunk and get the block data from every block
        for (int x = 0; x < chunkSize; x++) {
            for (int z = 0; z < chunkSize; z++) {
                for (int y = 0; y < chunkSize; y++) {
                    blocks[x, y, z].AddBlockToChunk(this, ref chunkMesh);
                }
            }
        }
    }

    void RenderChunkMesh() {
        // add the mesh to the mesh filter
        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = chunkMesh.verticesList.ToArray();
        meshFilter.mesh.triangles = chunkMesh.trianglesList.ToArray();
        meshFilter.mesh.uv = chunkMesh.uvList.ToArray();

        // add the collider mesh to the mesh collider
        meshCollider.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = chunkMesh.colliderVertices.ToArray();
        mesh.triangles = chunkMesh.colliderTriangles.ToArray();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}
