using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu(""), ExecuteInEditMode]
public class vJoystick : MonoBehaviour
{
    private SpriteRenderer _baseSprite;
    [SerializeField]
    private Sprite _baseSpritePrefab;
    [SerializeField]
    private UnityEngine.Camera _camera;
    [SerializeField]
    private float _deadZone = 0.1f;
    private int _fingerId;
    [SerializeField]
    private bool _hideBase;
    private bool _isHold;
    [SerializeField]
    private bool _isStatic = true;
    [SerializeField]
    private float _size = 1f;
    private Vector3 _thumbOriginScale;
    [SerializeField]
    private float _thumbRadius = 1f;
    [SerializeField]
    private float _thumbScaleOnTouch = 1.25f;
    private SpriteRenderer _thumbSprite;
    [SerializeField]
    private Sprite _thumbSpritePrefab;
    [SerializeField]
    private Rect _touchArea;

    public event System.Action OnTouchBegin;

    public event System.Action OnTouchEnd;

    private void Awake()
    {
        Transform transform = base.transform.FindChild("base");
        if (transform != null)
        {
            this._baseSprite = transform.GetComponent<SpriteRenderer>();
        }
        Transform transform2 = base.transform.FindChild("thumb");
        if (transform2 != null)
        {
            this._thumbSprite = transform2.GetComponent<SpriteRenderer>();
            this._thumbOriginScale = this._thumbSprite.transform.localScale;
        }
        this.IsStatic = this._isStatic;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (this._camera != null)
        {
            Gizmos.DrawWireCube(new Vector3((this._camera.transform.position.x + this._touchArea.x) + (this._touchArea.width * 0.5f), (this._camera.transform.position.y + this._touchArea.y) + (this._touchArea.height * 0.5f), base.transform.position.z), new Vector3(this._touchArea.width, this._touchArea.height));
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(base.transform.position, this._thumbRadius * this._size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(base.transform.position, (Vector3) (new Vector3((this._thumbRadius * this._size) * this._deadZone, this._thumbRadius * this._size) * 2f));
        Gizmos.DrawWireCube(base.transform.position, (Vector3) (new Vector3(this._thumbRadius * this._size, (this._thumbRadius * this._size) * this._deadZone) * 2f));
    }

    private void Update()
    {
        if (((this._thumbSprite != null) && (this._baseSprite != null)) && (this._camera != null))
        {
            Rect rect = new Rect(this._touchArea);
            if (rect.width < 0f)
            {
                rect.x += rect.width;
                rect.width *= -1f;
            }
            if (rect.height < 0f)
            {
                rect.y += rect.height;
                rect.height *= -1f;
            }
            rect.x += this._camera.transform.position.x;
            rect.y += this._camera.transform.position.y;
            bool flag = false;
            Vector2 point = new Vector2();
            int fingerId = 0;
            for (int i = 0; i < Input.touchCount; i++)
            {
                fingerId = Input.GetTouch(i).fingerId;
                point = this._camera.ScreenToWorldPoint((Vector3) Input.GetTouch(i).position);
                flag = rect.Contains(point);
                if (flag)
                {
                    break;
                }
            }
            if (flag)
            {
                if (!this._isHold)
                {
                    this._isHold = true;
                    if (!this._isStatic)
                    {
                        base.transform.position = new Vector3(point.x, point.y, base.transform.position.z);
                    }
                    this._baseSprite.gameObject.SetActive(true);
                    this._thumbSprite.gameObject.SetActive(true);
                    Transform transform = this._thumbSprite.transform;
                    transform.localScale = (Vector3) (transform.localScale * this.ThumbTouchScale);
                    this._fingerId = fingerId;
                    if (this.OnTouchBegin != null)
                    {
                        this.OnTouchBegin();
                    }
                }
                else if (this._fingerId != fingerId)
                {
                    this._fingerId = fingerId;
                    if (!this._isStatic)
                    {
                        base.transform.position = new Vector3(point.x, point.y, base.transform.position.z);
                    }
                }
            }
            else if (this._isHold)
            {
                this._isHold = false;
                this._thumbSprite.transform.localPosition = Vector3.zero;
                this._thumbSprite.transform.localScale = this._thumbOriginScale;
                this.Value = Vector2.zero;
                if (!this._isStatic)
                {
                    this._baseSprite.gameObject.SetActive(false);
                    this._thumbSprite.gameObject.SetActive(false);
                }
                else
                {
                    this._baseSprite.gameObject.SetActive(!this._hideBase);
                }
                if (this.OnTouchEnd != null)
                {
                    this.OnTouchEnd();
                }
            }
            if (this._isHold)
            {
                Vector2 position = base.transform.position;
                float z = this._thumbSprite.transform.position.z;
                Vector2 vector3 = position + Vector2.ClampMagnitude(point - position, this._thumbRadius * this._size);
                this._thumbSprite.transform.position = new Vector3(vector3.x, vector3.y, z);
                this.Value = (Vector2) (this._thumbSprite.transform.localPosition / this._thumbRadius);
                float x = (this.Value.x <= 0f) ? Mathf.Lerp(0f, -1f, (-this.Value.x - this._deadZone) / (1f - this._deadZone)) : Mathf.Lerp(0f, 1f, (this.Value.x - this._deadZone) / (1f - this._deadZone));
                float y = (this.Value.y <= 0f) ? Mathf.Lerp(0f, -1f, (-this.Value.y - this._deadZone) / (1f - this._deadZone)) : Mathf.Lerp(0f, 1f, (this.Value.y - this._deadZone) / (1f - this._deadZone));
                this.Value = new Vector2(x, y);
            }
        }
    }

    public Sprite BaseSprite
    {
        get => 
            this._baseSpritePrefab;
        set
        {
            if (this._baseSprite != null)
            {
                if (Application.isPlaying)
                {
                    UnityEngine.Object.Destroy(this._baseSprite.gameObject);
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(this._baseSprite.gameObject);
                }
            }
            System.Type[] components = new System.Type[] { typeof(SpriteRenderer) };
            this._baseSprite = new GameObject("base", components).GetComponent<SpriteRenderer>();
            this._baseSprite.sprite = value;
            this._baseSprite.transform.parent = base.transform;
            this._baseSprite.transform.localPosition = new Vector3(0f, 0f, 0.1f);
            this._baseSprite.gameObject.layer = base.gameObject.layer;
            this._baseSpritePrefab = value;
        }
    }

    public UnityEngine.Camera Camera
    {
        get => 
            this._camera;
        set
        {
            if (value.isOrthoGraphic)
            {
                this._camera = value;
            }
        }
    }

    public float DeadZone
    {
        get => 
            this._deadZone;
        set
        {
            this._deadZone = Mathf.Clamp01(value);
        }
    }

    public bool HideBase
    {
        get => 
            this._hideBase;
        set
        {
            this._hideBase = value;
        }
    }

    public bool IsStatic
    {
        get => 
            this._isStatic;
        set
        {
            this._isStatic = value;
            if (Application.isPlaying)
            {
                if (this._baseSprite != null)
                {
                    this._baseSprite.gameObject.SetActive(this._isStatic);
                }
                if (this._thumbSprite != null)
                {
                    this._thumbSprite.gameObject.SetActive(this._isStatic);
                }
            }
        }
    }

    public bool IsTouched =>
        this._isHold;

    public float Size
    {
        get => 
            this._size;
        set
        {
            this._size = value;
            base.transform.localScale = (Vector3) (Vector3.one * this._size);
        }
    }

    public float ThumbAreaRadius
    {
        get => 
            this._thumbRadius;
        set
        {
            this._thumbRadius = value;
        }
    }

    public Sprite ThumbSprite
    {
        get => 
            this._thumbSpritePrefab;
        set
        {
            if (this._thumbSprite != null)
            {
                if (Application.isPlaying)
                {
                    UnityEngine.Object.Destroy(this._thumbSprite.gameObject);
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(this._thumbSprite.gameObject);
                }
            }
            System.Type[] components = new System.Type[] { typeof(SpriteRenderer) };
            this._thumbSprite = new GameObject("thumb", components).GetComponent<SpriteRenderer>();
            this._thumbSprite.sprite = value;
            this._thumbSprite.transform.parent = base.transform;
            this._thumbSprite.transform.localPosition = Vector3.zero;
            this._thumbSprite.gameObject.layer = base.gameObject.layer;
            this._thumbSpritePrefab = value;
        }
    }

    public float ThumbTouchScale
    {
        get => 
            this._thumbScaleOnTouch;
        set
        {
            this._thumbScaleOnTouch = value;
        }
    }

    public Rect TouchArea
    {
        get => 
            this._touchArea;
        set
        {
            this._touchArea = value;
        }
    }

    public Vector2 Value { get; private set; }
}

