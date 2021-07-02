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

    //用来记录销毁的对象数
	static int  num = 0;
	// 最大血量的变量
	public float maxHP = 100;
	// 当前血量的变量
	public float currentHP;
	//定义Slider用于获取血条
	Slider slider;
    // 用来存放内存中获取弹窗
    GameObject[] Canvas;

    //敌人的名字
    string[] CanvasName = { "Canvas", "Canvas1" };

    void Awake()
    {
        //加载内存中弹窗
        //开辟空间大小 取决于弹窗的长度
        Canvas = new GameObject[CanvasName.Length];
        for (int i = 0; i < Canvas.Length; i++)
        {
            //通过循环 将Res文件夹内对应的预制体进行获取并依次赋值实现
            //Resources.Load  方法可以通过在该文件夹中根据传入的路径进行加载游戏对象
            //                 参数  string类型  要的是该文件下具体到加载对象名字的路径
            Canvas[i] = Resources.Load<GameObject>("Enemys/" + CanvasName[i]);
        }
    }

    void Start()
	{
		//开始运行进行赋值
		currentHP = maxHP;
        //在当前挂在敌人身上寻找血条
        slider = GetComponentInChildren<Slider>();
	}

    void Update()
    {
        print(currentHP);
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
                //获取销毁数量
				++num;
				//销毁对象
				Destroy(gameObject); 
            }
        }
        if(num==10)
        {
            //获取成功弹窗
            Instantiate(Canvas[1]);
        }
	}
}
