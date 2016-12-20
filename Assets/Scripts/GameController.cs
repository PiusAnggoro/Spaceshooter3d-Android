using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	//private GUIStyle guiStyle = new GUIStyle();
	//private string restartText;
	//private string gameOverText;

	private int score;
	public Text scoreText;
	public Text gameOverText;
	public GameObject restartButton;

	private bool gameOver;
	//private bool restart;

	void Start (){
		/*
		guiStyle.fontSize = 34;
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.normal.textColor = Color.white;
		restartText = "";
		gameOverText = "";
		restart = false;
		*/
		gameOver = false;
		gameOverText.text = "";
		restartButton.SetActive (false);
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	/*
	void Update (){
		if (restart){
			if (Input.GetKeyDown (KeyCode.R)){
				SceneManager.LoadScene("Main");
			}
		}
	}
	*/

	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while (true){
			for (int i = 0; i < hazardCount; i++){
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);	

			if (gameOver) {
				//restartText = "Tekan 'R' untuk mulai! ";
				//restart = true;
				restartButton.SetActive (true);
				break;
			}
		}
	}
		
	/* versi lama
	public void AddScore (int newScoreValue){
		score += newScoreValue;
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20), "Nilai: " + score, guiStyle);
		GUI.Label(new Rect(Screen.width /2, Screen.height /2, 100, 20), gameOverText, guiStyle);
		GUI.Label(new Rect(Screen.width *2/3, 10, 100, 20), restartText, guiStyle);
	}

	public void GameOver(){
		gameOverText = "Game Selesai! ";
		gameOver = true;
	}
	*/

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore (){
		scoreText.text = "Nilai: " + score;
	}

	public void GameOver (){
		gameOverText.text = "Game Selesai! \n Terima kasih";
		gameOver = true;
	}

	public void RestartGame () {
		SceneManager.LoadScene("Main");
	}
}