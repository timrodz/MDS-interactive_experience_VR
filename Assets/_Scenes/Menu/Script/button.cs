using UnityEngine;
using System.Collections;

public class button : MonoBehaviour
{
    public float ZSelectOffset = -3.0f;

    private Collider MyCollider = null; 
    private Camera MainCamera = null; 
    private GameObject MyGameObject = null; 
    private Transform MyTransform = null; 
    public AudioSource HoverSound = null; 
    public AudioSource SelectSound = null; 

    private Menu MySceneManager = null;

    private Vector3 OriginalPosition = Vector3.zero;
    private Vector3 OffsetPosition = Vector3.zero; 
    private Vector3 CurrentPosition = Vector3.zero; 

    private bool Selected = false;
    private bool Hover = false;

    void Awake()
    {
        MyCollider = GetComponent<Collider>();
        MainCamera = Camera.main;
        MyGameObject = gameObject;
        MyTransform = transform;

        OriginalPosition = MyTransform.localPosition;
        OffsetPosition = OriginalPosition + new Vector3(0.0f, 0.0f, ZSelectOffset);

        CurrentPosition = OriginalPosition;

        MySceneManager = Menu.Instance;
    }

    void Update()
    {
        if (MySceneManager && MySceneManager.ActionSelected)
        {
            // return;
        }

        if (!Selected)
        {
            Ray _ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (MyCollider && MyCollider.Raycast(_ray, out hit, 1000.0f))
            {
                CurrentPosition = OffsetPosition;

                GetComponent<Renderer>().material.color = Color.white;

                if (MySceneManager)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (SelectSound && !SelectSound.isPlaying)
                        {
                            SelectSound.Play();
                        }

                        GetComponent<Renderer>().material.color = Color.red;

                        MySceneManager.OnButtonSelect(MyGameObject.name);

                        //Selected = true;
                    }
                    else
                    {
                        if (HoverSound && !HoverSound.isPlaying && !Hover)
                        {
                            HoverSound.Play();

                            Hover = true;
                        }
                    }
                }
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.grey;
                CurrentPosition = OriginalPosition;
                Hover = false;
            }
        }
        MyTransform.localPosition = Vector3.Lerp(MyTransform.localPosition, CurrentPosition, Time.deltaTime);
    }   
}
