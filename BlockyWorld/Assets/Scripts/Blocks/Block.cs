using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block {

    // define the face sides of the block
    public enum FaceDirection { up, down, east, west, north, south };
    // define the position of the texture in the texture atlas
    public struct TextureAtlasCoords {
        public int x;
        public int y;
    }
    // tile size is calculated by dividing 1 through the number of tiles per side
    const float tileSize = 0.25f;

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
    /// Returns the uv coordinates of the face.
    /// </summary>
    /// <param name="faceDir">The direction of the face</param>
    /// <returns>The uv coordinates</returns>
    public Vector2[] GetFaceUVCoordinates(FaceDirection faceDir) {
        Vector2[] uvCoords = new Vector2[4];

        // get the position of the texture
        TextureAtlasCoords texturePos = GetTextureTileCoords(faceDir);

        uvCoords[0] = new Vector2(tileSize * texturePos.x, tileSize * texturePos.y + tileSize);             // texture for upper left vertex
        uvCoords[1] = new Vector2(tileSize * texturePos.x + tileSize, tileSize * texturePos.y + tileSize);  // texture for upper right vertex
        uvCoords[2] = new Vector2(tileSize * texturePos.x + tileSize, tileSize * texturePos.y);             // texture for lower right vertex
        uvCoords[3] = new Vector2(tileSize * texturePos.x, tileSize * texturePos.y);                        // texture for lower left vertex

        return uvCoords;
    }

    /// <summary>
    /// Returns the texture atlas coordinates for the given face.
    /// </summary>
    /// <param name="faceDir">The direction of the face</param>
    /// <returns>The texture atlas coordinates of the face</returns>
    public abstract TextureAtlasCoords GetTextureTileCoords(FaceDirection faceDir);

    /// <summary>
    /// Returns whether the face with the given direction of the block is solid or not.
    /// </summary>
    /// <param name="faceDir">The direction of the face to check</param>
    /// <returns></returns>
    public abstract bool IsFaceSolid(FaceDirection faceDir);

    /// <summary>
    /// Adds the mesh data of the block to the given chunk mesh.
    /// </summary>
    /// <param name="chunkMesh">The given chunk mesh</param>
    /// <returns></returns>
    public virtual void AddBlockToChunk(Chunk chunk, ref ChunkMesh chunkMesh) {
        // check the block above and below
        Block upperBlock = chunk.GetBlock(new Vector3(blockPos.x, blockPos.y + 1, blockPos.z));
        Block lowerBlock = chunk.GetBlock(new Vector3(blockPos.x, blockPos.y - 1, blockPos.z));
        if (!upperBlock.IsFaceSolid(FaceDirection.down)) {
            AddBlockFace(FaceDirection.up, ref chunkMesh);
        }
        if (!lowerBlock.IsFaceSolid(FaceDirection.up)) {
            AddBlockFace(FaceDirection.down, ref chunkMesh);
        }

        // check the east and west block
        Block eastBlock = chunk.GetBlock(new Vector3(blockPos.x - 1, blockPos.y, blockPos.z));
        Block westBlock = chunk.GetBlock(new Vector3(blockPos.x + 1, blockPos.y, blockPos.z));
        if (!eastBlock.IsFaceSolid(FaceDirection.west)) {
            AddBlockFace(FaceDirection.west, ref chunkMesh);
        }
        if (!westBlock.IsFaceSolid(FaceDirection.east)) {
            AddBlockFace(FaceDirection.east, ref chunkMesh);
        }

        // check the north and south block
        Block northBlock = chunk.GetBlock(new Vector3(blockPos.x, blockPos.y, blockPos.z + 1));
        Block southBlock = chunk.GetBlock(new Vector3(blockPos.x, blockPos.y, blockPos.z - 1));
        if (!northBlock.IsFaceSolid(FaceDirection.south)) {
            AddBlockFace(FaceDirection.north, ref chunkMesh);
        }
        if (!southBlock.IsFaceSolid(FaceDirection.north)) {
            AddBlockFace(FaceDirection.south, ref chunkMesh);
        }
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

        // add the mesh data to the chunk mesh
        chunkMesh.AddFace(point1, point2, point3, point4);
        // add the uv coordinates of the face
        Vector2[] uvCoords = GetFaceUVCoordinates(direction);
        chunkMesh.AddUVCoordinates(uvCoords);
    }
}
