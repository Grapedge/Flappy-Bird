using UnityEngine;
using System.Collections;

public class BackgroundSetAuto : MonoBehaviour {


	public Sprite[] backGround = new Sprite[2];
	/// <summary>
	/// 用于根据时间自动使用相应背景图片
	/// </summary>
	private void Start () {
		int hour = System.DateTime.Now.Hour;      // 获取时间
		if (hour >= 18 || hour <= 6) GetComponent<SpriteRenderer>().sprite = backGround [1];
		else GetComponent<SpriteRenderer>().sprite = backGround [0];
		Destroy(this);
	}

}
