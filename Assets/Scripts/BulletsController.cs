using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹的控制脚本
/// 子弹诞生后，获取当前塔检测到的敌人目标后
/// 进行攻击逻辑划分
/// 跟随和抛物线效果
/// </summary>
public class BulletsController : MonoBehaviour {

	//公开子弹类型枚举 供编辑器选择子弹类型
	public BulletType BulletType;

	//由塔的控制脚本进行赋值 代表攻击对象的位置
	Transform target;

	//子弹的速度
	public float moveSpeed = 10;

	//[HideInInspector]  公开变量  但在编辑器中隐藏
	[HideInInspector]
	public AtkType atkType;

	//由塔的控制脚本进行访问 并将塔获取的攻击对象传递进来
	public void SetTarget(Transform enemyTF)
    {
		//赋值给子弹攻击目标
		target = enemyTF;
    }

	void Start()
    {
		//开启自动攻击线程
		StartCoroutine(AutoFire());
    }

	//设置协程，自动攻击
	IEnumerator AutoFire()
    {
		while(true)
        {
            switch (atkType)
            {
				//如果是跟随效果
				case AtkType.Follow:
                    if (target != null)
                    {
						//如果当前子弹的位置和敌人的位置距离大于3米
                        while (target!=null&& Vector3.Distance(transform.position, target.position) > 3)
                        {
							//慢慢移动至敌人位置
							transform.position= Vector3.Lerp(transform.position
								, target.position,moveSpeed*Time.deltaTime);

							//看向目标点
							transform.LookAt(target);
							//等待一个渲染帧
							yield return null;
                        }
						//如果目标不为空访问敌人身上的状态脚本  扣血逻辑
                    }
					Destroy(gameObject, 3);
					break;

				//如果是抛物线
				case AtkType.Lob:
					//if (target != null)
					//{
					//	//如果当前子弹的位置和敌人的位置距离大于3米
					//	while (target!=null&& Vector3.Distance(transform.position, target.position) > 3)
					//	{
					//		//慢慢移动至敌人位置
					//	}
					//}
					//Destroy(gameObject, 1);
					break;
            }
			yield return null;
        }
    }
}

//子弹的类型
public enum BulletType
{
	//大炮的子弹
	Fire,
	//弓弩的子弹
	Ice
}

//攻击的类型
public enum AtkType
{
	//跟随
	Follow,
	//抛物线
	Lob
}