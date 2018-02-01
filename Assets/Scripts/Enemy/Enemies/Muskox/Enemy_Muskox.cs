using UnityEngine;

public class Enemy_Muskox : Enemy
{
    private float baseSpeed = 0.9f;
    private float speedRange = 0.5f;

    internal override float setSpeed()
    {
        float randomizer = Random.Range(baseSpeed - speedRange / 2, baseSpeed + speedRange / 2);
        float enemySpeed = randomizer;
        return enemySpeed;
    }
}
