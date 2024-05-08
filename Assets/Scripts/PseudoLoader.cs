using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PseudoLoader : MonoBehaviour
{
    public Slider slider;
    public TMP_Text progressText;
    public Animation title, menu;   

    void Awake()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        int progress = 0;

        while (progress < 100)
        {
            progress += Random.Range(1, 5);
            if (progress > 100) progress = 100;
            progressText.text = progress.ToString() + "%";

            slider.value = progress / 100f;

            yield return null;
        }

        title.Play();
        yield return new WaitForSeconds(.5f);
        title.gameObject.SetActive(false);

        GameManager.instance.audioSource.Play();

        menu.gameObject.SetActive(true);
        menu.Play();        
        yield return new WaitForSeconds(.5f);        
    }
}
