using UnityEngine;

public class CrowSpawner : MonoBehaviour
{
	public Crow crowPrefab;
	public Transform[] goals;

	public void SpawnCrows()
	{
		for (int i = 0; i < goals.Length; i++)
		{
			Crow crow = Instantiate(crowPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<Crow>();
			crow.transform.eulerAngles = new Vector3(0f, -45f, 0f);
			crow.goal = goals[i].transform;
			crow.transform.parent = gameObject.transform;
		}
	}
}
