using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject[] Hazards;
	public Vector3 spawnvalues;
	public int HazardsCount;
	public float spawnwait;
	public float startwait;
	public float wavewait;
	public Text RestartText;
	public Text GameOverText;

	private bool gameOver;
	private bool restart;


	// Use this for initialization
	void Start()
	{
		StartCoroutine (SpawnWaves());
		gameOver = false;
		restart = false;
		RestartText.text = "";
		GameOverText.text = "";	

	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				LevelManager obj = GameObject.Find("LevelManager").GetComponent<LevelManager>();
				obj.LoadLevel("Game");
			}
			if (Input.GetKeyDown(KeyCode.Q)){
				LevelManager obj = GameObject.Find("LevelManager").GetComponent<LevelManager>();
				obj.LoadLevel("Start Menu");
			}
		}
	}
	// Update is called once per frame
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startwait); // czas na przygotowanie 
		while (true)
		{
			for (int i = 0; i < HazardsCount; i++)
			{
				GameObject hazard = Hazards[Random.Range(0, Hazards.Length)];
				Vector3 spawnposition = new Vector3(Random.Range(-spawnvalues.x, spawnvalues.x), spawnvalues.y, 0);
				Quaternion spawnrotation = Quaternion.identity;
				Instantiate(hazard, spawnposition, spawnrotation);
				yield return new WaitForSeconds(spawnwait); // co ile powtarza

			}
			yield return new WaitForSeconds(wavewait);
			if (gameOver)
			{
				RestartText.text = "Press R for Restart Q for Quit";
				restart = true;
				break;
			}
		}
	}

	public void GameOver()
	{
		GameOverText.text = "Game over";
		gameOver = true;


	}
}
