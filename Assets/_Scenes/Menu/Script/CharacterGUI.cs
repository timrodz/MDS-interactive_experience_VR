using UnityEngine;
using System.Collections;

public class CharacterGUI : MonoBehaviour
{
    public GameObject Frame1 = null;
    public GameObject Frame2 = null;

    public GameObject NameText = null;

    public float StartOffset = 2;

    public float Speed = 1.0f;

    public float FrameSpeed = 0.25f;

    public bool SequenceTerminator = false;

    public Vector3 _target = Vector3.zero;
    public Vector3 _start = Vector3.zero;
    public Vector3 _end = Vector3.zero;

    private Vector3 _current = Vector3.zero;

    private Transform _myTransform;

    private float _interpolator = 0.0f;

    private bool _sequenceRunning = false;

    private float _timer = 0.0f;
    private float _oldTimer = 0.0f;

    private bool _animate = true;

    void Awake()
    {
        _myTransform = transform;
        _target = _myTransform.localPosition;
    }

    void OnEnable()
    {
        _start = _end = _target;

        _start.x = _target.x + 100.0f;
        _end.x = _target.x - 100.0f;

        _myTransform.localPosition = _start;

        if(NameText != null)
        {
            NameText.SetActive(false);
        }

        _timer = 0;
        _oldTimer = 0;
        _animate = true;
        _interpolator = 0.0f;

        _sequenceRunning = false;
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
        _timer += Time.deltaTime;

        if (!_sequenceRunning && _timer > StartOffset)
        {
            StartCoroutine(AnimatedSequence());
        }

        if (Frame1 != null && Frame2 != null && _animate)
        {
            if (_timer - _oldTimer > FrameSpeed)
            {
                _oldTimer = _timer;

                Frame1.SetActive(!Frame1.activeSelf);
                Frame2.SetActive(!Frame2.activeSelf);
            }
        }
	}

    private IEnumerator AnimatedSequence()
    {
        _sequenceRunning = true;

        _myTransform.localPosition = _start;

        _interpolator = 0.0f;
        _animate = true;

        if (Frame1 && Frame1.GetComponent<Renderer>())
        {
            Frame1.GetComponent<Renderer>().enabled = true;
        }
        if (Frame2 && Frame2.GetComponent<Renderer>())
        {
            Frame2.GetComponent<Renderer>().enabled = true;
        }
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().enabled = true;
        }

        while ( _interpolator < 1.0f )
        {
            _interpolator += Time.deltaTime * Speed;

            _current = Vector3.Lerp(_start, _target, _interpolator);

            _myTransform.localPosition = _current;

            yield return null;
        }

        _animate = false;

        if(NameText != null)
        {
            NameText.SetActive(true);
        }

        yield return new WaitForSeconds(6);

        if (NameText != null)
        {
            NameText.SetActive(false);
        }

        _animate = true;

        _interpolator = 0.0f;

        while (_interpolator < 1.0f)
        {
            _interpolator += Time.deltaTime * Speed;
            _current = Vector3.Lerp(_target, _end, _interpolator);
            _myTransform.localPosition = _current;
            yield return null;
        }

        if (Frame1 && Frame1.GetComponent<Renderer>())
        {
            Frame1.GetComponent<Renderer>().enabled = false;
        }
        if (Frame2 && Frame2.GetComponent<Renderer>())
        {
            Frame2.GetComponent<Renderer>().enabled = false;
        }
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().enabled = false;
        }

        yield return new WaitForSeconds(20);

        _sequenceRunning = false;
    }

    void OnDisable()
    {
        _sequenceRunning = false;
    }
}
