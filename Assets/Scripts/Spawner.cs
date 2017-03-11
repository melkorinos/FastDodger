using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject fallingBlockPrefab;
	public GameObject shieldPrefab;
	public GameObject mushroomPrefab;

	public Vector2 secondsBetweenSpawnsMinMax;
	float nextSpawnTime = 1;


	public Vector2 spawnSizeMinMax;
	public float spawnAngleMax;

	public float shieldSecondsBetweenSpawns;
	public float mushroomSecondsBetweenSpawns;
	float shieldNextSpawnTime =5;
	float mushroomNextSpawnTime = 10;

	Vector2 	screenHalfSize;

	// Use this for initialization
	void Start () {

		//get game space sizes
		screenHalfSize = new Vector2 (Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
	}

	// Update is called once per frame
	void Update () {
		//Spawn Asteroids
		if (Time.timeSinceLevelLoad > nextSpawnTime) {
			float secondsBetweenSpawns = Mathf.Lerp (secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.getDifPercent ());
			nextSpawnTime += secondsBetweenSpawns;
			float spawnSize = Random.Range (spawnSizeMinMax.x, spawnSizeMinMax.y);
			Vector2 spawnPosition = new Vector2 (Random.Range (-screenHalfSize.x, screenHalfSize.x), screenHalfSize.y + spawnSize);

			//give spawn angle depending on spawn location
			float spawnAngle;
			if (spawnPosition.x >= 0) {
				spawnAngle = Random.Range (-spawnAngleMax, spawnAngleMax/2);
			} else {
				spawnAngle = Random.Range (-spawnAngleMax/2, spawnAngleMax);
			}

			GameObject newblock = (GameObject)Instantiate (fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
			newblock.transform.localScale = (Vector2.one * spawnSize);

		}
		//Spawn Shields
		if (Time.timeSinceLevelLoad > shieldNextSpawnTime) {
			shieldNextSpawnTime += Random.Range (shieldSecondsBetweenSpawns/2, shieldSecondsBetweenSpawns);

			Vector2 spawnPosition = new Vector2 (Random.Range (-screenHalfSize.x, screenHalfSize.x), screenHalfSize.y + shieldPrefab.transform.localScale.y);
			Instantiate (shieldPrefab, spawnPosition, Quaternion.identity);
		}

		//Spawn mushrooms
		if (Time.timeSinceLevelLoad > mushroomNextSpawnTime) {
			mushroomNextSpawnTime += Random.Range (mushroomSecondsBetweenSpawns/2, mushroomSecondsBetweenSpawns);

			Vector2 spawnPosition = new Vector2 (Random.Range (-screenHalfSize.x, screenHalfSize.x), screenHalfSize.y + shieldPrefab.transform.localScale.y);
			Instantiate (mushroomPrefab, spawnPosition, Quaternion.identity);
		}
	}


}