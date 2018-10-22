using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralModeling;

public class Pipe : ProceduralModelingBase 
{
	[SerializeField]
	//曲线类
	private Curve curve;
	[SerializeField]
	//曲线上点的个数
	private int curveSegment;
	
	protected override void Start()
	{
		base.Start();
		curve = new Curve();
	}

	protected override Mesh Build()
	{
		Mesh mesh = new Mesh();
		List<Vector3> vertices = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<Vector3> normals = new List<Vector3>();
		List<int> triangles = new List<int>();
		
		return mesh;
	}
}
