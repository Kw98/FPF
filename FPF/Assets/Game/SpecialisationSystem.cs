using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialisationSystem : MonoBehaviour
{
    private Manager m;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
        if (m.data.player.specialisation.general.actualLvl == 1)
            m.data.player.specialisation.general.totalExpNextLvl = 10000;
        m.data.player.specialisation.general.maxLvl = 10;
    }

    public void GainExp(string spec, int amount)
    {
        if (spec == "tomato")
            m.data.player.specialisation.tomato.GainExp(amount);
        else if (spec == "carrot")
            m.data.player.specialisation.carrot.GainExp(amount);
        else if (spec == "corn")
            m.data.player.specialisation.corn.GainExp(amount);
        else if (spec == "eggplant")
            m.data.player.specialisation.eggplant.GainExp(amount);
        else if (spec == "pumpkin")
            m.data.player.specialisation.pumpkin.GainExp(amount);
        else if (spec == "turnip")
            m.data.player.specialisation.turnip.GainExp(amount);
        else if (spec == "wood")
            m.data.player.specialisation.wood.GainExp(amount);
        else if (spec == "stone")
            m.data.player.specialisation.stone.GainExp(amount);
        else if (spec == "chicken")
            m.data.player.specialisation.chicken.GainExp(amount);
        else if (spec == "pig")
            m.data.player.specialisation.pig.GainExp(amount);
        else if (spec == "cow")
            m.data.player.specialisation.cow.GainExp(amount);
        m.data.player.specialisation.general.GainExp(amount);
    }

}
