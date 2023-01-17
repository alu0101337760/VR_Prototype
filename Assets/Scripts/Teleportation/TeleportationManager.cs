using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace VR_Prototype
{
    public class TeleportationManager : MonoBehaviour
    {
        public GameObject baseControllerObject;
        public GameObject teleportationObject;
        public InputActionReference teleportActivationReference;

        public LocomotionSystem locomotionSystem;
        private TeleportationProvider tp;

        [Space]
        public UnityEvent onTeleportActivate;
        public UnityEvent onTeleportCancel;

        private void Start()
        {
            teleportActivationReference.action.performed += TeleportModeActivate;
            teleportActivationReference.action.canceled += TeleportModeCancel;
            tp = locomotionSystem.GetComponent<TeleportationProvider>();
        }

        private void TeleportModeActivate(InputAction.CallbackContext obj)
        {
            if (tp.isActiveAndEnabled)
            {
                onTeleportActivate.Invoke();
            }
        }

        void DeactivateTeleporter()
        {
            onTeleportCancel.Invoke();
        }

        private void TeleportModeCancel(InputAction.CallbackContext obj)
        {
            Invoke("DeactivateTeleporter", .1f);
        }
    }
}