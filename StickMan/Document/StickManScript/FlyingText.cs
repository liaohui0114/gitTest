using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.TextMesh))]
public class FlyingText : MonoBehaviour
{
    private float _startTime;
    public float FadeDelay = 0.5f;
    public float LifeTime = 1f;
    public float MoveSpeed = 1f;
    public float MoveTime = 0.5f;

    private void Awake()
    {
        this.TextMesh = base.GetComponent<UnityEngine.TextMesh>();
        this._startTime = Time.time;
    }

    private void Start()
    {
        iTween.MoveAdd(base.gameObject, (Vector3) (Vector2.up * this.MoveSpeed), this.MoveTime);
        object[] args = new object[] { "alpha", 0, "delay", this.FadeDelay, "time", this.LifeTime - this.FadeDelay };
        iTween.ColorTo(base.gameObject, iTween.Hash(args));
    }

    private void Update()
    {
        if ((this._startTime > 0f) && ((Time.time - this._startTime) > this.LifeTime))
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }

    public UnityEngine.TextMesh TextMesh { get; private set; }
}

