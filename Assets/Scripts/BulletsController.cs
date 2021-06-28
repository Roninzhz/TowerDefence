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

	//可以被攻击的层级
	public LayerMask layer;
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
					//很有可能target被其他塔打死  子弹已经发射出来了，需要顺着当前的方位移动
                    else
                    {
						//如果高度大于200
						if(transform.position.y>200)
                        {
							//在当前的高度每帧下降两米
							transform.Translate(new Vector3(0, 2, 0), Space.World);
                        }
                    }
					Destroy(gameObject);
					break;

				//如果是抛物线
				case AtkType.Lob:

					if(target!=null)
                    {
						//用于计算比例的差值
						float t=0;
					
						//声明开始的位置
						Vector3 startPos = transform.position;
						//循环条件 如果当前子弹和敌人的位置大于5
						while(Vector3.Distance(transform.position,target.position)>5)
                        {
							//抛物线的逻辑
							t += Time.deltaTime;
							//计算从开始到结束线段上的比例
							Vector3 currentPos= Vector3.Lerp(startPos, target.position, t);
							//通过Sin 计算抛物线
							float h = Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI);
							//每个比例点增加高度
							currentPos.y += h * 20;
							//将设定好的位置赋值给当前子弹
							transform.position = currentPos;
							//等待一个渲染帧
							yield return null;
						}
						//获取当前范围内所有敌人，进行伤害
						Collider[] allEnemy = Physics.OverlapSphere(transform.position, 20, layer);
						if(allEnemy!=null&&allEnemy.Length!=0)
                        {
							for(int i=0;i<allEnemy.Length;i++)
                            {
								//依次进行扣血  范围伤害
								//allEnemy[i].GetComponent<EnemyInfo>().Damage(30);
                            }
                        }
					}
					Destroy(gameObject);
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