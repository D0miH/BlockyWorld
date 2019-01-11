using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : Block {

    /// <summary>
    /// Constructor of the stone block class.
    /// </summary>
    /// <param name="pos"></param>
    public StoneBlock(Vector3 pos) : base(pos) {
    }

    public override TextureAtlasCoords GetTextureTileCoords(FaceDirection faceDir) {
        TextureAtlasCoords texCoords = new TextureAtlasCoords();

        // use the stone texture for all sides of the block
        texCoords.x = 0;
        texCoords.y = 1;

        return texCoords;
    }

    public override bool IsFaceSolid(FaceDirection faceDir) {
        // because all sides of the stone block are solid always return true
        return true;
    }
}
