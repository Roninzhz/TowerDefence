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

    public int index = 0;

    public float speed = 30f;

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
            //获得 单位向量
            transform.Translate((PathManager.points[index] - transform.position).normalized * Time.deltaTime * speed);
            if (Vector3.Distance(PathManager.points[index], transform.position) <= 1f)
            {
                index++;
            }
        }
    }
}
