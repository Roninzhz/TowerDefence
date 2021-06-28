using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机控制脚本
/// 使用键盘水平和垂直轴值实现相机平移
/// 按住鼠标右键滑动鼠标实现相机左右旋转
/// 1.获取水平和垂直轴值做成Vector3
/// 2.根据v3进行移动
/// 3.当按住鼠标右键 获取鼠标水平轴值影响摄像机旋转
/// </summary>

public class CameraController : MonoBehaviour {

	//移动的速度
	[Tooltip("移动的速度")]
	public float moveSpeed = 10;
	//旋转速度
	[Tooltip("旋转的速度")]
	public float rotateSpeed = 20;

	// Update is called once per frame
	void Update () {
		//获取键盘水平垂直
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		//做成vector3
		Vector3 moveDir = new Vector3(h, 0, v);
		//四元素乘向量    基于摄像机的角度进行平移
		moveDir = transform.rotation * moveDir;
		//将y值实时赋值为0  防止向下
		moveDir.y = 0;
		//移动
		transform.Translate(moveDir * Time.deltaTime*moveSpeed,Space.World);
		//当摁住鼠标右键
		if(Input.GetMouseButton(1))
        {
			//获取鼠标的水平轴值
			float mX = Input.GetAxis("Mouse X");
			//根据鼠标左右移动  旋转摄像机
			transform.Rotate(new Vector3(0, mX, 0)*Time.deltaTime*rotateSpeed);
		}
		//开启
		StartCoroutine(Fun());
	}
	//协程  
	IEnumerator Fun()
    {
		yield return 0;
    }
}
