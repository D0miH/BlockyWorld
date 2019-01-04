using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {

    // define the face sides of the block
    public enum FaceDirection { up, down, east, west, north, south };

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
        AddBlockFace(FaceDirection.up, ref chunkMesh);
        AddBlockFace(FaceDirection.down, ref chunkMesh);
        AddBlockFace(FaceDirection.east, ref chunkMesh);
        AddBlockFace(FaceDirection.west, ref chunkMesh);
        AddBlockFace(FaceDirection.north, ref chunkMesh);
        AddBlockFace(FaceDirection.south, ref chunkMesh);
    }

    /// <summary>
    /// Adds the face which faces the given direction to the given chunk mesh. 
    /// </summary>
    /// <param name="direction">The direction of the face</param>
    /// <param name="chunkMesh">The reference to the chunk mesh that is going to be manipulated</param>
    void AddBlockFace(FaceDirection direction, ref ChunkMesh chunkMesh) {
        Vector3 point1;
        Vector3 point2;
        Vector3 point3;
        Vector3 point4;
        // assign the points depending on the directio of the face
        switch (direction) {
            case FaceDirection.south:
                point1 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper left front point
                point2 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper right front point
                point3 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower right front point
                point4 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower left front point
                break;
            case FaceDirection.north:
                point1 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper right back point
                point2 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper left back point
                point3 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower left back point
                point4 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower right back poin
                break;
            case FaceDirection.west:
                point1 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper left back point
                point2 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper left front point
                point3 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower left front point
                point4 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower left back point
                break;
            case FaceDirection.east:
                point1 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper right front point
                point2 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper right back point
                point3 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower right back point
                point4 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower right front poin
                break;
            case FaceDirection.down:
                point1 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower left front point
                point2 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z - 0.5f); // lower right front point
                point3 = new Vector3(blockPos.x + 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // lower right back point
                point4 = new Vector3(blockPos.x - 0.5f, blockPos.y - 0.5f, blockPos.z + 0.5f); // upper left front point
                break;
            default: // default case is to add the upper face
                point1 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper left back point
                point2 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z + 0.5f); // upper right back point
                point3 = new Vector3(blockPos.x + 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper right front point
                point4 = new Vector3(blockPos.x - 0.5f, blockPos.y + 0.5f, blockPos.z - 0.5f); // upper left front point
                break;
        }

        chunkMesh.AddFace(point1, point2, point3, point4);
    }
}
