using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnEventCollision(string eid, ArrayList args)
    {
        Vector2 hitPoint = (Vector2)args[0];
        StickmanElement s1 = (StickmanElement)args[1];
        StickmanElement s2 = (StickmanElement)args[2];

        DamageCalculation(s1, s2);
    }



    private void DamageCalculation(StickmanElement s1, StickmanElement s2)
    {
        float damage1 = 0f;
        float damage2 = 0f;
        switch ( s1.Type)
        {
            case StickmanElement.StickmanElementType.BODY:
                damage1 = s2.Attack - s1.Defence * (s2.Type == StickmanElement.StickmanElementType.BODY? 1f: 0f);
                damage1 = Mathf.Max(0f, damage1);
                s1.Hp -= damage1;
                break;
            case StickmanElement.StickmanElementType.EQUIPMENT:
                damage1 = s2.Attack - s1.Defence * (s2.Type == StickmanElement.StickmanElementType.WEAPON ? 0f : 1f);
                damage1 = Mathf.Max(0f, damage1);
                s1.Hp -= damage1;
                break;
            case StickmanElement.StickmanElementType.WEAPON:
                break;
        }


        switch (s2.Type)
        {
            case StickmanElement.StickmanElementType.BODY:
                damage2 = s1.Attack - s2.Defence * (s1.Type == StickmanElement.StickmanElementType.BODY ? 1f : 0f);
                damage2 = Mathf.Max(0f, damage2);
                s2.Hp -= damage2;
                break;
            case StickmanElement.StickmanElementType.EQUIPMENT:
                damage2 = s1.Attack - s2.Defence * (s1.Type == StickmanElement.StickmanElementType.WEAPON ? 0f : 1f);
                damage2 = Mathf.Max(0f, damage2);
                s2.Hp -= damage2;
                break;
            case StickmanElement.StickmanElementType.WEAPON:
                break;
        }

        string msg = string.Format("{0} {1} hurt {2},  {3} {4} hurt {5}", s1.Stickman.gameObject.name, s1.gameObject.name, damage1.ToString(), s2.Stickman.gameObject.name, s2.gameObject.name, damage2.ToString());
        Debugger.Log(msg);


    }


    void OnEnable()
    {
        EventDispatcher.Instance.AddHandler(EventName.EVENT_ON_COLLISION, OnEventCollision);
    }

    void OnDisable()
    {
        EventDispatcher.Instance.RemoveHandler(EventName.EVENT_ON_COLLISION, OnEventCollision);
    }


}
