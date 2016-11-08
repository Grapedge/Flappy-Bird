using UnityEngine;
using System.Collections;

public class Grapedge : MonoBehaviour {

	// 统一使用32像素为Unity中的1 unit, 1 像素 = 0.03125m

	public enum GameState { title, get_ready, playing, gameover }

	public static GameState stateInfo = GameState.title;

	public static int score = 123456789;

	public virtual void Initialize() {}
}
