using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTerrain {

    /// <summary>
    /// Swaps the block at the position which got hit by the raycast with the given block.
    /// </summary>
    /// <param name="raycastHit">The given raycast hit</param>
    /// <param name="block">The given block which will be placed</param>
    public static void SetBlock(RaycastHit raycastHit, Block block) {
        // get the chunk which got hit by the raycast
        Chunk chunk = raycastHit.collider.GetComponent<Chunk>();

        if (chunk == null) {
            // if the chunk is null, just return and do nothing
            return;
        }

        chunk.blockTerrain.SetBlock(GetBlockPos(raycastHit), block);
    }

    /// <summary>
    /// Returns the position of the block at the raycast hit in world coordinates.
    /// </summary>
    /// <param name="raycastHit">The given raycast hit</param>
    /// <returns></returns>
    public static Vector3 GetBlockPos(RaycastHit raycastHit) {
        Vector3 pos = raycastHit.point;
        Vector3 normal = raycastHit.normal;

        // get a position inside the block
        pos = pos - normal / 2;
        // and round to the next int which is the position of the block
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        pos.z = Mathf.RoundToInt(pos.z);

        return pos;
    }
}
