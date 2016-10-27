using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Transform CamWayPoint = null;   
    public GameObject TitleObject = null;   
    public GameObject OfObject = null;   
    public GameObject GameObject = null;   
    public GameObject MenuObject = null;     
    public float BOSInitialDelay = 1.11f; 
    public float BOSWordDelay = 0.75f;  

    private Vector3 OriginalComposition = Vector3.zero; 
    private Vector3 TargetComposition = Vector3.zero; 
    private Transform CameraTransform = null; 

    public bool ActionSelected = false;

    protected float ScreenFade = 1.0f;

    protected static Menu _Instance = null;

    public GameObject Screen1 = null;
    public GameObject Screen2 = null;


    public static Menu Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (Menu)FindObjectOfType(typeof(Menu));
            }

            return _Instance;
        }
    }

    void Awake()
    {
        AudioListener.volume = 1.0f;

        if (Camera.main)
        {
            CameraTransform = Camera.main.transform;

            OriginalComposition = CameraTransform.position;
        }

        if (CamWayPoint)
        {
            TargetComposition = CamWayPoint.transform.position;
        }
    }

    void Start()
    {
        ScreenFade = 0.0f;

        Cursor.visible = true;

        if (Screen1)
        {
            Screen1.SetActive(true);
        }
        if (Screen2)
        {
            Screen2.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider c)
    {        
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }

        float accum = BOSInitialDelay;

        Invoke("RevealTitle", accum);

        accum += BOSWordDelay;
        Invoke("RevealOf", accum);

        accum += BOSWordDelay;
        Invoke("RevealGame", accum);

        accum += BOSWordDelay;
        Invoke("RevealMenu", accum);
    }

    private void RevealTitle()
    {
        if (TitleObject)
        {
            TitleObject.SetActive(true);
        }
    }

    private void RevealOf()
    {
        if (OfObject)
        {
            OfObject.SetActive(true);
        }
    }

    private void RevealGame()
    {
        if (GameObject)
        {
            GameObject.SetActive(true);
        }
    }

    private void RevealMenu()
    {
        if (MenuObject)
        {
            MenuObject.SetActive(true);
        }
    }    

    public void OnButtonSelect(string buttonName)
    {     
        if (ActionSelected)
        {
            return;
        }

        // If the play button has been selected start the coroutine that animates the camera through the hole and then load the next scene
        if (buttonName == "Play")
        {
            ActionSelected = true;
            StartCoroutine(LoadGameScene());
        }
        // Otherwise, if the Quit button is pressed start the Coroutine that fades the scene and then loads in the Closing credits scene.
        else if (buttonName == "Quit")
        {
            ActionSelected = true;
            StartCoroutine(QuitGame());
        }
        else if (buttonName == "Helplines")
        {
            ActionSelected = true;        
            NextScreen();
            ActionSelected = false;
        }
        else if (buttonName == "Website")
        {
            ActionSelected = true;
            Application.OpenURL("www.mentalhealth.org.nz/get-help/in-crisis/helplines/");
            ActionSelected = false;
        }
        else if (buttonName == "Exit")
        {
            ActionSelected = true;
            NextScreen();
            ActionSelected = false;
        }
    }   

    private IEnumerator LoadGameScene()
    {
        Renderer[] renderers = MenuObject.GetComponentsInChildren<Renderer>();

        float timer = 1.0f;

        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;

            foreach (Renderer r in renderers)
            {
                if (r && r.material)
                {
                    Color col = r.material.color;

                    col.a = timer;
                    r.material.color = col;
                }
            }
            yield return null;
        }

        timer = 1.5f;

        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;

            if (timer >= 0.0f)
            {
                ScreenFade = 1.0f - (timer / 1.5f);
            }

            if (timer >= 0.0f)
            {
                CameraTransform.position = Vector3.Lerp(OriginalComposition, TargetComposition, 1 - (timer / 1.5f));
            }

            AudioListener.volume = timer / 1.5f;

            yield return null;
        }
        SceneManager.LoadScene("VR ROOM");
    }

    private IEnumerator QuitGame()
    {
        float timer = 2.5f;

        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;

            if (timer >= 0.0f)
            {
                ScreenFade = 1.0f - (timer / 2.5f);

            }
            AudioListener.volume = timer / 2.5f;

            yield return null;
        }
        SceneManager.LoadScene("Credits");
    }

    public void NextScreen()
    {
        if (Screen1)
        {
            Screen1.SetActive(!Screen1.activeSelf);
        }
        if (Screen2)
        {
            Screen2.SetActive(!Screen2.activeSelf);
        }
    }
}
