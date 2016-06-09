using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pouse : MonoBehaviour {

    [SerializeField]
    private GameObject pouseMenu;
    private bool pouse;

    // Use this for initialization
    void Start () {
        pouse = false;
        pouseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            Poused();
        }
    }
    public void Poused()
    {
        if(pouse)
        {
            pouse = false;
            Time.timeScale = 1;
        }
        else
        {

            Time.timeScale = 0;
            pouse = true;
        }
        pouseMenu.SetActive(pouse);
    }
    public void ToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenu");
    }
}
