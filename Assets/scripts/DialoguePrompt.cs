using UnityEngine;
using UnityEngine.UI;

public class DialoguePrompt : MonoBehaviour {
	
	public Text dialoguePromptText;
	
	private bool canInteractWithDialogue = false;
	
	private bool hasInteractedWithDialogue = false;

	private CanvasGroup currentCanvasGroup = null;

	private DialogueEnter dialogueScript = null;
	
	// Called before the object is created
	void Awake() {
	
		// Disable the dialogue prompt text	
		dialoguePromptText.enabled = false;
		
	}
	
	void Update() {
		
		if (canInteractWithDialogue) {
			
			// If the user presses E
			if (Input.GetKeyDown(KeyCode.E)) {
				
				// Make the dialogue box visible
				if (!hasInteractedWithDialogue) {
					
//					currentCanvasGroup.alpha = 1;
					StartCoroutine(dialogueScript.FadeIn());
					hasInteractedWithDialogue = true;

				}
				// Make the dialogue box invisible
				else {
					
//					currentCanvasGroup.alpha = 0;
					StartCoroutine(dialogueScript.FadeOut());
					hasInteractedWithDialogue = false;

				}
				
			} // !Input.GetKeyDown(KeyCode.E)
			
		} // !canInteractWithDialogue

	}
	
	// This method is called everytime the entity enters a collider that is set to work as a trigger
	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Dialogue") {
			
			// Access the canvas group object of the current 
			currentCanvasGroup = other.GetComponentInChildren<CanvasGroup>();
			dialogueScript = other.GetComponentInChildren<DialogueEnter>();
			
			if (currentCanvasGroup.alpha == 1) {
				hasInteractedWithDialogue = true;
			}
			
			canInteractWithDialogue = true;
			dialoguePromptText.enabled = true;
			
		}
		
	}
	
	// This method is called everytime the entity exits a collider that is set to work as a trigger
	void OnTriggerExit(Collider other) {
		
		if (other.tag == "Dialogue") {
			
			// Reset the canvas group
			currentCanvasGroup = null;
			dialogueScript = null;
			hasInteractedWithDialogue = false;
			canInteractWithDialogue = false;
			dialoguePromptText.enabled = false;
			
		}
		
	}
	
}