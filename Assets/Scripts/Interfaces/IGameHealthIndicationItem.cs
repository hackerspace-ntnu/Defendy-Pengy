using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameHealthIndicationItem{
	int GetHealthAmount();
	GameObject GetGameObject ();
}
