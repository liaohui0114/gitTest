using UnityEngine;
using System.Collections;

public class MoveStick : JoystickController {

    public GameObject joystickFg;
    public GameObject joystickBg;
 
    public override void init()
    {
        joystickFg.SetActive(false);
        joystickBg.SetActive(false);
        joystickBg.transform.position = basePostion;
        joystickFg.transform.position = basePostion;

    }
    public override void onBeginTouch()
    {

        joystickBg.SetActive(true);
        joystickFg.SetActive(true);
        joystickBg.transform.position = basePostion;
        joystickFg.transform.position = basePostion;

    }

    public override void onMoving()
    {
        joystickFg.transform.position = mousePostion;
    }

    public override void onStationary()
    {
        joystickFg.transform.position = mousePostion;
    }

    public override void onEndTouch()
    {

        joystickFg.SetActive(false);
        joystickBg.SetActive(false);

        joystickFg.transform.position = basePostion;
    }

}
