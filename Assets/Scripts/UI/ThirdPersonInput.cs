using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonInput : MonoBehaviour
{

    public FixedJoystick LeftJoystick;
    public FixedButton Button;
    public FixedTouchField TouchField;
    public ThirdPersonUserControl Control;

    protected float CameraAngle;
    protected float CameraAngleSpeed = 0.2f;

    // Use this for initialization
    void Start()
    {
        Control = GetComponent<ThirdPersonUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Control.m_Jump = Button.pressed;
        Control.Hinput = LeftJoystick.input.x;
        Control.Vinput = LeftJoystick.input.y;

        CameraAngle += TouchField.TouchDist.x * CameraAngleSpeed;

        Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3(0, 3, 4);
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);

    }
}