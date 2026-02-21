using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public TMP_Text textoAndar;

    public float tempoPreto = 1f;

    private void Awake()
    {
        SetAlpha(0f); 
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(Fader(sceneName, 1f));
    }

    IEnumerator Fader(string sceneName, float duration)
    {
        float t = 0;

        Color imgColor = image.color;
        Color textColor = textoAndar.color;

        // 🔹 Fade IN
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = t / duration;

            imgColor.a = alpha;
            textColor.a = alpha;

            image.color = imgColor;
            textoAndar.color = textColor;

            yield return null;
        }

        yield return new WaitForSeconds(tempoPreto);

        SceneManager.LoadScene(sceneName);

        yield return new WaitForSeconds(0.1f);

       
        t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = 1f - (t / duration);

            imgColor.a = alpha;
            textColor.a = alpha;

            image.color = imgColor;
            textoAndar.color = textColor;

            yield return null;
        }
    }

    void SetAlpha(float alpha)
    {
        Color imgColor = image.color;
        Color textColor = textoAndar.color;

        imgColor.a = alpha;
        textColor.a = alpha;

        image.color = imgColor;
        textoAndar.color = textColor;
    }

    public void Primeiro()
    {
        FadeAndLoad("1 Andar");
    }

    public void Segundo()
    {
        FadeAndLoad("2 Andar");
    }
}