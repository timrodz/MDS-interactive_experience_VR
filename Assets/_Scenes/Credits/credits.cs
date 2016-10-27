using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Credit
{
    public string Line1;
    public string Line2;
}

public class credits : MonoBehaviour {

    public TextMesh CreditMesh1 = null;
    public TextMesh CreditMesh2 = null;

    public List<Credit> CreditRoll = new List<Credit>();

    void Awake()
    {
        AudioListener.volume = 1.0f;
    }

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        //ScreenFade = 0.0f;
        StartCoroutine(DoCredits());
	}

    private IEnumerator DoCredits()
    {
        Material material1 = null, material2 = null;
        
        if(CreditMesh1 && CreditMesh1.GetComponent<Renderer>())
        {
            material1 = CreditMesh1.GetComponent<Renderer>().material;
        }

        if (CreditMesh2 && CreditMesh2.GetComponent<Renderer>())
        {
            material2 = CreditMesh2.GetComponent<Renderer>().material;
        }

        Color color1 = new Color(0.5f, 0.0f, 0.0f, 0);
        Color color2 = new Color(0.7f, 0.7f, 0.7f, 0);

        material1.color = color1;
        material2.color = color2;

        float timer = 0;
        while (timer < 1.0f)
        {
            if (Input.anyKeyDown)
            {
                Application.Quit();
            }

            timer += Time.deltaTime;

            yield return null;
        }

        if (material1 != null && material2 != null)
        {
            for (int i = 0; i < CreditRoll.Count; i++)
            {
                Credit credit = CreditRoll[i];

                CreditMesh1.text = credit.Line1;
                CreditMesh2.text = credit.Line2;

                timer = 0.0f;
                while (timer < 4.0f)
                {
                    if (Input.anyKeyDown)
                    {
                        Application.Quit();
                    }
                    timer += Time.deltaTime;

                    color1.a = color2.a = timer / 4.0f;

                    material1.color = color1;
                    material2.color = color2;

                    yield return null;
                }

                timer = 0.0f;
                while (timer < 3.0f)
                {
                    if (Input.anyKeyDown)
                    {
                        Application.Quit();
                    }
                    timer += Time.deltaTime;

                    color1.a = color2.a = 1 - timer / 3.0f;

                    material1.color = color1;
                    material2.color = color2;

                    yield return null;
                }

                if (i != CreditRoll.Count - 1)
                {
                    timer = 0.0f;
                    while (timer < 1.0f)
                    {
                        if (Input.anyKeyDown)
                        {
                            Application.Quit();
                        }
                        timer += Time.deltaTime;

                        yield return null;
                    }
                }
            }
        }
        timer = 0.0f;

        timer = 0.0f;
        while (timer < 4.0f)
        {
            if (Input.anyKeyDown)
            {
                Application.Quit();
            }
            timer += Time.deltaTime;
           // ScreenFade = timer / 4.0f;

            color1.a = color2.a = timer / 4.0f;

            AudioListener.volume = 1 - (timer / 4.0f);

            yield return null;
        }

        Application.Quit();
    }  

}
