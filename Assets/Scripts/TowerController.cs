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
	/// <summary>
	/// 间隔时间
	/// </summary>
	public float Interval = 2;
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
		//开启 协程
		StartCoroutine(AutoCheckRangeEnemy());
	}
	//协程 ：协同程序  使用场景：1.分解操作  2.延时操作

	//自动检测范围内的敌人
	IEnumerator AutoCheckRangeEnemy()
    {
		while(true)
        {
			yield return new WaitForSeconds(Interval);

			//查找敌人
			Transform enemy = GetEnemy();

			if(enemy!=null)
            {
				//旋转看向敌人
				//获取塔看向敌人方向
				Vector3 targetDir = enemy.position - transform.position;

				//计算旋转的角度
				Quaternion q = Quaternion.LookRotation(targetDir);
				//如果敌人不为空 且 塔的旋转大于目标角度
                while (enemy != null && Quaternion.Angle(transform.rotation, q) > 1)
                {
					//旋转
					//需要实现计算看向的方向  因为敌人是活动状态
					targetDir = enemy.position - transform.position;
					//不需要上下旋转
					targetDir.y = 0;
					//实时计算
					q = Quaternion.LookRotation(targetDir);
					//慢慢转向
					transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.5f);
					//等待一个渲染帧在执行下次循环
					yield return null;
                }
            }
		}
    }

	//代表敌人的层级
	public LayerMask layer;
	//检测敌人碰撞器数组
	Collider[] targets;
	//检测攻击的半径
	public float atkRange = 80;
	//查找敌人方法  通过球形检测范围内的值
	public Transform GetEnemy()
    {
		//OverlapSphere  球形检测方法 1.以自身点进行检测  2.半径大小 3.需要被检测层级
		targets= Physics.OverlapSphere(transform.position,atkRange,layer);
		if (targets == null || targets.Length == 0)
			return null;

		//根据距离  选择返回数组中最小的敌人
		float minDis = float.MinValue;
		//最小的敌人对应数组的索引下标值
		int minIndex = 0;
        //遍历数组
        for (int i = 0; i < targets.Length; i++)
        {
			//当前敌人已经存在  则中断当前循环执行下次循环
			if (targets[i] == null)
				continue;
			//如果假设的最小距离 大于当前计算的距离
			float currentDis = Vector3.Distance(transform.position, targets[i].transform.position);

			if(minDis>currentDis)
            {
				//将当前距离赋值给最小的距离(保证minDis中存放时最小的)
				minDis = currentDis;
				//修改当前最小索引
				minIndex = i;
            }
        }
		//返回最小的敌人
		return targets[minIndex].transform;
    }
}
