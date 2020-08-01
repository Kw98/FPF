using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temps de jeu total sur la partie
public class GlobalTime : MonoBehaviour
{
    private Manager manager;
    private float   lastTime;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager")) {
            Destroy(this);
            return;
        }
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        manager.data.time.timePlayed += Time.time - lastTime;
        lastTime = Time.time;
        Debug.Log(manager.data.time.timePlayed);
    }
}
