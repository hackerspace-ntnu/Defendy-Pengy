using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveParser
{

	public static List<EnemyWave> ParseWaveFile(TextAsset wavefile)
	{
		List<EnemyWave> waves = new List<EnemyWave>();
        List<int> spawnerID_currentWave = new List<int>();
        List<EnemySpawner.EnemyType> enemies_currentWave = new List<EnemySpawner.EnemyType>();
		List<float> delays_currentWave = new List<float>();

        foreach (string l in wavefile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
        {
            string line = l.Trim();
            if (line.Length == 0 || line[0] == '/' || line[0] == '#')
            {
                AddWave(waves, spawnerID_currentWave, enemies_currentWave, delays_currentWave);
                continue;
            }

            string[] tokens = line.Split(' ');

            if (tokens.Length != 4)
                throw new System.FormatException("Invalid enemy type declaration: " + line);

            int spawnID = int.Parse(tokens[0]);
            int enemyCount = int.Parse(tokens[1]);
            EnemySpawner.EnemyType enemy = GetEnemyType(tokens[2]);
            float delay = float.Parse(tokens[3]);
            for (int i = 0; i < enemyCount; i++)
            {
                spawnerID_currentWave.Add(spawnID);
                enemies_currentWave.Add(enemy);
                delays_currentWave.Add(delay);
            }
        }

		if (enemies_currentWave.Count > 0)
			AddWave(waves, spawnerID_currentWave, enemies_currentWave, delays_currentWave);

		return waves;
	}

	private static void AddWave(List<EnemyWave> waves, List<int> spawnerID_currentWave, List<EnemySpawner.EnemyType> enemies_currentWave, List<float> delays_currentWave)
	{
		if (enemies_currentWave.Count == 0)
			return;

		waves.Add(new EnemyWave(spawnerID_currentWave.ToArray(), enemies_currentWave.ToArray(), delays_currentWave.ToArray()));
        spawnerID_currentWave.Clear();
		enemies_currentWave.Clear();
		delays_currentWave.Clear();
	}

	private static EnemySpawner.EnemyType GetEnemyType(string token)
	{
		switch (token)
		{
			case "bear":
				return EnemySpawner.EnemyType.Bear;

			case "fish":
				return EnemySpawner.EnemyType.Fish;

			case "pig":
				return EnemySpawner.EnemyType.Pig;

			case "wolf":
				return EnemySpawner.EnemyType.Wolf;

			case "fox":
				return EnemySpawner.EnemyType.Fox;

            case "polarbear":
                return EnemySpawner.EnemyType.PolarBear;

            case "seal":
                return EnemySpawner.EnemyType.Seal;

			default:
				throw new System.FormatException("Invalid enemy type: " + token);
		}
	}
}
