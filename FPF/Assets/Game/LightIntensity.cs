using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    private Manager m;
    [SerializeField] private Light l;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager") || !l)
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        l.intensity = m.data.time.lightIntensity;
    }
}
