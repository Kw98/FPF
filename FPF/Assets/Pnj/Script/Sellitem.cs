using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sellitem : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject displayDialogue;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            print("achat");
        }
        if (Input.GetKeyDown("x"))
        {
            print("vendre");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            active = !active;
            if (active == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (active == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
            displayDialogue.SetActive(active);
            TriggerDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            active = !active;
            if (active == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (active == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
            displayDialogue.SetActive(active);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }


}
