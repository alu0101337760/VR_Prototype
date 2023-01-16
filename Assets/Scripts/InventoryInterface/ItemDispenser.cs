using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemDispenser : MonoBehaviour
{
    public GameObject[] itemInstances = new GameObject[2];

    public int currentInstance = 0;

    public void DispenseItem(int instance)
    {
        if (instance == currentInstance)
        {
            if (!itemInstances[(instance-1)%2].activeSelf)
            {
                ResetInstance((instance-1)%2);
            }
        } else
        {
            HideInstance(currentInstance);
            currentInstance = instance;
        }
    }

    private void ResetInstance(int instance)
    {
        itemInstances[instance].transform.position = transform.position;
        itemInstances[instance].transform.rotation = transform.rotation;
        itemInstances[instance].SetActive(true);
    }

    private void HideInstance(int instance)
    {
        itemInstances[instance].transform.position = transform.position;
        itemInstances[instance].transform.rotation = transform.rotation;
        itemInstances[instance].SetActive(false);
    }
}
