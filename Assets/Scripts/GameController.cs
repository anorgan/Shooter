using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;

	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public int score;

	void Start()
	{
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddScore(int addScore)
	{
		score += addScore;
		UpdateScore();
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true) {
			int i = 0;
			while (i < hazardCount) {
				i++;
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition =new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate(hazard, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds(spawnWait);
			}
			hazardCount++;
			yield return new WaitForSeconds(waveWait);
		}
	}
}
