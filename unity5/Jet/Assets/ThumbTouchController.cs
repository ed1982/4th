using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThumbTouchController : MonoBehaviour
{
    public Text t;

    public bool Left_isTouching = false;
    public bool Right_isTouching = false;

    public Vector2 ra = new Vector2 ();
    public Vector2 la = new Vector2 ();

    // Distance between touches
    // [-1 .. 1]
    public float Pinch = 0.0f;
    
    // Average y value above zero of both thumbs
    // [-1 .. 1]
    public float Lift = 0.0f;
    
    // Angle of line drawn between two thumbs
    // [-1 .. 1]
    public float Roll = 0.0f;

    Vector2 _LeftTouch_scaled = new Vector2();
    Vector2 LeftTouch_scaled
    {
        set
        {
            _LeftTouch_scaled.x = Mathf.Clamp01 (value.x);
            _LeftTouch_scaled.y = Mathf.Clamp01 (value.y);

            la.x = Mathf.Lerp (-1, 1, Mathf.Clamp01 (value.x * 2.0f));
            la.y = Mathf.Lerp (-1, 1, Mathf.Clamp01 (value.y));
        }
        get
        {
            return _LeftTouch_scaled;
        }
    }


    Vector2 _RightTouch_scaled = new Vector2 ();
    Vector2 RightTouch_scaled
    {
        set
        {
            _RightTouch_scaled.x = Mathf.Clamp01 (value.x);
            _RightTouch_scaled.y = Mathf.Clamp01 (value.y);

            ra.x = Mathf.Lerp (-1, 1, Mathf.Clamp01 ((value.x - 0.5f) * 2.0f));
            ra.y = Mathf.Lerp (-1, 1, Mathf.Clamp01 (value.y));
        }
        get
        {
            return _RightTouch_scaled;
        }
    }

    public enum ControlType
    {
        TOUCH,
        KEYBOARD
    }
    public ControlType controlType = ControlType.TOUCH;
    
    void Update ()
	{
        if (controlType == ControlType.TOUCH)
        {
            Left_isTouching = false;
            Right_isTouching = false;

            foreach (Touch touch in Input.touches)
            {
                Vector2 scaledTouch = new Vector2 ((touch.position.x / Screen.width), (touch.position.y / Screen.height));

                if (scaledTouch.x < 0.5f)
                {
                    LeftTouch_scaled = scaledTouch;
                    Left_isTouching = true;
                }
                else
                {
                    RightTouch_scaled = scaledTouch;
                    Right_isTouching = true;
                }
            }
        }
        else
        {
            Vector2 left = new Vector2 ();
            left.x = 0.25f + 0.2f * Input.GetAxis("HorizontalASDW");
            left.y = 0.50f + 0.4f * Input.GetAxis("VerticalASDW");
            LeftTouch_scaled = left;
            Left_isTouching = true;

            Vector2 right = new Vector2 ();
            right.x = 0.75f + 0.2f * Input.GetAxis("Horizontal");
            right.y = 0.50f + 0.4f * Input.GetAxis("Vertical");
            RightTouch_scaled = right;
            Right_isTouching = true;
        }
        
        if (Left_isTouching || Right_isTouching)
        {
            Pinch = Mathf.Clamp01 (Vector2.Distance (LeftTouch_scaled, RightTouch_scaled));
            Pinch = Mathf.Lerp (-1, 1, Pinch);

            Lift = ((la.y + ra.y)/2.0f);

            Roll = Mathf.Clamp01 (Vector2.Angle (RightTouch_scaled - LeftTouch_scaled, Vector2.up) / 180);
            Roll = Mathf.Lerp (-1, 1, Roll);
        }

        t.text  = "\nL: " + la + (Left_isTouching ? "TOUCHING":"");
        t.text += "\nR: " + ra + (Right_isTouching ? "TOUCHING":"");
        t.text += "\nPINCH: " + Pinch;
        t.text += "\nLIFT: " + Lift;
        t.text += "\nROLL: " + Roll;
    }
}
