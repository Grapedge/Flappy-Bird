using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {

	private AudioSource m_Audio;

	private void OnButtonPlayClick() {
		GameObject.Find("Fade").GetComponent<Animator>().Play("Fade");
	}

	private void OnButtonExitClick() {
		Application.Quit();
	}

	private void OnButtonGrapeClick() {
		Application.OpenURL("http://bbs.u-pt.pw/forum.php");
	}

	private GameObject m_LastButton;
	public GameObject buttonNormal;

	private void Start() {
		buttonNormal = GameObject.Find ("ButtonSprite");
		m_Audio = GetComponent<AudioSource>();
	}
	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hitInfo.collider != null) {
				if (hitInfo.collider.tag != "Button") return;
				hitInfo.collider.transform.position += new Vector3(0.03125f, -0.03125f, 0f); 
				m_LastButton = hitInfo.collider.gameObject; 
				if (hitInfo.collider.name == "button_play") {
					m_Audio.Play();
					OnButtonPlayClick();
				} else if (hitInfo.collider.name == "button_grapedge") {
					OnButtonGrapeClick();
				} else if (hitInfo.collider.name == "button_exit") {
					OnButtonExitClick();
				}
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			if (m_LastButton != null) {
				m_LastButton.transform.position -= new Vector3(0.03125f, -0.03125f, 0f);
				m_LastButton = null;
			}
		}

	}
	

}
