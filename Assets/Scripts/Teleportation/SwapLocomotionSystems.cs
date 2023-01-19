using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SwapLocomotionSystems : MonoBehaviour
{

    public GameObject xRRig;
    public CharacterController characterController;
    public CharacterControllerDriver characterControllerDriver;
    private ActionBasedContinuousMoveProvider actionBasedContinuousMoveProvider;
    private TeleportationProvider teleportationProvider;
    // Start is called before the first frame update
    void Start()
    {
        actionBasedContinuousMoveProvider = gameObject.GetComponent<ActionBasedContinuousMoveProvider>();
        teleportationProvider = gameObject.GetComponent<TeleportationProvider>();
        characterController = xRRig.GetComponent<CharacterController>();
        characterControllerDriver = xRRig.GetComponent<CharacterControllerDriver>();
        actionBasedContinuousMoveProvider.enabled = true;        
        characterController.enabled = true;
        characterControllerDriver.enabled = true;
        teleportationProvider.enabled = false;
    }

    public void ActivateTeleportLocomotion() {
        actionBasedContinuousMoveProvider.enabled = false;        
        characterController.enabled = false;
        characterControllerDriver.enabled = false;
        teleportationProvider.enabled = true;
    }

    public void ActivateContinuousLocomotion() {
        actionBasedContinuousMoveProvider.enabled = true;        
        characterController.enabled = true;
        characterControllerDriver.enabled = true;
        teleportationProvider.enabled = false;
    }

}
