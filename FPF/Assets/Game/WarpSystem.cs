using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSystem : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
       player.transform.position = tr.position;
    }
}
