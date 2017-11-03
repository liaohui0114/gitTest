using System;
using UnityEngine;

public class BaseMenu : MonoBehaviour
{
    public virtual void Init()
    {
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(base.transform.position, (Vector3) new Vector2(((Camera.main.orthographicSize * 2f) * 16f) / 9f, Camera.main.orthographicSize * 2f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(base.transform.position, (Vector3) new Vector2(((Camera.main.orthographicSize * 2f) * 4f) / 3f, Camera.main.orthographicSize * 2f));
    }
}

