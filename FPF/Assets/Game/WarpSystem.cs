using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour
{
    [SerializeField] private GameObject TpTo;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger: " + other.gameObject.name);
        //Debug.Log("parent: " + other.gameObject.transform.parent.name);
        Debug.Log("before pos: " + other.gameObject.transform.position);
        other.gameObject.transform.position = TpTo.transform.position;
        Debug.Log("after pos: " + other.gameObject.transform.position);
    }
}
