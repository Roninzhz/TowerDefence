using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 协程测试脚本
/// </summary>
public class IEnumratorTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator Create()
    {
		//当协程执行如下语句是会延时3秒在继续执行下面的语句
		yield return new WaitForSeconds(3);
    }
}
