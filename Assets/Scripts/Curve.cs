using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralModeling
{
	[System.Serializable]
	//生成N阶贝塞尔曲线
	public class Curve
	{
		[SerializeField]
		//曲线的控制点
		private	Vector3[] points;

		//获取贝塞尔曲线上t值所对应的点[0, 1]
		public Vector3 GetPoint(float t) 
		{
			//贝塞尔曲线阶数
			int n = points.Length - 1;
			//将t值控制在[0,1]
			if (t > 1f)
				t = 1f;
			if (t < 0f)
				t = 0f;
			int count = points.Length;
			Vector3[] tmp, lastPoints;
			lastPoints = points;
			for (int i = 0; i < n; i++)
			{
				tmp = new Vector3[count - 1];
				for (int j = 0; j < lastPoints.Length - 1; j++)
				{
					tmp[j] = (lastPoints[j + 1] - lastPoints[j]) * t + lastPoints[j];
				}
				lastPoints = tmp;
				count--;
			}
			return lastPoints[0];
		}
	}
}