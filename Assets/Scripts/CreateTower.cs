using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 创建塔的脚本
/// 当鼠标放在塔基的位置  应当创建阴影
/// 当点击左键时创建塔阴影对应的模型（弓弩，大炮）
/// 当按下键盘的1 2键切换创建的模型
/// </summary>
public class CreateTower : MonoBehaviour {

	/// <summary>
	/// 用来存放内存中获取的塔
	/// </summary>
	GameObject[] towerPrefabs;
	/// <summary>
	/// 用来存放内存中获取的塔阴影
	/// </summary>
	GameObject[] towerShadowPrefabs;
	/// <summary>
	/// 用来存放实例化到场景中的塔阴影
	/// </summary>
	GameObject[] towerInstanceShadowPrefabs;
	/// <summary>
	/// 用来存放场景中已经存在的模型(就是屏幕左上角的那两个)
	/// </summary>
	GameObject[] towerShows;
	//塔的名字
	string[] towerPrefabName = { "Tower_Crossbow", "Tower_Mortar" };
	//塔阴影的名字
	string[] towerShadowName = { "Tower_Crossbow_shadow", "Tower_Mortar_shadow" };
	//场景中显示的模型名字
	string[] towerShowName = { "Tower_Crossbow_show", "Tower_Mortar_show" };
	//当前显示塔的索引 当前索引为0时表示弓弩 索引为1是表示大炮
	int currentShowIndex = 0;
	void Awake()
    {
		//加载内存中获取的塔
		//开辟空间大小 取决于塔名字数组的长度
		towerPrefabs = new GameObject[towerPrefabName.Length];
        for (int i = 0; i < towerPrefabs.Length; i++)
        {
			//通过循环 将Res文件夹内对应的预制体进行获取并依次赋值实现
			//Resources.Load  方法可以通过在该文件夹中根据传入的路径进行加载游戏对象
		   //                 参数  string类型  要的是该文件下具体到加载对象名字的路径
			towerPrefabs[i]= Resources.Load<GameObject>("Towers/"+towerPrefabName[i]);
		}
		//加载内存中获取的塔阴影
		towerShadowPrefabs = new GameObject[towerShadowName.Length];
		for (int i = 0; i < towerShadowPrefabs.Length; i++)
		{
			towerShadowPrefabs[i] = Resources.Load<GameObject>("Towers/"+ towerShadowName[i]);
		}

		//加载实例化到场景中的塔阴影
		towerInstanceShadowPrefabs = new GameObject[towerShadowPrefabs.Length];
		for (int i = 0; i < towerInstanceShadowPrefabs.Length; i++)
		{
			//依次实例化并存储
			towerInstanceShadowPrefabs[i] = Instantiate(towerShadowPrefabs[i]);
			//禁用所有阴影
			towerInstanceShadowPrefabs[i].SetActive(false);
		}

		towerShows = new GameObject[towerShowName.Length];
        for (int i = 0; i < towerShows.Length; i++)
        {
			//GameObject.Find 根据传入的名字找到场景
			towerShows[i] = GameObject.Find(towerShowName[i]);
			//测试
			towerShows[i].SetActive(false);
			//如果i不等于当前索引
			if(i!=currentShowIndex)
            {
				//需要将当前不需要显示的索引禁用
				towerShows[i].SetActive(false);
            }
        }
	}

	//声明射线类型
	Ray ray;
	//射线返回的信息
	RaycastHit hit;
	//可以被检测层级对象
	public LayerMask layer;
	//塔的高度
	float high = 13.5f;
	// Update is called once per frame
	void Update () {
		//通过射线实现以下逻辑
		//接收射线
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//发射射线进行检测  根据层级来决定是否检测成功
		if(Physics.Raycast(ray,out hit,float.MaxValue,layer))
        {
			//启用阴影的先决条件：是空塔
			//查找射线返回的对象身上的脚本TowerBaseInfo
			TowerBaseInfo info = hit.transform.GetComponent<TowerBaseInfo>();
			if (info != null && info.type == TowerBase_Type.None)
			{
				//启用阴影
				towerInstanceShadowPrefabs[currentShowIndex].SetActive(true);
				//设置阴影的位置
				towerInstanceShadowPrefabs[currentShowIndex].transform.position = hit.transform.position + new Vector3(0, high, 0);
				//设置阴影的旋转
				towerInstanceShadowPrefabs[currentShowIndex].transform.rotation = hit.transform.rotation;
				//当按下鼠标左键时
				if(Input.GetMouseButtonDown(0))
                {
					//创建塔的逻辑
					//修改当前塔座的信息
					//直接当当前显示的索引转换为枚举值  赋值给塔的类型
					info.type = (TowerBase_Type)currentShowIndex;
					//隐藏阴影
					towerInstanceShadowPrefabs[currentShowIndex].SetActive(false);
					//实例化塔  根据索引创建预制体 创建在当前射线检测的位置+底座高度旋转使用射线旋转的角度
					Instantiate(towerPrefabs[currentShowIndex],hit.transform.position+new Vector3(0,high,0),hit.transform
						.rotation);
				}

			}
		}
		//禁用阴影
		else
			towerInstanceShadowPrefabs[currentShowIndex].SetActive(false);
		//尝试按下键盘的1键将当前显示的索引设置为0 按下2键当前显示的索引值为1
		//按下1键
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetTowerCurrentIndext(currentShowIndex = 0);
		}

		//按下2键
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SetTowerCurrentIndext(currentShowIndex = 1);
		}
	}
	//设置当前显示索引
	void SetTowerCurrentIndext(int index)
	{
        for (int i = 0; i < towerShows.Length; i++)
        {
			//如果传入索引=i
			if(i==index)
            {
				//显示当前i对应的模型
				towerShows[i].SetActive(true);
            }
			else
				//否则 则禁用
				towerShows[i].SetActive(false);
		}

		//每当切换左上角模型时
		for (int i = 0; i < towerInstanceShadowPrefabs.Length; i++)
		{
			//把场景中的阴影都关掉主要是为了更新阴影
			towerInstanceShadowPrefabs[i].SetActive(false);
		}
	}
}
