using UnityEngine;
using System.Collections;

public class RotateAnimator : MonoBehaviour
{
    private Transform _myTransform = null;

    public Vector3 _eulers = new Vector3(1.0f, 0.0f, 5.0f);

	void Start ()
    {
        _myTransform = transform;
		//iTween.RotateBy(this.gameObject, iTween.Hash("y", 1, "loopType", "loop"));
	}
	
	void Update ()
    {
        //_myTransform.Rotate(_eulers * Time.deltaTime);
	}
}
