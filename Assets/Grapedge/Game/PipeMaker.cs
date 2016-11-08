using UnityEngine;
using System.Collections;

public class PipeMaker : Grapedge {

	private GameObject[] pipes = new GameObject[3];
	public GameObject prefabs;
	public float waitTime = 3f;
	private float m_Timer = 0f;
	private bool inited = false;
	private int m_LastGameObjectIndex = 0;
	private int index = 0;
	
	void Update () {
		if (stateInfo == GameState.gameover)
			Destroy(gameObject);
		if (!inited) {
			m_Timer += Time.deltaTime;
			if (waitTime < m_Timer) {
				for (int i = 0; i < pipes.Length; i++) {
					float x = i == 0 ? 6.0f : pipes[i - 1].transform.position.x + 5.0f;
					pipes[i] = Instantiate(prefabs, new Vector3(x, Random.Range (-2, 5) + 0.5f, 0), Quaternion.identity) as GameObject;
				}
				m_LastGameObjectIndex = pipes.Length - 1;
				inited = true;
			}
			return ;
		}

		// 生成柱子
		if (pipes [index].transform.position.x <= -5.5f) {
			pipes [index].transform.position = new Vector3(pipes[m_LastGameObjectIndex].transform.position.x + 5.0f, Random.Range (-2, 5) + 0.5f, -1);
			m_LastGameObjectIndex = index;
			index = index + 1 < pipes.Length ? index + 1 : 0;
		}

	}
}
