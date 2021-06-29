using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人的诞生
/// </summary>

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
		//Instantiate(Enemys[0]);
		//print(Enemys[0].transform.position);
		//测试产生的敌人
		//Instantiate(Enemys[0]);
		//Instantiate(Enemys[1]);
	}
	// Use this for initialization
	void Start () {
		//开启协程
		StartCoroutine(AttackNum(10));
		//获取Path下的Point集合
		points = PathManager.points;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//AttackNum:敌人产生的波数
	IEnumerator AttackNum(int num)
	{
		yield return new WaitForSeconds(5);
		//当前波数
		int count = 0;
		//设置标志位：敌人类型
		int i;
		while (count < num)
		{
			count++;
			//控制敌人类型
            if (count % 2 == 0)
            {
				i = 1;
            }
			else
            {
				i = 0;
            }
			yield return StartCoroutine(EnemyNum(20,i));
			//每波怪产生的时间差
			yield return new WaitForSeconds(5);
		}
	}
	//EnemyNum：每波敌人的数量
	IEnumerator EnemyNum(int number,int i)
	{
		int count = 0;
		while (count < number)
		{
			count++;
			//每个怪产生的时间差
			yield return new WaitForSeconds(1f);
			//诞生敌人的 类型 位置 旋转
			Instantiate(Enemys[i], points[0], Quaternion.identity);
			//测试诞生体的位置
			//print(points[0]);
		}
	}
}
