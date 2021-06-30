using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	//AboutImage组件
	public GameObject AboutImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//打开关于作者
	public void AboutBtnClick()
    {
		AboutImage.SetActive(true);
    }

	//退出游戏
	public void ExitGameBtnClick()
    {
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
    }
}
