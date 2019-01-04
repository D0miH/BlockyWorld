using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour {

    // define the dimensions of the chunk
    public const int chunkSize = 1;

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


        blocks[0, 0, 0] = new Block(new Vector3(0, 0, 0));

        UpdateChunkMesh();
        RenderChunkMesh();
    }

    // Update is called once per frame
    void Update() {
    }

    void UpdateChunkMesh() {
        // iterate through the chunk and get the block data from every block
        for (int x = 0; x < chunkSize; x++) {
            for (int z = 0; z < chunkSize; z++) {
                for (int y = 0; y < chunkSize; y++) {
                    blocks[x, y, z].AddBlockToChunk(ref chunkMesh);
                }
            }
        }
    }

    void RenderChunkMesh() {
        // add the mesh to the mesh filter
        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = chunkMesh.verticesList.ToArray();
        meshFilter.mesh.triangles = chunkMesh.trianglesList.ToArray();

        // add the collider mesh to the mesh collider
        meshCollider.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = chunkMesh.colliderVertices.ToArray();
        mesh.triangles = chunkMesh.colliderTriangles.ToArray();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}
