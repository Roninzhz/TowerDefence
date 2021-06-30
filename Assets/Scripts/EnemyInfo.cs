using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour {

	/// <summary>
	/// 最大血量的变量
	/// </summary>
	public float maxHP = 100;
	/// <summary>
	/// 当前血量的变量
	/// </summary>
	private float currentHP;

	Slider slider;
	void Start()
	{
		//开始运行进行赋值
		currentHP = maxHP;
		//在当前挂在脚本的物体身上去找某一个组件
		slider = GetComponentInChildren<Slider>();
	}

	public void Damage(int v)
	{
        if (currentHP!=0)
        {
            currentHP -= v;
            slider.value = currentHP / maxHP * slider.maxValue;
			print(currentHP);
            if (currentHP <= 0)
            {
                Destroy(gameObject); //延迟一秒销毁对象
            }
        }
	}
}
