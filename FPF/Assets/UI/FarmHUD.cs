using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmHUD : MonoBehaviour
{
    [SerializeField] private BreedingSystem bs;
    [SerializeField] private InvenotryManager im;
    [SerializeField] private GameObject hud;


    // Start is called before the first frame update
    void Start()
    {
        hud.SetActive(false);
    }

    public void ShowHUD()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hud.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (hud.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            hud.SetActive(false);
        }
    }

    public void OnClick_BuyChicken()
    {
        if (im.money >= 4 && bs.current < bs.Max)
        {
            im.money -= 4;
            bs.NewAnimal(1);
        }
    }

    public void OnClick_BuyCow()
    {
        if (im.money >= 6 && bs.current < bs.Max)
        {
            im.money -= 6;
            bs.NewAnimal(0);
        }
    }
    public void OnClick_BuyPig()
    {
        if (im.money >= 5 && bs.current < bs.Max)
        {
            im.money -= 5;
            bs.NewAnimal(2);
        }
    }

    public void OnClick_Back()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hud.SetActive(false);
    }
}
