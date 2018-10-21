using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralModeling;

public class Quad : ProceduralModelingBase 
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float size = 0;

	protected override Mesh Build()
    {
        Mesh mesh = new Mesh();
        float halfSize = size * 0.5f;
        Vector3[] vertics = new Vector3[4]
        {
            new Vector3(-halfSize, -halfSize, 0),
            new Vector3(-halfSize, halfSize, 0),
            new Vector3(halfSize, halfSize, 0),
            new Vector3(halfSize, -halfSize, 0)
        };
        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f)
        };
        Vector3[] normal = new Vector3[4]
        {
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1)
        };
        int[] triangles = new int[]
        {
            0, 1, 3,
            1, 2, 3
        };
        mesh.vertices = vertics;
        mesh.uv = uv;
        mesh.normals = normal;
        mesh.triangles = triangles;
        return mesh;
    }
}
