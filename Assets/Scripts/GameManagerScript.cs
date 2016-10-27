using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	void Awake() {

		DontDestroyOnLoad(gameObject);

	}

	public void LoadScene(int _scene) {

		StartCoroutine(LoadSceneIE(_scene, 2));

	}

	IEnumerator LoadSceneIE(int _scene, float waitTime) {

		Transform player = GameObject.FindGameObjectWithTag("Player").transform;

		iTween.MoveBy(player.gameObject, iTween.Hash("y", 5, "speed", 2, "easeType", "easeInOutExpo", "loopType", "none"));

		yield return new WaitForSeconds(waitTime);

		print("Loading scene");

		SceneManager.LoadScene(_scene);

	}

}