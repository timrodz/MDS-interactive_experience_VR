using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueEnter : MonoBehaviour {

	public string dialogueText;

	[Range(0.1f, 1.0f)]
	public float fadeTime = 0.5f;

	[Range(0.01f, 0.02f)]
	public float textAnimationDelay = 0.015f;

	private CanvasGroup canvas;

	// Methods //

	void Awake() {

		canvas = GetComponent<CanvasGroup>();

	}

	// Use this for initialization
	void Start() {

		canvas.alpha = 0;

	}

	/// <summary>
	/// Fades the canvas group in
	/// </summary>
	public IEnumerator FadeIn() {

		for (float t = 0.0f; t < 1.0f; t += (Time.deltaTime / fadeTime)) {

			canvas.alpha = t;
			yield return null;

		}

		canvas.alpha = 1;
		StartCoroutine(TextIn());

	}

	/// <summary>
	/// Fades the canvas group out
	/// </summary>
	public IEnumerator FadeOut() {

		StartCoroutine(TextOut());

		yield return new WaitForSeconds(fadeTime);

		for (float t = 1.0f; t > 0.0f; t -= (Time.deltaTime / fadeTime)) {

			canvas.alpha = t;
			yield return null;

		}

		canvas.alpha = 0;


	}

	/// <summary>
	/// Shows the text in a typewriter style
	/// </summary>
	private IEnumerator TextIn() {

		string text = dialogueText;

		for (int i = 0; i <= text.Length; i++) {

			GetComponentInChildren<Text>().text = text.Substring(0, i);
			yield return new WaitForSeconds(textAnimationDelay);

		}

	}

	/// <summary>
	/// Removes the text in a typewriter style
	/// </summary>
	private IEnumerator TextOut() {

		string text = dialogueText;

		for (int i = text.Length; i >= 0; i--) {

			GetComponentInChildren<Text>().text = text.Substring(0, i);
			yield return new WaitForSeconds(textAnimationDelay);

		}

	}

}