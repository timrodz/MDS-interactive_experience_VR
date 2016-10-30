using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	/// <summary>
	/// The scene that will be loaded (defaults to 1)
	/// </summary>
	public int sceneToStart = 1;

	/// <summary>
	/// Animation clip fading to color (black default) when changing scenes
	/// </summary>
	public AnimationClip fadeColorAnimationClip;

	//[HideInInspector]
	/// <summary>
	/// The animator that controls the fade animation
	/// </summary>
	public Animator animColorFade;

	/// <summary>
	/// The title of the game
	/// </summary>
	public Transform gameTitle;

	/// <summary>
	/// Different canvas groups being used
	/// </summary>
	public CanvasGroup[] canvasGroups;

	private int currentCanvasIndex = 0;

	void Start() {

		for (int i = 1; i < canvasGroups.Length; i++) {

			canvasGroups[i].alpha = 0;
			canvasGroups[i].blocksRaycasts = false;

		}

	}

	#region ALL_PURPOSE
	public void FadeInTo(CanvasGroup cv) {

		GetCurrentCanvasIndex();

		if (currentCanvasIndex + 1 >= canvasGroups.Length)
			return;

		// Fade out the current canvas group upwards
		StartCoroutine(UIAnimation.FadeOut(canvasGroups[currentCanvasIndex], 1));
		iTween.MoveBy(canvasGroups[currentCanvasIndex].gameObject, iTween.Hash("y", 1, "speed", 1, "easeType", "easeInOutExpo"));

		currentCanvasIndex++;

		StartCoroutine(FadeObjectIn(cv, 1, 0.3f));

	}

	public void FadeOutTo(CanvasGroup cv) {

		GetCurrentCanvasIndex();

		if (currentCanvasIndex - 1 < 0)
			return;

		// Fade out the current canvas group downwards
		StartCoroutine(UIAnimation.FadeOut(canvasGroups[currentCanvasIndex], 1));
		iTween.MoveBy(canvasGroups[currentCanvasIndex].gameObject, iTween.Hash("y", -1, "speed", 1, "easeType", "easeInOutExpo"));

		currentCanvasIndex--;

		StartCoroutine(FadeObjectIn(cv, -1, 0.3f));

	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="cv">The canvas group</param>
	/// <param name="moveAmount">The amount to translate</param>
	private IEnumerator FadeObjectIn(CanvasGroup cv, float moveAmount, float timeToWait) {

		yield return new WaitForSeconds(timeToWait);
		
		StartCoroutine(UIAnimation.FadeIn(cv, 1));
		iTween.MoveBy(cv.gameObject, iTween.Hash("y", moveAmount, "speed", 1, "easeType", "easeInOutCubic", "delay", 0.1f));

	}

	/// <summary>
	/// Gets the current canvas index based on alpha values
	/// </summary>
	void GetCurrentCanvasIndex() {

		for (int i = 0; i < canvasGroups.Length; i++) {

			if (canvasGroups[i].alpha == 1) {
				currentCanvasIndex = i;
				break;
			}

		}

	}
	#endregion

	#region MAIN_MENU
	public void LoadScene() {

		Invoke("LoadSceneDelayed", fadeColorAnimationClip.length * 0.5f);

		iTween.MoveBy(gameTitle.gameObject, iTween.Hash("y", 5, "speed", 2, "easeType", "easeInOutExpo", "loopType", "none"));
		iTween.MoveBy(canvasGroups[1].gameObject, iTween.Hash("y", -5, "speed", 2, "easeType", "easeInOutExpo", "loopType", "none"));

		//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
		animColorFade.SetTrigger("fade");

	}

	private void LoadSceneDelayed() {

		SceneManager.LoadScene(sceneToStart);

	}

	public void Quit() {

		Application.Quit();

	}
	#endregion

}