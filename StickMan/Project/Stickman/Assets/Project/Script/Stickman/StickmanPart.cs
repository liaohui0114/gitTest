using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("")]
public class StickmanPart : StickmanElement
{
 
    private UnityEngine.Color _baseColor;
    private Mesh _bodyMesh;
    [SerializeField]
    private bool _canGetHit;
    [SerializeField]
    private bool _canHit;
    //private GameObject _circleEnd;
    //private GameObject _circleStart;
    [SerializeField]
    private UnityEngine.Color _color = UnityEngine.Color.white;
    private Mesh _endMesh;
    private bool _canControlled = true;
    [SerializeField]
    private float _length = 1f;
    private GameObject _body;
    private float _baseMass;
    private Mesh _startMesh;
    [SerializeField]
    private float _thickness = 1f;
    private float _time;
    [SerializeField]
    private string _partName;
    [SerializeField]
    private Stack<Equipment> EquipmentStack = new Stack<Equipment>();


    public override float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            //if (_inSuperArmer)
            //    return;

                if(this.Type == StickmanElementType.BODY)
                {
                    float hpDiff = value - hp;
                    Stickman.Hp += hpDiff;

                    if (hpDiff < 0)
                    {
                        HPBurstManager.Instance.ShowHPBurst(this.transform.position, (int)Math.Abs(hpDiff), !this.Stickman.IsPlayer);
                        //StartCoroutine(SuperArmerCoroutine());
                    }
                    hp = value;
                }
                else if(this.Type == StickmanElementType.EQUIPMENT)
                {
                    if (value < 0)
                    {
                        Undress(true);
                    }
                    else
                    {
                        if (value - hp < 0)
                        {
                            HPBurstManager.Instance.ShowHPBurst(this.transform.position, (int)Math.Abs(value - hp), !this.Stickman.IsPlayer);                            
                            //StartCoroutine(SuperArmerCoroutine());
                        }
                            
                        hp = value;
                    }
                        
                }
 
        }
    }




    public void Dress(Equipment equipment)
    {
        EquipmentStack.Push(equipment);

        Transform body = this.transform.FindChild("body");
        equipment.sprite = Instantiate(Resources.Load(equipment.Path, typeof(GameObject))) as GameObject;
        equipment.sprite.transform.SetParent(body);
        equipment.sprite.transform.localPosition = Vector3.zero;
        equipment.sprite.transform.localRotation = Quaternion.identity;

        equipment.sprite.transform.SetParent(this.transform);
        equipment.sprite.GetComponent<SpriteRenderer>().sortingOrder = equipment.LayerOrder;
        equipment.sprite.GetComponent<PolygonCollider2D>().enabled = false;
        UpdateEquipment();
    }

    public void Undress(bool drop = false)
    {
        if (EquipmentStack.Count > 1)
        {
            Equipment equipment = EquipmentStack.Pop();

            if(drop)
            {
                equipment.sprite.transform.SetParent(null);
                equipment.sprite.transform.position += Vector3.forward;
                Rigidbody2D rigidbody = equipment.sprite.AddComponent<Rigidbody2D>();
                rigidbody.mass = equipment.Mass;
                rigidbody.drag = 0.0f;
                rigidbody.gravityScale = GameOptions.gravityScale;
                PhysicsMaterial2D phyMat2D = new PhysicsMaterial2D("physicMat");
                phyMat2D.bounciness = GameOptions.bounciness;
                rigidbody.sharedMaterial = phyMat2D;
                rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
                rigidbody.AddForce(_rigidbody.velocity.normalized * rigidbody.mass *100f);
                equipment.sprite.GetComponent<PolygonCollider2D>().enabled = true;
            }
            else
            {
                Destroy(equipment.sprite);
            }
        }

        UpdateEquipment();
    }

    private void UpdateEquipment()
    {
        if(EquipmentStack.Count >0)
        {
            float totalMass = 0f;
            foreach (Equipment e in EquipmentStack)
                totalMass += e.Mass;
            this.GetComponent<Rigidbody2D>().mass = _baseMass  + totalMass;
            this.GetComponent<PolygonCollider2D>().points = EquipmentStack.Peek().sprite.GetComponent<PolygonCollider2D>().points;

            this.Hp = EquipmentStack.Peek().Hp;
            this.Attack = EquipmentStack.Peek().Damage;
            this.Defence = EquipmentStack.Peek().Armor;
            this.Type = EquipmentStack.Peek().isBody > 0 ? StickmanElementType.BODY : StickmanElementType.EQUIPMENT;
        }           
    }


    protected new void Start()
    {
        base.Start();
        
        this._body = base.transform.FindChild("body").gameObject;
        this.Transform = base.transform;
    }

    public void Blink()
    {
        this._time = Time.time;
        base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, -0.1f);
    }

    public static StickmanPart Create(string name = "Part", float mass = 1, string partName="")
    {
        GameObject obj = new GameObject(name);
        obj.SetActive(false);
        obj.tag = "Player";
        StickmanPart part = obj.AddComponent<StickmanPart>();

        part._partName = partName;
        part._rigidbody = part.gameObject.AddComponent<Rigidbody2D>();
        part._rigidbody.mass = mass;
        part._baseMass = mass;
        part._rigidbody.angularDrag = 1f;
        part._rigidbody.drag = 0.0f;
        part._rigidbody.gravityScale = GameOptions.gravityScale;
        PhysicsMaterial2D phyMat2D = new PhysicsMaterial2D("physicMat");
        phyMat2D.bounciness = GameOptions.bounciness;
        part._rigidbody.sharedMaterial = phyMat2D;
        part._rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate; 
        part._body = new GameObject("body");
        part._body.AddComponent<SphereCollider>().enabled = false;//标记位置用
        part._body.transform.parent = part.transform;
        part.UpdatePart();
        obj.SetActive(true);
        return part;
    }

    

    private void OnCollisionEnter2D_Legacy(Collision2D collision)
    {
        foreach (ContactPoint2D hit in collision.contacts)
        {
            Vector2 hitPoint = hit.point;


           
        }

        StickmanPart component = collision.gameObject.GetComponent<StickmanPart>();
        if (((((component != null) && (component.Stickman != this.Stickman)) && ((this.Stickman.Hp > 0) && (component.Stickman.Hp > 0))) && (this.Stickman.IsPlayer != component.Stickman.IsPlayer)) && (this._rigidbody.velocity.sqrMagnitude >= component._rigidbody.velocity.sqrMagnitude))
        {
            Vector3 point = collision.contacts[0].point;
            if (this.CanHit || component.CanHit)
            {
                //Vector3 vector4 = this._rigidbody.position - point;
                //this.StartForce((Vector2)(vector4.normalized * this._game.BlowForce), point, this._game.BlowTime);
                //Vector3 vector5 = component._rigidbody.position - point;
                //component.StartForce((Vector2)(vector5.normalized * this._game.BlowForce), point, this._game.BlowTime);
            }
            bool twice = false;
            if (this.CanHit && component.CanGetHit)
            {
                //this._game.CreateHitParticles(point, true);
                //component.Stickman.Hit(this.Stickman.Damage);
                component.Blink();
                if (component.Stickman.Hp <= 0)
                {
                    //this._game.PlayDeathSound();
                    twice = true;
                }
                else
                {
                    //this._game.PlayHitSound(false);
                }
                string str = string.Empty;
                if (base.name.Contains("Head"))
                {
                    str = "Butt";
                }
                else if (base.name.Contains("Arm"))
                {
                    str = "Punch";
                }
                else if (base.name.Contains("Leg"))
                {
                    str = "Kick";
                }
                string str2 = string.Empty;
                if (component.name.Contains("Head"))
                {
                    str2 = "Head";
                }
                else if (component.name.Contains("Arm"))
                {
                    str2 = "Arm";
                }
                else if (component.name.Contains("Leg"))
                {
                    str2 = "Leg";
                }
                else if (component.name.Contains("Spine"))
                {
                   str2 = "Body";
                }
                if (base.name.Contains("Head") && component.name.Contains("Head"))
                {
                    str2 = "Head";
                    str = "Bounce";
                }
                //this._game.CreateText(str2 + " " + str, UnityEngine.Color.red, point, 0, 0f, -1f, 0f, 0f);
                if (str2 == "Head")
                {
                    //component.Stickman.ReleaseWeapons();
                }
            }
            if (this.CanGetHit && component.CanHit)
            {
                //this._game.CreateHitParticles(point, true);
                //this.Stickman.Hit(component.Stickman.Damage);
                this.Blink();
                if (this.Stickman.Hp <= 0)
                {
                    //this._game.PlayDeathSound();
                    twice = true;
                }
                else
                {
                    //this._game.PlayHitSound(false);
                }
                string str3 = string.Empty;
                if (component.name.Contains("Head"))
                {
                    str3 = "Butt";
                }
                else if (component.name.Contains("Arm"))
                {
                    str3 = "Punch";
                }
                else if (component.name.Contains("Leg"))
                {
                    str3 = "Kick";
                }
                string str4 = string.Empty;
                if (base.name.Contains("Head"))
                {
                    str4 = "Head";
                }
                else if (base.name.Contains("Arm"))
                {
                    str4 = "Arm";
                }
                else if (base.name.Contains("Leg"))
                {
                    str4 = "Leg";
                }
                else if (base.name.Contains("Spine"))
                {
                    str4 = "Body";
                }
                if (base.name.Contains("Head") && component.name.Contains("Head"))
                {
                    str4 = "Head";
                    str3 = "Bounce";
                }
                //this._game.CreateText(str4 + " " + str3, UnityEngine.Color.red, point, 0, 0f, -1f, 0f, 0f);
                if (str4 == "Head")
                {
                    //this.Stickman.ReleaseWeapons();
                }
            }
            if ((this.CanHit && !this.CanGetHit) && (component.CanHit && !component.CanGetHit))
            {
                //this._game.CreateBlockParticles(point);
                //this._game.PlayBlockSound();
            }
            if (this.CanHit || component.CanHit)
            {
                //this._game.SlowMo(twice);
                //this._game.AddStickmanKey(this.Stickman);
                //this._game.AddStickmanKey(component.Stickman);
            }
        }
    }


    public void Move(Vector2 force)
    {
        if (_canControlled)
            this._rigidbody.AddForce(force, ForceMode2D.Force);
    }

    private void Update()
    {
        if (this._time != 0f)
        {
            float t = (Time.time - this._time) / 0.5f;
            if (t > 1f)
            {
                this._time = 0f;
                base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
            }
            else
            {
                //this.Color = UnityEngine.Color.Lerp(UnityEngine.Color.red, this._baseColor, t);
            }
        }
    }

    private void UpdatePart()
    {
        if (this._partName =="Head")
        {
            this._body.transform.parent = base.transform;
            this._body.transform.localPosition = new Vector3(0f, 0f);

            PolygonCollider2D component = this.gameObject.GetComponent<PolygonCollider2D>();
            if (component == null)
                component = this.gameObject.AddComponent<PolygonCollider2D>();           
            component.offset = new Vector2(0f, 0f);
        }
        else if(this._partName == "Spine")
        {
            this._body.transform.parent = base.transform;           
            this._body.transform.localPosition = new Vector3(0f, this._length * 0.5f);      

            PolygonCollider2D component = this.gameObject.GetComponent<PolygonCollider2D>();
            if (component == null)
                component = this.gameObject.AddComponent<PolygonCollider2D>();

            component.offset = new Vector2(0f, this._length * 0.5f);
        }
        else
        {
            this._body.transform.parent = base.transform;
            this._body.transform.localPosition = new Vector3(0f, -this._length * 0.5f);

            PolygonCollider2D component = this.gameObject.GetComponent<PolygonCollider2D>();
            if (component == null)
                component = this.gameObject.AddComponent<PolygonCollider2D>();

            component.offset = new Vector2(0f, -this._length * 0.5f);
        }
    }



    public bool CanGetHit
    {
        get
        {
            return this._canGetHit;
        }
        set
        {
            this._canGetHit = value;
        }
    }

    public bool CanHit
    {
        get
        {
            return this._canHit;
        }
        set
        {
            this._canHit = value;
        }
    }
 
    public float Length
    {
        get
        {
            return this._length;
        }

        set
        {
            this._length = Mathf.Clamp(value, 0f, float.MaxValue);
            this.UpdatePart();
        }
    }

    public float Thickness
    {
        get
        {
            return this._thickness;
        }

        set
        {
            this._thickness = Mathf.Clamp(value, 0f, float.MaxValue);
            this.UpdatePart();
        }
    }

    public UnityEngine.Transform Transform { get; private set; }


 
}
 