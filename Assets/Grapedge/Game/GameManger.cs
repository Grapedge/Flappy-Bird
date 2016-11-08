using UnityEngine;
using System.Collections;

public class GameManger : Grapedge {

	public GameObject pipeMakerPrefabs;

	private void Update () {
		if (stateInfo == GameState.get_ready) {
			// 在此状态下点击鼠标开始游戏
			GetReady();
		}
	}

	private void GetReady() {
		// 当处于准备开始状态时，按下鼠标开始游戏
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			GameObject.Find("text_ready").GetComponent<Animator>().Play("play");
			GameObject.FindWithTag("Player").GetComponent<PlayerController>().StartPlayer();
			Instantiate(pipeMakerPrefabs);
			/*pipeMaker = (GameObject)Instantiate(pipeMakerPrefabs);
			getReady.GetComponent<Animator>().Play("forward");
			score = 0; // for change*/
		}
	}
}
