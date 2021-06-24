using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 代表每座塔的信息
/// </summary>
public class TowerBaseInfo : MonoBehaviour {
	//设置默认状态
	public TowerBase_Type type = TowerBase_Type.None;

	
}
///<summary>
/// 代表塔类型的枚举    -1空塔   0弓弩   1代表大炮
/// </summary>
public enum TowerBase_Type
{
	//空塔
	None = -1,
	//弓弩
	TowerCrossBow,
	//大炮
	TowerMotar
}
