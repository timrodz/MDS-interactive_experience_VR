using UnityEngine;
using UnityEngine.UI;

public class DialoguePrompt : MonoBehaviour {

	/// <summary>
	/// The key that the player will interact with
	/// </summary>
	public KeyCode interactionKey;

	private bool canInteractWithDialogue = false;	
	private bool hasInteractedWithDialogue = false;
	private CanvasGroup containerCanvasGroup = null;
	private CanvasGroup promptCanvasGroup = null;
	private DialogueEnter dialogueScript = null;
	
	void Update() {
		
		if (canInteractWithDialogue) {
			
			// If the user presses E
			if (Input.GetKeyDown(interactionKey)) {
				
				// Make the dialogue box visible
				if (!hasInteractedWithDialogue) {

					iTween.MoveBy(promptCanvasGroup.gameObject, 
						iTween.Hash("y", -1.2, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1)
						);

					StartCoroutine(UIAnimation.FadeIn(containerCanvasGroup, 1));
					//StartCoroutine(dialogueScript.FadeIn());
					hasInteractedWithDialogue = true;

				}
				// Make the dialogue box invisible
				else {

					iTween.MoveBy(promptCanvasGroup.gameObject,
						iTween.Hash("y", 1.2, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1)
						);

					StartCoroutine(UIAnimation.FadeOut(containerCanvasGroup, 1));
					//StartCoroutine(dialogueScript.FadeOut());
					hasInteractedWithDialogue = false;

				}
				
			} // !Input.GetKeyDown(KeyCode.E)
			
		} // !canInteractWithDialogue

	}
	
	// This method is called everytime the entity enters a collider that is set to work as a trigger
	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Dialogue") {

			// Access the canvas group object of the current object
			CanvasGroup[] dialogueBoxCanvasGroups = other.GetComponentsInChildren<CanvasGroup>();

			promptCanvasGroup = dialogueBoxCanvasGroups[0];
			containerCanvasGroup = dialogueBoxCanvasGroups[1];
			//dialogueScript = other.GetComponentInChildren<DialogueEnter>();

			StartCoroutine(UIAnimation.FadeIn(promptCanvasGroup, 1));
			//promptCanvasGroup.alpha = 1;

			if (containerCanvasGroup.alpha == 1) {
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
			StartCoroutine(UIAnimation.FadeOut(promptCanvasGroup, 1));
			//promptCanvasGroup.alpha = 0;
			//promptCanvasGroup = null;
			containerCanvasGroup = null;
			//dialogueScript = null;
			hasInteractedWithDialogue = false;
			canInteractWithDialogue = false;
			
		}
		
	}
	
}