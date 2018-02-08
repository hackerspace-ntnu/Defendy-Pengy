using UnityEngine;

public class GameOverText : MonoBehaviour {
	private Transform parent;
	private int index;

	void Start () {
		parent = transform.parent;
		index = gameObject.transform.GetSiblingIndex();

		var vector = new Vector3(3f,0f,0f);
		vector = Quaternion.Euler(0f, 90f*((float)index), 0f) * vector;
		transform.position = parent.transform.position + vector;
		transform.LookAt(parent.transform);
	}
}
