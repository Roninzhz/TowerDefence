using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人的移动
/// </summary>

public class EnemyMoving : MonoBehaviour {

    private Vector3[] positions;

    public int index = 0;

    public float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        //从PathManager脚本获取路径点数组
        positions = PathManager.points;
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
        //如果index小于 路径点数组点最大下标 就继续移动
        if (index <= positions.Length - 1)
        {
            //获得 单位向量
            transform.Translate((positions[index] - transform.position).normalized * Time.deltaTime * speed);
            if (Vector3.Distance(positions[index], transform.position) <= 1f)
            {
                index++;
            }
        }
    }
}
