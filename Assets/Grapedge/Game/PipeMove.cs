using UnityEngine;
using System.Collections;

public class PipeMove : ObjectMove {
	private void Update() { 
		if (stateInfo == Grapedge.GameState.gameover) {
			BoxCollider2D[] bc = GetComponentsInChildren<BoxCollider2D>();
			foreach (var a in bc) a.enabled = false;
			return;
		}
		TGMove();
	}
}
