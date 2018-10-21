using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralModeling;

public class Plane : ProceduralModelingBase 
{
	[SerializeField]
	[Range(2, 30)]
	//每个Plane分格数
	private int heightSegment = 2, widthSegment = 2;
	[SerializeField]
	[Range(0.1f, 10.0f)]
	//Plane的长宽
	private float height = 1f, width = 1f;
	[SerializeField]
	//高度
	private float depth;
	[SerializeField]
	[Range(0.1f, 10f)]
	//偏移程度
	private float uScale = 0.1f, vScale = 0.1f;
	[SerializeField]
	private float uOffset = 0f, vOffset = 0f;


	protected override Mesh Build()
	{
		Mesh mesh = new Mesh();
		var vertices = new List<Vector3>();
		var uv = new List<Vector2>();
		var normals = new List<Vector3>();
		var triangles = new List<int>();
		var winv = 1.0f / (widthSegment - 1);
		var hinv = 1.0f / (heightSegment -1);
		for (int y = 0; y < heightSegment; y++)
		{
			//当前列的UV坐标
			var ry = y * hinv;
			for (int x = 0; x < widthSegment; x++)
			{
				//当前行的UV坐标
				var rx = x * winv;
				var rz = Mathf.PerlinNoise(rx * uScale + uOffset, ry * vScale + vOffset) * depth;  
				vertices.Add(new Vector3(rx * width, rz, ry * height));
				uv.Add(new Vector2(rx, ry));
				normals.Add(new Vector3(0, 1f, 0));
			}
		}
		int index = 0;
		int a, b, c, d;
		for (int y = 0; y < heightSegment - 1; y++)
		{
			for (int x = 0; x < widthSegment - 1; x++)
			{
				index = y * widthSegment + x;
				a = index;
				b = index + widthSegment;
				c = index + widthSegment + 1;
				d = index + 1;
				//Quad第一个三角形
				triangles.Add(a);
				triangles.Add(b);
				triangles.Add(d);
				//Quad第二个三角形
				triangles.Add(b);
				triangles.Add(c);
				triangles.Add(d);
			}
		}
		mesh.vertices = vertices.ToArray();
		mesh.uv = uv.ToArray();
		mesh.normals = normals.ToArray();
		mesh.triangles = triangles.ToArray();
		return mesh;
	}
}
