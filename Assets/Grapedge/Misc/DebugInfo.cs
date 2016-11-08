using UnityEngine;
using System.Collections;

public class DebugInfo : Grapedge {

	public Grapedge.GameState state;
	public int scores;
	
	// Update is called once per frame
	void Update () {
		state = stateInfo;
		scores = score;
	}
}
