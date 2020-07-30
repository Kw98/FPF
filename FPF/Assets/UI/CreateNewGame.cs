using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateNewGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI   input;  

    public void    OnClick_CreateGame()
    {
        GameObject gm = GameObject.Find("GameManager");
        gm.GetComponent<Manager>().NewGame(input.text);
        SceneManager.LoadScene("Map_begin");
    }
}
