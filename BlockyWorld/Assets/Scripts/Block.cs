using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {

    // define the face sides of the block
    public enum FaceDirections { up, down, east, west, north, south };

    // the position of the block in chunk coordinates
    public Vector3 blockPos;

    /// <summary>
    /// The constructor of the block class.
    /// </summary>
    /// <param name="pos">The position of the block in chunk coordinates</param>
    public Block(Vector3 pos) {
        blockPos = pos;
    }

    /// <summary>
    /// Adds the mesh data of the block to the given chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The given chunk mesh</param>
    /// <returns></returns>
    public void AddBlockToChunk(ref ChunkMesh chunkMesh) {
        AddBlockFaceUp(ref chunkMesh);
        AddBlockFaceDown(ref chunkMesh);
        AddBlockFaceEast(ref chunkMesh);
        AddBlockFaceWest(ref chunkMesh);
        AddBlockFaceNorth(ref chunkMesh);
        AddBlockFaceSouth(ref chunkMesh);
    }


    /// <summary>
    /// Adds the data for the up-facing face of the block to the chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFaceUp(ref ChunkMesh chunkMesh) {
        // calculate the vertices of the face
        Vector3 point1 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper left back point of cube
        Vector3 point2 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper right back point
        Vector3 point3 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper right front point
        Vector3 point4 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper left front point

        // add them to the mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
    }

    /// <summary>
    /// Adds the data for the down-facing face of the block to the chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFaceDown(ref ChunkMesh chunkMesh) {
        // calculate the vertices of the face
        Vector3 point1 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower left front point
        Vector3 point2 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower right front point
        Vector3 point3 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower right back point
        Vector3 point4 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // upper left front point

        // add them to the mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
    }

    /// <summary>
    /// Adds the data for the east-facing face of the block to the chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFaceEast(ref ChunkMesh chunkMesh) {
        // calculate the vertices of the face
        Vector3 point1 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper right front point
        Vector3 point2 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper right back point
        Vector3 point3 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower right back point
        Vector3 point4 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower right front point

        // add them to the mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
    }

    /// <summary>
    /// Adds the data for the west-facing face of the block to the chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFaceWest(ref ChunkMesh chunkMesh) {
        // calculate the vertices of the face
        Vector3 point1 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper left back point
        Vector3 point2 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper left front point
        Vector3 point3 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower left front point
        Vector3 point4 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower left back point

        // add them to the mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
    }

    /// <summary>
    /// Adds the data for the north-facing face of the block to the chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFaceNorth(ref ChunkMesh chunkMesh) {
        // calculate the vertices of the face
        Vector3 point1 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper right back point
        Vector3 point2 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper left back point
        Vector3 point3 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower left back point
        Vector3 point4 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower right back point

        // add them to the mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
    }

    /// <summary>
    /// Adds the data for the south-facing face of the block to the chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFaceSouth(ref ChunkMesh chunkMesh) {
        // calculate the vertices of the face
        Vector3 point1 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper left front point
        Vector3 point2 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper right front point
        Vector3 point3 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower right front point
        Vector3 point4 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower left front point

        // add them to the mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
    }
}
