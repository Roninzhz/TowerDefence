using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanEnemy : MonoBehaviour {

	// 用来存放内存中获取的敌人
	GameObject[] Enemys;

	//敌人的名字
	string[] EnemyName = { "Enemy01", "Enemy02" };

	//路线数组
	Vector3[] points;

	void Awake()
    {
		//加载内存中获取的敌人
		//开辟空间大小 取决于敌人名字数组的长度
		Enemys = new GameObject[EnemyName.Length];
		for (int i = 0; i < Enemys.Length; i++)
		{
			//通过循环 将Res文件夹内对应的预制体进行获取并依次赋值实现
			//Resources.Load  方法可以通过在该文件夹中根据传入的路径进行加载游戏对象
			//                 参数  string类型  要的是该文件下具体到加载对象名字的路径
			Enemys[i] = Resources.Load<GameObject>("Enemys/" + EnemyName[i]);			
		}
		Instantiate(Enemys[0]);
		//print(Enemys[0].transform.position);
		//测试产生的敌人
		//Instantiate(Enemys[0]);
		//Instantiate(Enemys[1]);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
