using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 路线管理器：1.在开始时找到所有的路点  依次存储
///             2.绘制路线  需要LineRender组件才能绘制在Game视窗
/// </summary>
public class PathManager : MonoBehaviour {

	/// <summary>
	/// 路线数组
	/// </summary>
	public static Vector3[] points;
	/// <summary>
	/// 线性渲染组件
	/// </summary>
	LineRenderer line;
	public static Transform[] wayPoint;
	// Use this for initialization
	void Start () {


		line = GetComponent<LineRenderer>();
		//分配路点数组的空间
		points = new Vector3[transform.childCount];
		wayPoint = new Transform[transform.childCount];
		for(int  i=0;i<points.Length;i++)
        {
			//获取父物体下的每一个路点 存储到路点数组中
			points[i] = transform.GetChild(i).position;

			wayPoint[i] = transform.GetChild(i);
			//测试
			//print(wayPoint[i].position);
		}

		//将绘制点数量赋值给线性组件
		line.positionCount = points.Length;
		line.startColor = Color.red;
		line.endColor = Color.black;
		//根据路点数组  绘制一根线
		line.SetPositions(points);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
