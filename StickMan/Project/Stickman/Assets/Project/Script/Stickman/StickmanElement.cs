using UnityEngine;
using System.Collections;

public class StickmanElement : MonoBehaviour {


    public enum StickmanElementType
    {
        WEAPON,
        BODY,
        EQUIPMENT
    }

    protected Rigidbody2D _rigidbody;
    public Stickman Stickman { get; set; }
    public StickmanElementType Type { get; set; }
    public float Attack { get; set; }
    public float Defence { get; set; }

    public virtual float  Hp
    { get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    [SerializeField]
    protected float hp;

    protected bool _inSuperArmer = false;

    // Use this for initialization
    protected void Start () {
 
        if (base.transform.parent != null)
        {
            this.Stickman = base.transform.parent.GetComponent<Stickman>();
        }
        this._rigidbody = base.GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        StickmanElement other = null;
        if (collision.gameObject.CompareTag("Player"))
        {
            other = collision.gameObject.GetComponent<StickmanPart>();
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            other = collision.gameObject.GetComponent<StickmanWeapon>();
        }

        if (!_inSuperArmer && ((((other != null) && (other.Stickman != this.Stickman)))) && (this._rigidbody.velocity.sqrMagnitude > other.Rigidbody.velocity.sqrMagnitude))
        {
            Vector2 hitPoint = collision.contacts[0].point;
            Vector2 vector4 = this._rigidbody.position - hitPoint;
            this.StartForce(1000f * vector4.normalized, hitPoint, 0.2f);
          
            Vector2 vector5 = other.Rigidbody.position - hitPoint;
            other.StartForce(1000f * vector5.normalized, hitPoint, 0.2f);


            ArrayList args = new ArrayList { hitPoint, this, other };
            EventDispatcher.Instance.DispatchImmediately(EventName.EVENT_ON_COLLISION, args);
            StartCoroutine(SuperArmerCoroutine());

        }

    }

    private IEnumerator SuperArmerCoroutine()
    {
        _inSuperArmer = true;
        yield return new WaitForSeconds(0.5f);
        _inSuperArmer = false;
    }


    public void StartForce(Vector2 force, Vector2 hitPoint, float time)
    {
        StopAllCoroutines();
        StartCoroutine(AddForceCoroutine(force, hitPoint, time));
    }

    private IEnumerator AddForceCoroutine(Vector2 force, Vector2 hitPoint, float time)
    {

        float timer = 0f;
        if (GameOptions.useBulletTime)
            Time.timeScale = GameOptions.minTimeScale;
        while (timer < time)
        {
            timer += Time.deltaTime;
            this._rigidbody.AddForceAtPosition(force * (1f - timer / time), hitPoint);
            if (GameOptions.useBulletTime)
                Time.timeScale = (1f - GameOptions.minTimeScale) / time * timer + GameOptions.minTimeScale;
            //Time.timeScale = (4f -4f * GameOptions.minTimeScale) / time / time * timer * timer + ( 4f * GameOptions.minTimeScale -4f) / time * timer + 1f;
            yield return null;
        }
        Time.timeScale = 1f;
    }

    public Rigidbody2D Rigidbody { get { return _rigidbody; } }
 
}
