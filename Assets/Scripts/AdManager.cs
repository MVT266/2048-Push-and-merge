using UnityEngine;

public class AdManager : MonoBehaviour
{
    public GameObject[] themes;
    public GameObject confirm, ad;

    private void Start()
    {
        if (GameManager.instance.isAd)
        {
            PlayAd();
            GameManager.instance.isAd = false;
        }
    }

    public void Store(int index)
    {
        if (index != 0 && index != 4 && index != 8 && index != 12 && index != 13 && PlayerPrefs.GetInt("item" + index.ToString()) == 0) confirm.SetActive(true);

        //

        PlayerPrefs.SetInt("item" + index.ToString(), 1);

        switch (index) {
            case 0: case 1: case 2: case 3:
                for (int i = 0; i < 4; i++)
                {
                    themes[i].SetActive(false);
                }
                themes[index].SetActive(true);
                PlayerPrefs.SetInt("theme", index);
                break;
            case 4: case 5: case 6: case 7:
                //
                break;
            case 8: case 9: case 10: case 11:
                //
                break;
            case 12:
                //
                break;
            case 13:
                //
                break;
        }
    }

    public void PlayAd()
    {
        ad.SetActive(true);
    }
}
