using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlock : Block {

    /// <summary>
    /// Constructor of the DirtBlock class.
    /// </summary>
    /// <param name="pos"></param>
    public DirtBlock(Vector3 pos) : base(pos) {
    }

    public override TextureAtlasCoords GetTextureTileCoords(FaceDirection faceDir) {
        TextureAtlasCoords texCoords = new TextureAtlasCoords();

        // use the dirt texture for all sides of the block
        texCoords.x = 1;
        texCoords.y = 0;

        return texCoords;
    }

    public override bool IsFaceSolid(FaceDirection faceDir) {
        // because all sides of the dirt block are solid always return true
        return true;
    }
}
