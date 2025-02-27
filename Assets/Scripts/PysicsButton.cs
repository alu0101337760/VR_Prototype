using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PysicsButton : MonoBehaviour
{
    [SerializeField]
    private float threshold = .1f;
    [SerializeField]
    private float deadZone = 0.025f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
    }

    private float GetValue()
    {
        float value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        return (Mathf.Abs(value) < deadZone) ? 0 : Mathf.Clamp(value, -1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }
}
