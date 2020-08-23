using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private    GameObject  settingsMenu;
    private GameObject  gameManager;
    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
        gameManager = GameObject.Find("GameManager");
    }

    private void Update() {
        if (Input.GetKeyDown("space") && !settingsMenu.activeInHierarchy)
            settingsMenu.SetActive(true);
        else if (Input.GetKeyDown("escape") && settingsMenu.activeInHierarchy)
            settingsMenu.SetActive(false);

    }

    public void OnClick_Save()
    {
        gameManager.GetComponent<SaveSystem>().Save();
    }

    public void OnClick_Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClick_CloseMenu()
    {
        settingsMenu.SetActive(false);
    }
}
