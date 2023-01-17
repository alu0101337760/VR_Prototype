using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementConsole : MonoBehaviour
{
    public Material activatedMaterial;
    public Material deactivatedMaterial;
    public GameObject locomotionObject;
    public MeshRenderer tPVisuals;
    public MeshRenderer actionBasedVisuals;

    public GameObject leftHandController;
    public GameObject rightHandController;    

    private ActionBasedContinuousMoveProvider actionBasedMoveProvider;
    private TeleportationProvider teleportationProvider;

    private void Start()
    {
        actionBasedMoveProvider = locomotionObject.GetComponent<ActionBasedContinuousMoveProvider>();
        teleportationProvider = locomotionObject.GetComponent<TeleportationProvider>();
    }

    public void ActivateTeleportLocomotion()
    {
        if (actionBasedMoveProvider.enabled)
        {
            actionBasedMoveProvider.enabled = false;
            teleportationProvider.enabled = true;
            tPVisuals.material = activatedMaterial;
            actionBasedVisuals.material = deactivatedMaterial;
        }
    }
    public void ActivateActionBased()
    {
        if (teleportationProvider.enabled)
        {
            actionBasedMoveProvider.enabled = true;
            teleportationProvider.enabled = false;
            tPVisuals.material = deactivatedMaterial;
            actionBasedVisuals.material = activatedMaterial;
        }
    }
}
