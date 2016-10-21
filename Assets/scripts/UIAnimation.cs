using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VR = UnityEngine.VR;

public class UIAnimation : MonoBehaviour {

	public animationType AnimationType;

	public enum animationType {
		FadeIn,
		FadeOut,
		TextIn,
		TextOut
	};

	public void Animate(animationType _at) {

		switch (_at) {
			case animationType.FadeIn:
				break;
			case animationType.FadeOut:
				break;
			case animationType.TextIn:
				break;
			case animationType.TextOut:
				break;
			default:
				break;
		}

	}

	/// <summary>
	/// Fades the canvas group in
	/// </summary>
	public static IEnumerator FadeIn(CanvasGroup canvas, float fadeTime) {

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
	public static IEnumerator FadeOut(CanvasGroup canvas, float fadeTime) {

		for (float t = 1.0f; t > 0.0f; t -= (Time.deltaTime / fadeTime)) {

			canvas.alpha = t;
			yield return null;

		}

		canvas.alpha = 0;

		canvas = null;

	}

	/// <summary>
	/// Shows the text in a typewriter style
	/// </summary>
	public static IEnumerator TextIn(Text dialogueBox, string strText, float textDelay) {

		for (int i = 0; i <= strText.Length; i++) {

			dialogueBox.text = strText.Substring(0, i);
			yield return new WaitForSeconds(textDelay);

		}

	}

	/// <summary>
	/// Removes the text in a typewriter style
	/// </summary>
	public static IEnumerator TextOut(Text dialogueBox, string strText, float textDelay) {

		for (int i = strText.Length; i >= 0; i--) {

			dialogueBox.text = strText.Substring(0, i);
			yield return new WaitForSeconds(textDelay);

		}

		//StartCoroutine(FadeOut());

	}

}