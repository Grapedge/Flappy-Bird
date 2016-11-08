using UnityEngine;
using System.Collections;

public class ObjectMove : Grapedge {

	public Vector3 directionNormal;
	public float speed;
	public Transform[] m_Transform;

	protected void TGMove() {
		foreach (var tr in m_Transform) {
			tr.Translate(directionNormal * speed * Time.deltaTime);
		}
	}
}
