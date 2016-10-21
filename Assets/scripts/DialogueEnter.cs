using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueEnter : MonoBehaviour {

	public bool isTestingDialogueSize = true;

	private Text dialogueBoxText;

	private string tempDialogueBoxText;

	[Range(0.1f, 1.0f)]
	public float fadeTime = 0.5f;

	[Range(0.001f, 0.02f)]
	public float textAnimationDelay = 0.01f;

	private CanvasGroup canvas;

	// Methods //

	void Awake() {

		dialogueBoxText = GetComponentInChildren<Text>();
		canvas = GetComponent<CanvasGroup>();

	}

	// Use this for initialization
	void Start() {

		if (!isTestingDialogueSize)
			canvas.alpha = 0;

		#region animatedText
		// Obtain the text from the dialogue box and empty it afterwards
		//tempDialogueBoxText = dialogueBoxText.text;
		//dialogueBoxText.text = "";
		#endregion

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
		//StartCoroutine(TextIn());

	}

	/// <summary>
	/// Fades the canvas group out
	/// </summary>
	public IEnumerator FadeOut() {

		for (float t = 1.0f; t > 0.0f; t -= (Time.deltaTime / fadeTime)) {

			canvas.alpha = t;
			yield return null;

		}

		canvas.alpha = 0;

	}

	/// <summary>
	/// Shows the text in a typewriter style
	/// </summary>
	public IEnumerator TextIn() {

		for (int i = 0; i <= tempDialogueBoxText.Length; i++) {

			dialogueBoxText.text = tempDialogueBoxText.Substring(0, i);
			yield return new WaitForSeconds(textAnimationDelay);

		}

	}

	/// <summary>
	/// Removes the text in a typewriter style
	/// </summary>
	public IEnumerator TextOut() {

		for (int i = tempDialogueBoxText.Length; i >= 0; i--) {

			dialogueBoxText.text = tempDialogueBoxText.Substring(0, i);
			yield return new WaitForSeconds(textAnimationDelay);

		}

		StartCoroutine(FadeOut());

	}

}