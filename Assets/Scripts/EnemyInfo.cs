using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 敌人信息类
/// 
/// 设置当前血量最大血量并实现3D血条效果，
/// 对外提供受伤（Damage）方法，
/// 供子弹控制脚本进行传递伤害，
/// 当血量为0时销毁当前敌人。
/// </summary>

public class EnemyInfo : MonoBehaviour {


	// 最大血量的变量
	public float maxHP = 100;
	// 当前血量的变量
	private float currentHP;
	//定义Slider用于获取血条
	Slider slider;

	void Start()
	{
		//开始运行进行赋值
		currentHP = maxHP;
		//在当前挂在敌人身上寻找血条
		slider = GetComponentInChildren<Slider>();
	}

	public void Damage(int v)
	{
		//当前血量不为0，执行
        if (currentHP!=0)
        {
			//进行伤害
            currentHP -= v;
			//实时显示血量
            slider.value = currentHP / maxHP * slider.maxValue;
			//血量不足时，执行
            if (currentHP <= 0)
            {
				//销毁对象
				Destroy(gameObject); 
            }
        }
	}
}
