using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveParser
{
	public static List<EnemyWave> ParseWaveFile(TextAsset wavefile)
	{
		List<EnemyWave> waves = new List<EnemyWave>();

		List<EnemySpawner.EnemyType> enemies_currentWave = new List<EnemySpawner.EnemyType>();
		List<float> delays_currentWave = new List<float>();

		foreach (string l in wavefile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
		{
			string line = l.Trim();
			if (line.Length == 0 || line[0] == '/' || line[0] == '#')
			{
				AddWave(waves, enemies_currentWave, delays_currentWave);
				continue;
			}

			string[] tokens = line.Split(' ');
			switch (tokens.Length)
			{
				case 0:
					break;

				case 2:
					enemies_currentWave.Add(GetEnemyType(tokens[0]));
					delays_currentWave.Add(float.Parse(tokens[1]));
					break;

				case 3:
					int enemyCount = int.Parse(tokens[0]);
					EnemySpawner.EnemyType enemy = GetEnemyType(tokens[1]);
					float delay = float.Parse(tokens[2]);
					for (int i = 0; i < enemyCount; i++)
					{
						enemies_currentWave.Add(enemy);
						delays_currentWave.Add(delay);
					}
					break;

				default:
					throw new System.FormatException("Invalid enemy type declaration: " + line);
			}
		}

		if (enemies_currentWave.Count > 0)
			AddWave(waves, enemies_currentWave, delays_currentWave);

		return waves;
	}

	private static void AddWave(List<EnemyWave> waves, List<EnemySpawner.EnemyType> enemies_currentWave, List<float> delays_currentWave)
	{
		if (enemies_currentWave.Count == 0)
			return;

		waves.Add(new EnemyWave(enemies_currentWave.ToArray(), delays_currentWave.ToArray()));
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

			default:
				throw new System.FormatException("Invalid enemy type: " + token);
		}
	}
}
