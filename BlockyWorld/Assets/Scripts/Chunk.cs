using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour {

    // define the dimensions of the chunk
    public const int chunkSize = 3;

    // the variables for the mesh components
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    // the array to store the blocks of the chunk
    Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];

    // the mesh of this chunk
    ChunkMesh chunkMesh;

    // Start is called before the first frame update
    void Start() {
        // get the mesh components and assign them
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        chunkMesh = new ChunkMesh();

        for (int x = 0; x < chunkSize; x++) {
            for (int z = 0; z < chunkSize; z++) {
                for (int y = 0; y < chunkSize; y++) {
                    blocks[x, y, z] = new GrassBlock(new Vector3(x, y, z));
                }
            }
        }

        blocks[chunkSize - 1, chunkSize - 1, chunkSize - 1] = new AirBlock(new Vector3(chunkSize - 1, chunkSize - 1, chunkSize - 1));
        blocks[chunkSize - 1, chunkSize - 1, 0] = new AirBlock(new Vector3(chunkSize - 1, chunkSize - 1, 0));
        blocks[0, chunkSize - 1, 0] = new AirBlock(new Vector3(0, chunkSize - 1, 0));
        blocks[0, chunkSize - 1, chunkSize - 1] = new AirBlock(new Vector3(0, chunkSize - 1, chunkSize - 1));

        UpdateChunkMesh();
        RenderChunkMesh();
    }

    // Update is called once per frame
    void Update() {
    }

    /// <summary>
    /// Returns the block at the given position. The position should be given in chunk coordinates.
    /// </summary>
    /// <param name="blockPos">The given block position in the chunk</param>
    public Block GetBlock(Vector3 blockPos) {
        // if the block is not in the range of the chunk, return an air block in order to render the outer blocks of the chunk
        if (blockPos.x < 0 || blockPos.x >= chunkSize
        || blockPos.y < 0 || blockPos.y >= chunkSize
        || blockPos.z < 0 || blockPos.z >= chunkSize) {
            return new AirBlock(blockPos);
        }

        return blocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z];
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
