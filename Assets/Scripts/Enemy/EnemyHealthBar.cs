using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
	public void Display(float percentage)
	{ // percentage = [0,1]
		var globalScale = new Vector3(percentage, 0.1f, 0.1f);

		transform.localScale = Vector3.one;
		transform.localScale = new Vector3(
			globalScale.x / transform.lossyScale.x,
			globalScale.y / transform.lossyScale.y,
			globalScale.z / transform.lossyScale.z);
	}
}
