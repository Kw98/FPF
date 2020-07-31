using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour

{
    public Texture[] texture;
    public int currentTexture;
    public GameObject Player;
    private 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextSkin()
    {
        currentTexture++;
        currentTexture %= texture.Length;
        Player.GetComponent<Renderer>().material.mainTexture = texture[currentTexture];
    }

    public void PreviousSkin()
    {
        currentTexture--;
        if (currentTexture < 0)
            currentTexture = texture.Length -1 ;
        else
            currentTexture %= texture.Length;
        Player.GetComponent<Renderer>().material.mainTexture = texture[currentTexture];
    }


}
