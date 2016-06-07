using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject creditsNames;
    [SerializeField]
    private GameObject howToPlayBox;
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private GameObject htpButton;
    [SerializeField]
    private GameObject creditsButton;
    [SerializeField]
    private GameObject backButton;

    void Start()
    {
        BackButtonHTP();
        creditsNames.SetActive(false);
        howToPlayBox.SetActive(false);
        backButton.SetActive(false);
    }

    void Update()
    {
        BackButtonHTP();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Scene1"); //Load Level
    }

    public void HowToPlayButton()
    {
        playButton.SetActive(false);
        htpButton.SetActive(false);
        creditsButton.SetActive(false);
        howToPlayBox.SetActive(true);
        backButton.SetActive(true);
    }

    public void CreditsButton()
    {
        playButton.SetActive(false);
        htpButton.SetActive(false);
        creditsButton.SetActive(false);
        creditsNames.SetActive(true);
        backButton.SetActive(true);

    }
    public void back()
    {

        creditsNames.SetActive(false);
        playButton.SetActive(true);
        htpButton.SetActive(true);
        creditsButton.SetActive(true);
        howToPlayBox.SetActive(false);
        backButton.SetActive(false);
    }

    public void BackButtonHTP()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            back();
        }       
    }
}
