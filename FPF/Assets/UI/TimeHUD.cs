using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeHUD : MonoBehaviour
{
    [SerializeField] private Manager m;
    [SerializeField] private GlobalTime gt;
    [SerializeField] private TextMeshProUGUI timeHUD;
    [SerializeField] private TextMeshProUGUI dayHUD;
    [SerializeField] private TextMeshProUGUI seasonHUD;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager") || !gt)
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
        UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
    }

    private void UpdateHUD() {
        timeHUD.text = gt.hour + "h" + gt.minute;
        dayHUD.text = "day " + m.data.time.dayInSeason + "/" + gt.daysBySeason;
        if (m.data.time.season == 0)
            seasonHUD.text = "printemps";
        else if (m.data.time.season == 1)
            seasonHUD.text = "été";
        else if (m.data.time.season == 2)
            seasonHUD.text = "automne";
        else if (m.data.time.season == 3)
            seasonHUD.text = "hiver";
    }
}
