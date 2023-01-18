using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype
{
  public class BinaryConsole : MonoBehaviour
  {

    [Space]
    public Material activatedMaterial;
    public Material deactivatedMaterial;
    [Space]
    public MeshRenderer leftButtonVisuals;
    public MeshRenderer rightButtonVisuals;
    [Space]
    public UnityEvent onLeftButtonPressed;
    public UnityEvent onRightButtonPressed;

    public void ActivateLeftButton()
    {
      leftButtonVisuals.material = activatedMaterial;
      rightButtonVisuals.material = deactivatedMaterial;
      onLeftButtonPressed.Invoke();

    }
    public void ActivateRightButton()
    {
      leftButtonVisuals.material = deactivatedMaterial;
      rightButtonVisuals.material = activatedMaterial;
      onRightButtonPressed.Invoke();
    }
  }
}