using UnityEngine;
using System.Collections;

public class LandMove : ObjectMove {

	private int firstIndex = 0;

	/// <summary>
	/// 移动地面
	/// </summary>
	private void Update () {
		if (stateInfo == Grapedge.GameState.gameover) return;
		TGMove ();    // 移动transform组件
		if (m_Transform [firstIndex].position.x <= -10f) {
			int next = firstIndex == 0 ? 1 : 0;
			m_Transform[firstIndex].position = m_Transform[next].position + Vector3.right * 10.5f;
			firstIndex = next;
		}
	}
}
