using UnityEngine;

public class Enemy_PolarBear : Enemy
{
    private float baseSpeed = 1.5f;
    private float speedRange = 1f;

    internal override float setSpeed()
    {
        float randomizer = Random.Range(baseSpeed - speedRange / 2, baseSpeed + speedRange / 2);
        float enemySpeed = randomizer;
        return enemySpeed;
    }
}
