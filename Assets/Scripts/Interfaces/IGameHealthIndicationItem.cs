using UnityEngine;

public interface IGameHealthIndicationItem
{
	int GetHealthAmount();
	GameObject GetGameObject();
	void Kill();
}
