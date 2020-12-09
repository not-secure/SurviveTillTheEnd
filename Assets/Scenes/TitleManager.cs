using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Image title;
    public Text startText;

    public void Start()
    {
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0);
        startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 0);

        StartCoroutine(FadeController());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (startText.color.a < 1)
            {
                title.color = new Color(title.color.r, title.color.g, title.color.b, 1f);
                startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 1f);
            } else
            {
                SceneManager.LoadScene("SceneTest");
                Debug.Log("Go to scene");
            }
        }
    }

    public IEnumerator FadeController()
    {
        while (title.color.a < 1)
        {
            title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a + 0.01f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.5f);

        while (startText.color.a < 1)
        {
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, startText.color.a + 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
