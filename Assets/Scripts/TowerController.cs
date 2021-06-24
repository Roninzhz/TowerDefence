using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔的控制脚本     每隔一个攻击间隔时间进行检测范围内是否有敌人
/// 如果有敌人，则根据当前塔攻击的类型来决定攻击方式
/// 攻击方式：弓弩为跟随效果  大炮为抛物线
/// 生成所需要的子弹类型
/// </summary>
public class TowerController : MonoBehaviour {

	/// <summary>
	/// 子弹预制体
	/// </summary>
	GameObject[] bullets;
	/// <summary>
	/// 子弹预制体的名字
	/// </summary>
	string[] bulletNames = { "Bullet_Fire", "Bullet_Ice" };
	// Use this for initialization
	void Start () {
		//开辟空间
		bullets = new GameObject[bulletNames.Length];
        //循环
        for (int i = 0; i < bullets.Length; i++)
        {
			//从内容加载，赋值给数组
			bullets[i] = Resources.Load<GameObject>("Bullets/" + bulletNames[i]);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
