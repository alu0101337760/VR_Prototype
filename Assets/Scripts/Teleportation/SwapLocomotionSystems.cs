using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SwapLocomotionSystems : MonoBehaviour
{
    private ActionBasedContinuousMoveProvider actionBasedContinuousMoveProvider;
    private TeleportationProvider teleportationProvider;
    // Start is called before the first frame update
    void Start()
    {
        actionBasedContinuousMoveProvider = gameObject.GetComponent<ActionBasedContinuousMoveProvider>();
        teleportationProvider = gameObject.GetComponent<TeleportationProvider>();
        actionBasedContinuousMoveProvider.enabled = true;
        teleportationProvider.enabled = false;
    }

    public void ActivateTeleportLocomotion() {
        actionBasedContinuousMoveProvider.enabled = false;
        teleportationProvider.enabled = true;
    }

    public void ActivateContinuousLocomotion() {
        actionBasedContinuousMoveProvider.enabled = true;
        teleportationProvider.enabled = false;
    }

}
