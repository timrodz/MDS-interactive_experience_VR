﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Shows the dialogue boxes
/// </summary>
public class DialoguePrompt : MonoBehaviour {

	/// <summary>
	/// The key that the player will interact with
	/// </summary>
	public KeyCode interactionKey = KeyCode.Space;
	
	/// <summary>
	/// The time in which the interaction prompt will fade in/out
	/// </summary>
	public float dialoguePromptFadeTime = 1f;

	// Animation values
	[Range (0.1f, 1f)]
	private float containerTransitionValue = 0.2f;

	// others
	private bool canInteractWithDialogue = false;
	private bool hasInteractedWithDialogue = false;
	private CanvasGroup promptCanvasGroup = null;
	private List<CanvasGroup> containers;

	void Start() {

		containers = new List<CanvasGroup>();

	}

	void Update() {

		if (canInteractWithDialogue) {

			// If the user presses E
			if (Input.GetKeyDown(interactionKey)) {

				// Make the dialogue box visible
				if (!hasInteractedWithDialogue) {

					iTween.MoveBy(promptCanvasGroup.gameObject, iTween.Hash("y", -1, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1));

					foreach (CanvasGroup child in containers) {

						StartCoroutine(UIAnimation.FadeIn(child, dialoguePromptFadeTime));
						iTween.MoveBy(child.gameObject, iTween.Hash("y", containerTransitionValue, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1));

					}

					hasInteractedWithDialogue = true;

				}
				// Make the dialogue box invisible
				else {

					iTween.MoveBy(promptCanvasGroup.gameObject, iTween.Hash("y", 1, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1));

					foreach (CanvasGroup child in containers) {

						StartCoroutine(UIAnimation.FadeOut(child, dialoguePromptFadeTime));
						iTween.MoveBy(child.gameObject, iTween.Hash("y", -containerTransitionValue, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0));

					}

					hasInteractedWithDialogue = false;

				}

			} // !Input.GetKeyDown(KeyCode.E)

		} // !canInteractWithDialogue

	}

	// This method is called everytime the entity enters a collider that is set to work as a trigger
	void OnTriggerEnter(Collider other) {

		if (other.tag == "Dialogue") {

			// Access the canvas group object of the current object
			CanvasGroup[] cvGroups = other.GetComponentsInChildren<CanvasGroup>();

			// The first canvas group will be the prompt text
			promptCanvasGroup = cvGroups[0];

			// The remaining objects are the containers
			for (int i = 1; i < cvGroups.Length; i++) {
				containers.Add(cvGroups[i]);
			}

			StartCoroutine(UIAnimation.FadeIn(promptCanvasGroup, dialoguePromptFadeTime));

			foreach (CanvasGroup child in containers) {
				if (child.alpha == 1)
					hasInteractedWithDialogue = true;
			}

			canInteractWithDialogue = true;

		}

	}


	/// <summary>
	/// // This method is called everytime the entity exits a collider that is set to work as a trigger
	/// </summary>
	/// <param name="other"> the trigger that caused the collision</param>
	void OnTriggerExit(Collider other) {

		if (other.tag == "Dialogue") {

			// Reset the variable states
			StartCoroutine(UIAnimation.FadeOut(promptCanvasGroup, dialoguePromptFadeTime));
			containers.Clear();
			hasInteractedWithDialogue = false;
			canInteractWithDialogue = false;

		}

	}

}