using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralModeling
{
	public enum ProceduralModelingMaterial 
	{
		Standard,
		UV,
		Normal
	};

	[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
	[ExecuteInEditMode]
	public abstract class ProceduralModelingBase : MonoBehaviour 
	{
		private MeshFilter filter;
		private MeshRenderer meshRenderer;
		protected ProceduralModelingMaterial materialType = ProceduralModelingMaterial.UV;

		public MeshFilter Filter
		{
			get
			{
				if(filter == null)
				{
					filter = GetComponent<MeshFilter>();
				}
				return filter;
			}
		}

		public MeshRenderer Renderer
		{
			get
			{
				if (meshRenderer == null)
				{
					meshRenderer = GetComponent<MeshRenderer>();
				}
				return meshRenderer;
			}
		}

		protected virtual void Start ()
		{
			Rebuild();
		}

		//重构网格
		public void Rebuild() 
		{
			if(Filter.sharedMesh != null) 
			{
				if(Application.isPlaying) 
				{
					Destroy(Filter.sharedMesh);
				} 
				else 
				{
					DestroyImmediate(Filter.sharedMesh);
				}
			} 
			Filter.sharedMesh = Build();
			Renderer.sharedMaterial = LoadMaterial(materialType);
		}

		//材质载入
		private Material LoadMaterial(ProceduralModelingMaterial type)
		{
			switch (type)
			{
				case ProceduralModelingMaterial.UV:
				{
					return Resources.Load<Material>("Materials/UV");
				}
				case ProceduralModelingMaterial.Normal:
				{
					return Resources.Load<Material>("Materials/Normal");
				}
			}
			return Resources.Load<Material>("Standard");
		}

		//网格构建
		protected abstract Mesh Build();
		
	}
}

