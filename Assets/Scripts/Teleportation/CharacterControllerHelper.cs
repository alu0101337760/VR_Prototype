using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


public class CharacterControllerHelper : MonoBehaviour
{
  public XROrigin XROrigin;
  public CharacterController characterController;

  private CharacterControllerDriver driver;

  private void Start()
  {
    XROrigin = GetComponent<XROrigin>();
    characterController = GetComponent<CharacterController>();
    driver = GetComponent<CharacterControllerDriver>();
  }
  protected virtual void UpdateCharacterController()
  {
    if (XROrigin == null || characterController == null)
      return;

    var height = Mathf.Clamp(XROrigin.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

    Vector3 center = XROrigin.CameraInOriginSpacePos;
    center.y = height / 2f + characterController.skinWidth;

    characterController.height = height;
    characterController.center = center;
  }

  // Update is called once per frame
  void Update()
  {
    UpdateCharacterController();
  }
}
