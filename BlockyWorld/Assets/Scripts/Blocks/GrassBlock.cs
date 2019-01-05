using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBlock : Block {

    /// <summary>
    /// Constructor of the GrassBlock class.
    /// </summary>
    /// <param name="pos"></param>
    public GrassBlock(Vector3 pos) : base(pos) {
    }

    public override TextureAtlasCoords GetTextureTileCoords(FaceDirection faceDir) {
        TextureAtlasCoords texCoords = new TextureAtlasCoords();

        // depending on the direction return the coordinates for the texture
        switch (faceDir) {
            case FaceDirection.up:
                texCoords.x = 2;
                texCoords.y = 0;
                return texCoords;
            case FaceDirection.down:
                texCoords.x = 1;
                texCoords.y = 0;
                return texCoords;
            default: // return the coordinates for the side face textures
                texCoords.x = 0;
                texCoords.y = 0;
                return texCoords;
        }
    }
}
