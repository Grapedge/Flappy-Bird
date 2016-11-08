using UnityEngine;
using System.Collections;

public class ReloadGame : Grapedge {
	
	private bool has_DeleteTitle = false;

	private AudioSource m_Audio;

	private void Start() {
		m_Audio = GetComponent<AudioSource>();
	}
	/// <summary>
	/// 重新初始化游戏
	/// </summary>
	/// <returns>The game.</returns>
	public override void Initialize() {
		if (!has_DeleteTitle) {
			Destroy(GameObject.Find("Title"));
			has_DeleteTitle = true;
		}
		GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
		foreach (var a in pipes) Destroy(a);
		GameObject.FindWithTag("Player").GetComponent<PlayerController>().Initialize();
		GameObject.Find("ButtonManger").GetComponent<UIButton>().buttonNormal.SetActive(false);
		GameObject.Find("text_ready").GetComponent<Animator>().Play("ready_in");
		score = 0;
		GameObject.Find("ScoreManger").GetComponent<UIText>().Initialize();
		stateInfo = GameState.get_ready;
	}

	public void GameOver() {
		m_Audio.Play();
		GameObject.Find("ButtonManger").GetComponent<UIButton>().buttonNormal.SetActive(true);
	}
}
