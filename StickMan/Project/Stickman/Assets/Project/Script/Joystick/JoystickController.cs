using UnityEngine;
using System.Collections;

public class JoystickController : MonoBehaviour {

    public enum platformType
    {
        PC,
        MOBILE
    }

    public enum ScreenLocation
    {
        LEFT,
        RIGHT
    }
 
    public bool fixPosition = false;
    public float AtiShakeThreshold = 0.3f;
    public platformType platform;
    public ScreenLocation location;

    public float stickActivateSize = 20f;
    public float stickFieldSize = 100f;//double max drag radius
    public Vector3 TouchPosition
    {
        get
        {
            if (touchPosition.magnitude > AtiShakeThreshold * stickFieldSize/2f)
                return touchPosition.normalized;
            else return Vector3.zero;
        }
    }

    private bool activated = true;
    private Vector3 touchPosition = Vector3.zero;
    private bool locationValid = true;
    protected Vector3 mousePostion;
    protected Vector3 basePostion;
    private int fingerID = -1;

    // Use this for initialization
    void Start () {

    #if UNITY_EDITOR
            platform = platformType.PC;
    #elif UNITY_ANDROID || UNITY_IPHONE
            platform = platformType.MOBILE;
    #endif

        basePostion = this.transform.position;
        Input.multiTouchEnabled = true;//开启多点触碰
        init();
    }
	
	// Update is called once per frame
	void Update () {

            if (platform == platformType.PC)
                update_PC();
            else if (platform == platformType.MOBILE)
                update_Mobile();

    }


    bool LocationValid(Vector3 touchPosition)
    {
        if ((location == ScreenLocation.LEFT && touchPosition.x > Screen.width / 2f)
        || (location == ScreenLocation.RIGHT && touchPosition.x < Screen.width / 2f))
        {
            return false;
        }
        else if (fixPosition && Vector3.Distance(touchPosition, basePostion) > stickActivateSize / 2f)
        {
            return false;
        }

        return true;
    }

    int GetTouchIndex(int finger_ID)
    {
        for(int i = 0;i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).fingerId == finger_ID)
                return i;
        }
        return -1;
    }


    void update_Mobile()
    {
        int index = -1;
        if (fingerID >= 0)
            index = GetTouchIndex(fingerID);
        else
        {
            for (int i = 0; i < Input.touchCount; i++)
            {

               if( LocationValid(Input.GetTouch(i).position))
                {
                    index = i;
                    fingerID = Input.GetTouch(i).fingerId;
                }                              
            }
        }

        Debug.Log("Input.touchCount  " + Input.touchCount.ToString() + "  index "+ index.ToString() + " fingerid" + fingerID.ToString());

        if (index < 0)
        {
            touchPosition = Vector3.zero; 
            return;
        }

        if (Input.touches[index].phase == TouchPhase.Began)
        {
            mousePostion = Input.touches[index].position;

            if (!fixPosition)
            {
                basePostion = Input.touches[index].position;
            }

            touchPosition = Vector3.zero;
            onBeginTouch();

        }

        if(Input.touches[index].phase == TouchPhase.Moved || Input.touches[index].phase == TouchPhase.Stationary)
        {
            mousePostion = Input.touches[index].position;

            if (Vector3.Distance(mousePostion, basePostion) > stickFieldSize / 2f)
            {
                mousePostion = basePostion + (mousePostion - basePostion).normalized * stickFieldSize / 2f;
            }


            Vector3 velocitytemp = mousePostion - basePostion;

            if (velocitytemp.magnitude > Mathf.Epsilon)
                touchPosition = velocitytemp ;
            else
                touchPosition = Vector3.zero;

            if (Input.touches[index].phase == TouchPhase.Moved)
                onMoving();
            else if (Input.touches[index].phase == TouchPhase.Stationary)
                onStationary();
        
        }

        if (Input.touches[index].phase == TouchPhase.Ended || Input.touches[index].phase == TouchPhase.Canceled)
        {
            //if (!activated)
            //    return;

            activated = false;
            //touchPosition = Vector3.zero;
            fingerID = -1;
            onEndTouch();
        }
    }

    void update_PC()
    {
        if (Input.GetMouseButtonDown(0))
        {

            mousePostion = Input.mousePosition;

            if (!fixPosition)
            {
                basePostion = Input.mousePosition;
                activated = true;
            }
            else
            {
                if (Vector3.Distance(mousePostion, basePostion) > stickActivateSize / 2f)
                {
                    activated = false;
                    return;
                }
                else activated = true;
            }


            if ((location == ScreenLocation.LEFT && basePostion.x > Screen.width / 2f)
                || (location == ScreenLocation.RIGHT && basePostion.x < Screen.width / 2f))
            {
                locationValid = false;
                return;
            }
            else
                locationValid = true;

            touchPosition = Vector3.zero;

            onBeginTouch();

        }

        if (locationValid && Input.GetMouseButton(0))
        {
            if (!activated)
                return;

            mousePostion = Input.mousePosition;

            if (Vector3.Distance(mousePostion, basePostion) > stickFieldSize / 2f)
            {
                mousePostion = basePostion + (mousePostion - basePostion).normalized * stickFieldSize / 2f;
            }


            Vector3 velocitytemp = mousePostion - basePostion;
 
            if (velocitytemp.magnitude > Mathf.Epsilon)
                touchPosition = velocitytemp;
            else
                touchPosition = Vector3.zero;

            onMoving();
    
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!activated)
                return;

            activated = false;
            touchPosition = Vector3.zero;
            onEndTouch();
 
        }
    }

    public virtual void init()
    {

    }

    public virtual void onBeginTouch()
    {

    }

    public virtual void onMoving()
    {

    }

    public virtual void onStationary()
    {

    }

    public virtual void onEndTouch()
    {

    }

}
