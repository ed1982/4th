using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]

public class JetController : MonoBehaviour
{
    public ThumbTouchController thumb;

    Rigidbody _Rb;
    Rigidbody Rb
    {
        get
        {
            if (_Rb == null)
            {
                _Rb = gameObject.GetComponent<Rigidbody>();
            }
            return _Rb;
        }
    }

    public GameObject Jet_whole;
    public GameObject Jet_parts;

    void OnCollisionEnter (Collision col)
    {
        Jet_whole.SetActive (false);
        Jet_parts.SetActive (true);
        Rb.drag = 100000;
    }


    public float torque;

    [Range (0.0f, 10.0f)] public float Multiplier_Roll = 1.0f;
    [Range (0.0f, 10.0f)] public float Multiplier_Lift = 1.0f;

    [Range (0.0f, 32.0f)] public float Multiplier_Pinch = 1.0f;
    [Range (0.0f, 32.0f)] public float BaseForce_Pinch = 1.0f;

    [Range (0.0f, 1.0f)] public float deadzone = 0.1f;

    public ForceMode forceMode = ForceMode.Impulse;

    void FixedUpdate ()
    {
        bool touching = (thumb.Left_isTouching && thumb.Right_isTouching);

        // Roll
        if (touching)
        {
            if (Mathf.Abs (thumb.Roll) > deadzone)
            {
                Rb.AddTorque (transform.forward * thumb.Roll * - 1.0f * Multiplier_Roll, forceMode);
            }


            // Lift
            if (Mathf.Abs (thumb.Lift) > deadzone)
            {
                Rb.AddTorque (transform.right * thumb.Lift * Multiplier_Lift, forceMode);
            }
        }
        else
        {
            transform.position = Vector3.zero;
        }

        // Go
        if (Mathf.Abs (thumb.Pinch) > deadzone)
        {
            Rb.AddForce (transform.forward * (BaseForce_Pinch + thumb.Pinch * Multiplier_Pinch));
        }
        else
        {
            Rb.AddForce (transform.forward * (BaseForce_Pinch));
        }

//        if ((touching && Mathf.Abs (thumb.Roll) > deadzone)
//          ||(touching && Mathf.Abs (thumb.Lift) > deadzone))
//        {
//            Rb.angularDrag = 0;
//        }
//        else
//        {
//            Rb.angularDrag = 32.0f;
//        }
	}
}
