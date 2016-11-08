using UnityEngine;
using System.Collections;

public class UIText : Grapedge {

	private string text;
	public Texture2D fonts;
	public GameObject prefabs;
	private Sprite[] numbers = new Sprite[10];

	// Use this for initialization
	private void Start () {
		for (int i = 0; i < 10; i++) {
			numbers[i] = Sprite.Create(fonts, new Rect(i * 24, 0, 24, 36), new Vector2(0.5f, 0.5f), 32);
		}
	}
	

	private ArrayList list = new ArrayList();
	public Vector3 lastPos = Vector3.zero;

	public override void Initialize() {
		foreach(var a in list) {
			Destroy((GameObject)a);
		}
		list.Clear();
		lastPos = new Vector3(0.375f, 4.88f, -3f);
		TextUpdate();
	}
	/// <summary>
	/// 更新分数 
	/// </summary>
	/// <param name="score">Score.</param>
	public void TextUpdate () {
		int index = 0;
		int tmp = score;
		do {
			MakeNewText(index);
			GameObject obj = (GameObject)list[index];
			obj.GetComponent<SpriteRenderer>().sprite = numbers[tmp % 10];
			tmp /= 10;
			index++;
		} while (tmp > 0);
	}

	private readonly float textOffset = 0.375f;
	/// <summary>
	/// 创建新的数字
	/// </summary>
	/// <param name="index">Index.</param>
	private void MakeNewText(int index) {
		if (index >= list.Count) {
			foreach (var a in list) {
				GameObject obj = (GameObject)a;
				lastPos = obj.transform.position;
				obj.transform.position += Vector3.right * textOffset;
			}
			GameObject scobj = Instantiate(prefabs, lastPos - Vector3.right * textOffset, Quaternion.identity) as GameObject;
			list.Add(scobj);
		}
	}
}
