using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveParser
{
	public static List<EnemyWave> ParseWaveFile(TextAsset wavefile)
	{
		List<EnemyWave.Spawn> spawns = new List<EnemyWave.Spawn>();
		List<EnemyWave> waves = new List<EnemyWave>();

		foreach (string l in wavefile.text.Split(new[] { "\n" }, StringSplitOptions.None))
		{
			string line = l.Trim();
			if (line.Length == 0 || line[0] == '/' || line[0] == '#')
			{
				AddWave(waves, spawns);
				continue;
			}

			string[] tokens = line.Split(' ');

			if (tokens.Length != 4)
				throw new System.FormatException("Invalid enemy type declaration: " + line);

			int spawnID = int.Parse(tokens[0]);
			int enemyCount = int.Parse(tokens[1]);
			Enemy.Type enemy = GetEnemyType(tokens[2]);
			float delay = float.Parse(tokens[3]);
			for (int i = 0; i < enemyCount; i++)
				spawns.Add(new EnemyWave.Spawn(spawnID, enemy, delay));
		}

		if (spawns.Count > 0)
			AddWave(waves, spawns);

		return waves;
	}

	private static void AddWave(List<EnemyWave> waves, List<EnemyWave.Spawn> spawns)
	{
		if (spawns.Count == 0)
			return;

		waves.Add(new EnemyWave(spawns));
		spawns.Clear();
	}

	private static Enemy.Type GetEnemyType(string token)
	{
		switch (token.ToLower())
		{
			case "wolf":
				return Enemy.Type.Wolf;

			case "bear":
				return Enemy.Type.Bear;

			case "fox":
				return Enemy.Type.Fox;

			case "seal":
				return Enemy.Type.Seal;

			case "polarbear":
				return Enemy.Type.PolarBear;

			case "muskox":
				return Enemy.Type.Muskox;

			default:
				throw new System.FormatException("Invalid enemy type: " + token);
		}
	}
}
