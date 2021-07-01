using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人运动脚本    当敌人被创建时，本脚本执行（挂载到小怪身上）
/// 判断当前小怪的位置和当前第一个路点的位置 距离是否大于1
/// 如果条件成立就一直运动
/// 不成立 就停止
/// </summary>

public class EnemyController : MonoBehaviour {

    //获取敌人运动索引
    public int index = 0;

    //定义敌人的速度
    float speed = 15f;

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
        //Instantiate(Images[0]);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //调用敌人移动方法
        Moving();
    }
    void Moving()
    {
        //执行移动方法
        //print("执行移动方法");
        //从PathManager脚本获取路径点数组PathManager.points
        //如果index小于 路径点数组点最大下标 就继续移动
        if (index <= PathManager.points.Length - 1)
        {
            //加入看向
            Vector3 p = PathManager.points[index];
            transform.LookAt(p);
            //获得 单位向量
            transform.position = Vector3.MoveTowards(transform.position, p, Time.deltaTime * speed);
            if (Vector3.Distance(PathManager.points[index], transform.position) <= 1f)
            {
                index++;
            }
        }
        else
        {
            //产生失败弹窗
            Instantiate(Canvas[0]);
            //销毁对象
            Destroy(gameObject);
        }
    }
}
