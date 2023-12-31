using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public int RequiredWeight = 50;
    [SerializeField] List<GameObject> DoorsToOpen;
    [SerializeField] List<GameObject> LasersToTrigger;
    [SerializeField] bool TriggerLasersOn = false;
    bool doorOpen = false;
    int ObjOnSwitch = 0;

    public GameObject objOnButton;
    void Update()
    {
        if (ObjOnSwitch > 0 && objOnButton.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
        {
            if (DoorsToOpen.Count > 0)
            {
                foreach(GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().OpenDoor(); }
            }
            if (LasersToTrigger.Count > 0)
            {
                foreach (GameObject laser in LasersToTrigger) { laser.SetActive(TriggerLasersOn); }
            }
        }           
        else
        {
            if (DoorsToOpen.Count > 0)
            {
                foreach (GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().CloseDoor(); }
            }
            if (LasersToTrigger.Count > 0)
            {
                foreach (GameObject laser in LasersToTrigger) { laser.SetActive(!TriggerLasersOn); }
            }
        }           
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() != null)
        {
            if (ObjOnSwitch > 0)
            {
                ObjOnSwitch--;
            }
        }
        if (other.gameObject.CompareTag("PlayerTrigger"))
        {
            if (ObjOnSwitch > 0)
            {
                ObjOnSwitch--;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ObjOnSwitch == 0)
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                objOnButton = other.gameObject;
                if (other.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
                {
                    ObjOnSwitch++;
                }
            }
            if (other.gameObject.CompareTag("PlayerTrigger"))
            {
                objOnButton = other.transform.parent.gameObject;
                if (other.gameObject.GetComponentInParent<Stats>().Weight > RequiredWeight)
                {
                    ObjOnSwitch++;
                }
            }
        }
    }
}
