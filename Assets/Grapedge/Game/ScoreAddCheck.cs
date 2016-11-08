using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ScoreAddCheck : Grapedge {

	private RaycastHit2D hitInfo;
	private UIText scoreManger;
	private bool enter = false;
	private AudioSource m_Audio;

	private void Start() {
		scoreManger = GameObject.Find("ScoreManger").GetComponent<UIText>();
		m_Audio = GetComponent<AudioSource>();
	}

	private void Update () {
		hitInfo = Physics2D.Linecast(transform.position + Vector3.up * 5, transform.position + Vector3.up * 11, 1 << LayerMask.NameToLayer("Player"));
		if (hitInfo.collider == null) {
			enter = false;
			return;
		}
		if (!enter && hitInfo.collider.tag == "Player") {
			m_Audio.Play();
			score++;
			scoreManger.TextUpdate();
			enter = true;
		}
	}
}
