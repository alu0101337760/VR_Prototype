using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class HideHands : MonoBehaviour
{
    public void Grab(SelectEnterEventArgs args)
    {
        this.gameObject.SetActive(false);
    }
    public void Release(SelectExitEventArgs args)
    {
        this.gameObject.SetActive(true);
    }
}
