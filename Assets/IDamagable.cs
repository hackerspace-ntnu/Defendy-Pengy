using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {
	float GetHealth ();
	float DecreaseHealth ();
}