using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBlock : Block {

    public AirBlock(Vector3 pos) : base(pos) {
    }

    public override TextureAtlasCoords GetTextureTileCoords(FaceDirection faceDir) {
        // just return un initialized texture coords. They won't be used.
        return new TextureAtlasCoords();
    }

    public override void AddBlockToChunk(Chunk chunk, ref ChunkMesh chunkMesh) {
        // override the standard method and add no faces to the chunkMesh
        return;
    }

    public override bool IsFaceSolid(FaceDirection faceDir) {
        return false;
    }
}
