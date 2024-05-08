using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text currentScoreText, progressText;
    public int currentScore, goalScore;
    public AudioClip[] audioClips;

    public Slider slider;
    public TMP_Text[] bonusTexts;
    public GameObject volume, vibration, w, l, prize, received, rate;
    
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public bool isAd;

    private int rateCounter;

    private void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();

        UpdateScoreText();
        UpdateVolumeVibration();
    }

    private void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    private void UpdateScoreText()
    {
        if (currentScoreText != null) currentScoreText.text = currentScore.ToString() + "/<color=yellow>" + goalScore.ToString() + "</color>";
    }

    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText();

        if (currentScore >= goalScore)
        {
            PlayerPrefs.SetInt("prize", PlayerPrefs.GetInt("prize") + 20);

            if (PlayerPrefs.GetInt("prize") == 100)
            {
                PlayerPrefs.SetInt("prize", 0);

                int[] bonuses = { 1, 1 };

                int random = Random.Range(0, 100);
                if (random < 40) bonuses[0] = 2;
                else if (random < 80) bonuses[1] = 2;
                else
                {
                    bonuses[0] = 2;
                    bonuses[1] = 2;
                }

                bonusTexts[0].text = bonuses[0].ToString();
                bonusTexts[1].text = bonuses[1].ToString();

                PlayerPrefs.SetInt("power1", PlayerPrefs.GetInt("power1") + bonuses[0]);
                PlayerPrefs.SetInt("power2", PlayerPrefs.GetInt("power2") + bonuses[1]);

                prize.SetActive(false);
                received.SetActive(true);
            }
            else
            {
                progressText.text = PlayerPrefs.GetInt("prize").ToString() + "%";

                slider.value = PlayerPrefs.GetInt("prize");
            }

            SetTimeScale(0);
            w.SetActive(true);

            audioSource.Stop();
            audioSource.PlayOneShot(audioClips[4]);

            rateCounter++;
            if (rateCounter % 5 == 0) rate.SetActive(true);
        }
    }

    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    private void UpdateVolumeVibration()
    {
        volume.SetActive(PlayerPrefs.GetFloat("volume") == 0 ? true : false);
        vibration.SetActive(PlayerPrefs.GetFloat("vibration") == 0 ? true : false);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);        
    }

    public void SetVibration(float volume)
    {
        PlayerPrefs.SetFloat("vibration", volume);
    }


    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index > 30 ? 0 : index);
    }

    public void MoveScene(int index)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex + index > 30 ? 0 : buildIndex + index);
    }

    public void OpenURL(string url)
    {

        Application.OpenURL(url);
    }
}
