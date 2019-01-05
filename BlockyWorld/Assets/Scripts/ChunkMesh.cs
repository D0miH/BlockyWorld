using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMesh {

    // lists for the actual mesh of the chunk
    public List<Vector3> verticesList = new List<Vector3>();
    public List<int> trianglesList = new List<int>();

    // lists for the collider mesh of the chunk
    public List<Vector3> colliderVertices = new List<Vector3>();
    public List<int> colliderTriangles = new List<int>();

    // list of uv coordinates for texturing
    public List<Vector2> uvList = new List<Vector2>();

    /// <summary>
    /// Adds a face to the mesh of the chunk which is defined by the four given points. 
    /// First point should be upper left point of the quad. Then cycle through the points clockwise.
    /// </summary>
    /// <param name="upperLeft"></param>
    /// <param name="upperRight"></param>
    /// <param name="lowerRight"></param>
    /// <param name="point"></param>
    public void AddFace(Vector3 upperLeft, Vector3 upperRight, Vector3 lowerRight, Vector3 lowerLeft) {
        // add the points to the vertices lists
        AddVertex(upperLeft);
        AddVertex(upperRight);
        AddVertex(lowerRight);
        AddVertex(lowerLeft);

        // add the indices of the vertices to the triangles list
        int verticesListLength = verticesList.Count;
        // add the upper right triangle
        AddTriangle(verticesListLength - 4, verticesListLength - 3, verticesListLength - 2);
        // add the lower left triangle
        AddTriangle(verticesListLength - 2, verticesListLength - 1, verticesListLength - 4);
    }

    /// <summary>
    /// Adds a given vertex to the mesh and the collider mesh of the chunk.
    /// </summary>
    /// <param name="vector">The given vertex</param>
    void AddVertex(Vector3 vertex) {
        verticesList.Add(vertex);
        colliderVertices.Add(vertex);
    }

    /// <summary>
    /// Takes an array of uv coordinates and adds them to the uv list.
    /// </summary>
    /// <param name="uvCoords">The given array of uv coordinates</param>
    public void AddUVCoordinates(Vector2[] uvCoords) {
        uvList.AddRange(uvCoords);
    }

    /// <summary>
    /// Adds the given indices to the triangle list of the mesh and the triangle list of the collider mesh.
    /// </summary>
    /// <param name="index1">The given index</param>
    /// <param name="index2">The given index</param>
    /// <param name="index3">The given index</param>
    void AddTriangle(int index1, int index2, int index3) {
        // add them to the triangle list of the actual mesh
        trianglesList.Add(index1);
        trianglesList.Add(index2);
        trianglesList.Add(index3);
        // add them to the triangle list of the collider mesh
        colliderTriangles.Add(index1);
        colliderTriangles.Add(index2);
        colliderTriangles.Add(index3);
    }
}
