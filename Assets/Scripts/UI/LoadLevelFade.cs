using UnityEngine;
using System.Collections;

public class LoadLevelFade : MonoBehaviour {

	/// <summary>
	/// The time to fade out
	/// </summary>
	public float fadeOutTime = 1f;

	void OnLevelWasLoaded() {
		
		StartCoroutine(UIAnimation.FadeOut(this.GetComponent<CanvasGroup>(), fadeOutTime));

	}

}