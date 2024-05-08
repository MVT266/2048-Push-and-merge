using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelButton;
    public Transform[] panels;

    void Awake()
    {
        for (int a = 0; a < 3; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                GameObject newButton = Instantiate(levelButton, levelButton.transform.position, levelButton.transform.rotation, panels[a]);

                RectTransform rt = newButton.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(rt.position.x + (b % 2) * 300, rt.position.y - Mathf.Floor(b / 2) * 300);

                int index = a * 10 + b;

                if (index <= PlayerPrefs.GetInt("progress"))
                {
                    TMP_Text label = newButton.transform.Find("Label").GetComponent<TextMeshProUGUI>();
                    label.text = (index + 1).ToString();

                    Button button = newButton.GetComponent<Button>();
                    button.onClick.AddListener(() => GameManager.instance.LoadScene(index + 1));

                    Image image = newButton.GetComponent<Image>();
                    image.color = Color.HSVToRGB((index * 12) / 360f, .5f, 1f);
                }  
            }
        }
    }
}
