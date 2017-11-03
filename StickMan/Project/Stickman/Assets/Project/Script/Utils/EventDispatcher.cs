using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventDispatcher{

    public static EventDispatcher Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EventDispatcher();              
            }
            return _instance;
        }
    }

    private static EventDispatcher _instance;
    private Dictionary<string, List<Action<string,ArrayList>>> _eventHashTable = new Dictionary<string, List<Action<string, ArrayList>>>();

    
    public void AddHandler(string eid, Action<string, ArrayList> handler)
    {
        List<Action<string, ArrayList>> list = null;
        if( _eventHashTable.TryGetValue(eid, out list))
        {
            list.Add(handler);
        }
        else
        {
             list = new List<Action<string, ArrayList>> { handler };
            _eventHashTable.Add(eid, list);
        }
    }

    public void RemoveHandler(string eid, Action<string,ArrayList> handler)
    {
        List<Action<string, ArrayList>> list = null;
        if (_eventHashTable.TryGetValue(eid, out list))
        {
            list.Remove(handler);
            if (list.Count <= 0)
                _eventHashTable.Remove(eid);
        }
     
    }

    public void DispatchImmediately(string eid, ArrayList args = null)
    {
        List<Action<string, ArrayList>> list = null;
        if (_eventHashTable.TryGetValue(eid, out list))
        {
            foreach (var handler in list)
                handler(eid, args);
        }
    }

    public void DispatchOnUpdate(string eid)
    {

    }
}
