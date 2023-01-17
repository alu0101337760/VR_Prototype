using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class HideHands : MonoBehaviour
{
    public Collider handCollider;
    public SkinnedMeshRenderer handRenderer; 
    public void Grab(SelectEnterEventArgs args)
    {
        handCollider.enabled = false;
        handRenderer.enabled = false;
    }
    public void Release(SelectExitEventArgs args)
    {
        handCollider.enabled = true;
        handRenderer.enabled = true;
    }
}
