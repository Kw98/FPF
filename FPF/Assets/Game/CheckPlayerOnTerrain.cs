using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerOnTerrain : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char_2")
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerActions>().farmingZone = gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Char_2")
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerActions>().farmingZone = null;
    }
}
