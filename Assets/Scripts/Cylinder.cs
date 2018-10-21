using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralModeling;

public class Cylinder : ProceduralModelingBase 
{
	[SerializeField]
	//圆柱的高
	private	float height;
	[SerializeField]
	[Range(6, 360)]
	//圆周上点的个数
	private int segment = 6;
	[SerializeField]
	//圆周半径
	private float radius;

	protected override Mesh Build()
	{
		Mesh mesh = new Mesh();
		var vertices = new List<Vector3>();
		var normals = new List<Vector3>();
		var uvs = new List<Vector2>();
		var triangles = new List<int>();
		//绘制圆柱的侧面
		GenCircle(segment, vertices, uvs, normals, true);
		int index = 0;
		var len = segment * 2;
		for (int i = 0; i < segment; i++)
		{
			index = i * 2;
			int a = index, b = (index + 1) % len, c = (index + 2) % len, d = (index + 3) % len;
			triangles.Add(a);
			triangles.Add(d);
			triangles.Add(b);
			triangles.Add(a);
			triangles.Add(c);
			triangles.Add(d);
		}
		//绘制圆柱的上底和下底
		GenCircle(segment, vertices, uvs, normals, false);
		vertices.Add(new Vector3(0, height * 0.5f, 0));
		vertices.Add(new Vector3(0, -height * 0.5f, 0));
		uvs.Add(new Vector2(0.5f, 1f));
		uvs.Add(new Vector2(0.5f, 0f));
		normals.Add(new Vector3(0, 1, 0));
		normals.Add(new Vector3(0, -1, 0));
		//上底和下底的圆心
		int upC = vertices.Count - 2, downC = vertices.Count - 1;
		for (int i = 0; i < segment; i++)
		{
			index = i * 2;
			//上底
			int a = index + len, b = (index + 2) % len + len;
			//下底
			int c = (index + 1) % len + len, d = (index + 3) % len + len;
			triangles.Add(upC);
			triangles.Add(b);
			triangles.Add(a);
			triangles.Add(downC);
			triangles.Add(c);
			triangles.Add(d);
		}
		mesh.vertices = vertices.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.normals = normals.ToArray();
		mesh.triangles = triangles.ToArray();
		return mesh;
	}

	//绘制上底和下底的顶点
	private void GenCircle(int segment, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, bool isSide)
	{
		var rad = (float)(Mathf.PI * 2 / segment);
		float rx = 0f, ry = 0f;
		for (int i = 0; i < segment; i++)
		{
			var cos = Mathf.Cos(rad * i);
			var sin = Mathf.Sin(rad * i);
			rx = radius * cos;
			ry = radius * sin;
			vertices.Add(new Vector3(rx, height * 0.5f, ry));
			vertices.Add(new Vector3(rx, -height * 0.5f, ry));
			uvs.Add(new Vector2((float)(i / segment), 1f));
			uvs.Add(new Vector2((float)(i / segment), 0f));
			if (isSide)
			{
				var normal = new Vector3(cos, 0, sin);
				normals.Add(normal);
				normals.Add(normal);
			}
			else
			{
				normals.Add(new Vector3(0f, 1f, 0f));
				normals.Add(new Vector3(0f, -1f, 0f));
			}
		}
	}
}
