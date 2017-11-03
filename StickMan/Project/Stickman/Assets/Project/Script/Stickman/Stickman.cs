using System;
using UnityEngine;
using System.Collections.Generic;


public static class PartName
{
    public static readonly string Head = "Head";
    public static readonly string Spine = "Spine";
    public static readonly string DownArmL = "DownArmL";
    public static readonly string DownArmR = "DownArmR";
    public static readonly string DownLegL = "DownLegL";
    public static readonly string DownLegR = "DownLegR";
    public static readonly string UpArmR = "UpArmR";
    public static readonly string UpArmL = "UpArmL";
    public static readonly string UpLegL = "UpLegL";
    public static readonly string UpLegR = "UpLegR";
}



[ExecuteInEditMode]
public class Stickman : MonoBehaviour,IEquipable,IArmable
{
    [SerializeField]
    private bool _canHoldWeapon = true;
    [SerializeField]
    private Color _color = Color.white;
    //[SerializeField]
    //private int _damage = 10;
    [SerializeField]
    private float _maxVelocity = 15f;
    [SerializeField]
    private float _damp = 20f;
    [SerializeField]
    private float _hp = 100;
    [SerializeField]
    private bool _isPlayer =false;
    [SerializeField]
    private int _movingForce = 200;
    [SerializeField]
    private int _partMass = 5;
    [SerializeField]
    private float _spring = 3500f;
    private StickmanPart _spine;
    private StickmanPart _downArmL;
    private StickmanPart _downArmR;
    private StickmanPart _downLegL;
    private StickmanPart _downLegR;
    private StickmanPart _head;
    private StickmanPart _upArmL;
    private StickmanPart _upArmR;
    private StickmanPart _upLegL;
    private StickmanPart _upLegR;

    private Rigidbody2D _headRigid;
    private CharPose _charPose;

    private GameObject[] _weaponParent = new GameObject[2];
 
    //[SerializeField]
    //private int _difficulty = 10;
    //private GameObject _rightLife;
    //private Mesh _rightLifeMesh;
    //private GameObject _leftLife;
    //private Mesh _leftLifeMesh;
    //[SerializeField]
    //private bool _useRoll = true;
    //private const float ARM_COEFF = 1.25f;
    //private const float HEAD_COEFF = 2.5f;
    //private const float LEG_COEFF = 2f;

    #region Equipable
    public void Dress(Equipment equipment)
    {      
        Transform part = this.transform.FindChild(equipment.Position);
        part.GetComponent<StickmanPart>().Dress(equipment);
    }

    public void Undress(string stickmanPart, bool drop)
    {
        Transform part = this.transform.FindChild(stickmanPart);
        part.GetComponent<StickmanPart>().Undress(drop);
    }
 

    #endregion

    #region IArmable
    public void Arm(Weapon weapon)
    {
        weapon.owner = this;


        weapon.sprite = Instantiate(Resources.Load(weapon.Path, typeof(GameObject))) as GameObject;
        weapon.sprite.tag = "Weapon";
        IgnoreWeaponCollision(weapon.sprite.GetComponent<Collider2D>());
        weapon.sprite.transform.SetParent(this.transform);
        weapon.sprite.transform.localPosition = Vector3.zero;
        weapon.sprite.GetComponent<SpriteRenderer>().sortingOrder = weapon.LayerOrder;

        AttachWeapon(weapon);
    }

    public void Disarm(Weapon weapon)
    {
         
    }
 
    #endregion

    private void IgnoreWeaponCollision(Collider2D weaponCol)
    {
        Physics2D.IgnoreCollision(weaponCol, this._spine.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._head.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._upArmL.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._upArmR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._downArmL.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._downArmR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._upLegL.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._upLegR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._downLegL.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(weaponCol, this._downLegR.gameObject.GetComponent<Collider2D>());

    }

    private void AttachWeapon(Weapon weapon)
    {
        int handnum = 1;
        _weaponParent[0] = _weaponParent[1] = null;
        Vector3 weaponMountPos;
        Vector3 handMountPos;

        switch (weapon.Position)
        {
            case "BH":
                handnum = 2;
                _weaponParent[0] = this._downArmL.gameObject;
                _weaponParent[1] = this._downArmR.gameObject;
                handMountPos = Utils.FindHideChildGameObject(_weaponParent[0], "mount_point").transform.position;
                Vector3 rightHandMountPos = Utils.FindHideChildGameObject(_weaponParent[1], "mount_point").transform.position;
                weapon.sprite.transform.right = (rightHandMountPos - handMountPos).normalized;
                weaponMountPos = Utils.FindHideChildGameObject(weapon.sprite, "mount_point").transform.position;
                weapon.sprite.transform.localPosition += handMountPos - weaponMountPos;
                break;
            case "LH":
                _weaponParent[0] = this._downArmL.gameObject;
                weapon.sprite.transform.right = _weaponParent[0].transform.right;
                weaponMountPos = Utils.FindHideChildGameObject(weapon.sprite, "mount_point").transform.position;
                handMountPos = Utils.FindHideChildGameObject(_weaponParent[0], "mount_point").transform.position;
                weapon.sprite.transform.localPosition += handMountPos - weaponMountPos;
                break;
            case "RH":
                _weaponParent[0] = this._downArmR.gameObject;
                weapon.sprite.transform.right = _weaponParent[0].transform.right;
                weaponMountPos = Utils.FindHideChildGameObject(weapon.sprite, "mount_point").transform.position;
                handMountPos = Utils.FindHideChildGameObject(_weaponParent[0], "mount_point").transform.position;
                weapon.sprite.transform.localPosition += handMountPos - weaponMountPos;
                break;
            default:
                _weaponParent[0] = this._downArmR.gameObject;
                break;
        }

        for(int i=0; i <handnum;i++)
        {
            if(weapon.sprite.GetComponent<Rigidbody2D>() == null)
            {
                weapon.sprite.AddComponent<Rigidbody2D>().gravityScale = GameOptions.gravityScale;
                weapon.sprite.GetComponent<Rigidbody2D>().mass = weapon.Mass;
                weapon.sprite.GetComponent<Rigidbody2D>().angularDrag =1f;
                weapon.sprite.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            }

            HingeJoint2D joint = weapon.sprite.AddComponent<HingeJoint2D>();
            joint.connectedBody = _weaponParent[i].GetComponent<Rigidbody2D>();
            joint.useLimits = true;

            
            joint.anchor = weapon.sprite.transform.InverseTransformPoint(Utils.FindHideChildGameObject(_weaponParent[i], "mount_point").transform.position);   
            JointAngleLimits2D limits2 = new JointAngleLimits2D
            {
                min = -180f,
                max = 180f
            };

            joint.limits = limits2;


            RelativeJoint2D springJoint = weapon.sprite.AddComponent<RelativeJoint2D>();
            springJoint.connectedBody = _weaponParent[i].GetComponent<Rigidbody2D>();
            springJoint.maxForce = 0f;
            springJoint.maxTorque = GameOptions.maxRelativeTorque;

        }
        StickmanWeapon weaponPart =  weapon.sprite.AddComponent<StickmanWeapon>();

        weaponPart.Attack = weapon.Damage;
        weaponPart.Defence = weapon.Armor;
    }


    private void Start()
    {
        this._spine = base.transform.FindChild("Spine").GetComponent<StickmanPart>();
        this._head = base.transform.FindChild("Head").GetComponent<StickmanPart>();
        this._upArmL = base.transform.FindChild("UpArmL").GetComponent<StickmanPart>();
        this._upArmR = base.transform.FindChild("UpArmR").GetComponent<StickmanPart>();
        this._downArmL = base.transform.FindChild("DownArmL").GetComponent<StickmanPart>();
        this._downArmR = base.transform.FindChild("DownArmR").GetComponent<StickmanPart>();
        this._upLegL = base.transform.FindChild("UpLegL").GetComponent<StickmanPart>();
        this._upLegR = base.transform.FindChild("UpLegR").GetComponent<StickmanPart>();
        this._downLegL = base.transform.FindChild("DownLegL").GetComponent<StickmanPart>();
        this._downLegR = base.transform.FindChild("DownLegR").GetComponent<StickmanPart>();


        Physics2D.IgnoreCollision(this._downLegL.gameObject.GetComponent<Collider2D>(), this._downLegR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._upLegL.gameObject.GetComponent<Collider2D>(), this._upLegR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downLegL.gameObject.GetComponent<Collider2D>(), this._upLegR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._upLegL.gameObject.GetComponent<Collider2D>(), this._downLegR.gameObject.GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(this._upArmL.gameObject.GetComponent<Collider2D>(), this._head.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._upArmR.gameObject.GetComponent<Collider2D>(), this._head.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmL.gameObject.GetComponent<Collider2D>(), this._head.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmR.gameObject.GetComponent<Collider2D>(), this._head.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._upArmL.gameObject.GetComponent<Collider2D>(), this._upArmR.gameObject.GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(this._head.gameObject.GetComponent<Collider2D>(), this._spine.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._upArmL.gameObject.GetComponent<Collider2D>(), this._spine.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._upArmR.gameObject.GetComponent<Collider2D>(), this._spine.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmL.gameObject.GetComponent<Collider2D>(), this._spine.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmR.gameObject.GetComponent<Collider2D>(), this._spine.gameObject.GetComponent<Collider2D>());


        Physics2D.IgnoreCollision(this._downArmL.gameObject.GetComponent<Collider2D>(), this._upLegR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmL.gameObject.GetComponent<Collider2D>(), this._upLegL.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmR.gameObject.GetComponent<Collider2D>(), this._upLegR.gameObject.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(this._downArmR.gameObject.GetComponent<Collider2D>(), this._upLegL.gameObject.GetComponent<Collider2D>());
 
        if (Application.isPlaying)
        {
            //this._leftLife = GameObject.CreatePrimitive(PrimitiveType.Quad);
            //UnityEngine.Object.DestroyImmediate(this._leftLife.GetComponent<Collider>());
            //this._leftLife.name = "leftLife";
            //this._leftLife.transform.localScale = (Vector3)(base.transform.lossyScale * this._thickness);
            //this._leftLife.transform.parent = this._downArmL.transform;
            //this._leftLife.transform.localPosition = new Vector3(0f, -this._downArmL.Length * 0.7f, -0.01f);
            //this._leftLifeMesh = this._leftLife.GetComponent<MeshFilter>().mesh;

            //this._rightLife = GameObject.CreatePrimitive(PrimitiveType.Quad);
            //UnityEngine.Object.DestroyImmediate(this._rightLife.GetComponent<Collider>());
            //this._rightLife.name = "rightLife";
            //this._rightLife.transform.localScale = (Vector3)(base.transform.lossyScale * this._thickness);
            //this._rightLife.transform.parent = this._downArmR.transform;
            //this._rightLife.transform.localPosition = new Vector3(0f, -this._downArmR.Length * 0.7f, -0.01f);
            //this._rightLifeMesh = this._rightLife.GetComponent<MeshFilter>().mesh;

            float num =1f;
            this._spring *= num;
            this._damp *= num * 0.9f;
            this.UpdateJoints();
            this.Id = base.GetInstanceID();
        }

    }

    public static Stickman Create(string name, CharPose charpose)
    {
        GameObject obj = new GameObject(name);
        obj.SetActive(false);
        Stickman stickman = obj.AddComponent<Stickman>();
        stickman._charPose = charpose;

        stickman._spine = stickman.CreatePart("Spine", null, charpose.spine_mass, (int)charpose.spine_limit, (int)charpose.spine_angle);
        stickman._head = stickman.CreatePart("Head", stickman._spine, charpose.head_mass, (int)charpose.head_limit, (int)charpose.head_angle);
        stickman._upArmL = stickman.CreatePart("UpArmL", stickman._spine, charpose.upArmL_mass, (int)charpose.upArmL_limit, (int)charpose.upArmL_angle);
        stickman._upArmR = stickman.CreatePart("UpArmR", stickman._spine, charpose.upArmR_mass, (int)charpose.upArmR_limit, (int)charpose.upArmR_angle);
        stickman._downArmR = stickman.CreatePart("DownArmR", stickman._upArmR, charpose.downArmR_mass, (int)charpose.downArmR_limit, (int)charpose.downArmR_angle);
        stickman._downArmL = stickman.CreatePart("DownArmL", stickman._upArmL, charpose.downArmL_mass, (int)charpose.downArmL_limit, (int)charpose.downArmL_angle);
        stickman._upLegL = stickman.CreatePart("UpLegL", stickman._spine, charpose.upLegL_mass, (int)charpose.upLegL_limit, (int)charpose.upLegL_angle);
        stickman._upLegR = stickman.CreatePart("UpLegR", stickman._spine, charpose.upLegR_mass, (int)charpose.upLegR_limit, (int)charpose.upLegR_angle);
        stickman._downLegL = stickman.CreatePart("DownLegL", stickman._upLegL, charpose.downLegL_mass, (int)charpose.downLegL_limit, (int)charpose.downLegL_angle);
        stickman._downLegR = stickman.CreatePart("DownLegR", stickman._upLegR, charpose.downLegR_mass, (int)charpose.downLegR_limit, (int)charpose.downLegR_angle);

        stickman._spine.Stickman = stickman;
        stickman._head.Stickman = stickman;
        stickman._upArmL.Stickman = stickman;
        stickman._upArmR.Stickman = stickman;
        stickman._downArmR.Stickman = stickman;
        stickman._downArmL.Stickman = stickman;
        stickman._upLegL.Stickman = stickman;
        stickman._upLegR.Stickman = stickman;
        stickman._downLegL.Stickman = stickman;
        stickman._downLegR.Stickman = stickman;


        stickman._headRigid = stickman._head.GetComponent<Rigidbody2D>();
        bool flag = true;
        stickman._head.CanHit = flag;
        stickman._head.CanGetHit = flag;
        flag = true;
        stickman._spine.CanGetHit = flag;
        flag = true;
        stickman._downLegR.CanHit = flag;
        stickman._downLegL.CanHit = flag;
        stickman._downArmR.CanHit = flag;
        stickman._downArmL.CanHit = flag;
        flag = true;
        stickman._upLegR.CanGetHit = flag;
        stickman._upLegL.CanGetHit = flag;
        stickman._upArmR.CanGetHit = flag;
        stickman._upArmL.CanGetHit = flag;
        //stickman.UpdateMass();
        stickman.UpdateSize();
        stickman.UpdateJoints();

        obj.SetActive(true);
        return stickman;
    }

    private StickmanPart CreatePart(string partName, StickmanPart parent, float mass, int limits, int angle = 0)
    {
        StickmanPart part = StickmanPart.Create(partName, mass, partName);

        if (parent == null) //spine
        {
            part.transform.parent = base.transform;
            part.transform.localPosition = Vector3.zero;
        }
        else
        {         
            part.transform.parent = parent.transform;
            part.transform.localPosition = new Vector3(0f, -parent.Length);
            HingeJoint2D joint = part.gameObject.AddComponent<HingeJoint2D>();
            
            joint.connectedBody = parent.GetComponent<Rigidbody2D>();
            joint.useLimits = true;

            JointAngleLimits2D limits2 = new JointAngleLimits2D
            {
                min = -limits/2f,
                max =  limits/2f
            };

            joint.limits = limits2;

            RelativeJoint2D springJoint = part.gameObject.AddComponent<RelativeJoint2D>();
            springJoint.connectedBody = parent.GetComponent<Rigidbody2D>();
            springJoint.maxForce = 0f;
            springJoint.maxTorque = GameOptions.maxRelativeTorque;
            //springJoint.frequency = this._spring;
            //springJoint.autoConfigureConnectedAnchor = true;
            //springJoint.dampingRatio = this.Damper;
        }
        part.transform.localEulerAngles = new Vector3(0f, 0f, (float)angle);
        return part;
    }

    public StickmanPose GetPose()
    {
        return new StickmanPose
        {
            Spine1Angle = this._spine.Transform.eulerAngles.z,
            HeadAngle = this._head.Transform.eulerAngles.z,
            UpArmLAngle = this._upArmL.Transform.eulerAngles.z,
            UpArmRAngle = this._upArmR.Transform.eulerAngles.z,
            DownArmLAngle = this._downArmL.Transform.eulerAngles.z,
            DownArmRAngle = this._downArmR.Transform.eulerAngles.z,
            UpLegLAngle = this._upLegL.Transform.eulerAngles.z,
            UpLegRAngle = this._upLegR.Transform.eulerAngles.z,
            DownLegLAngle = this._downLegL.Transform.eulerAngles.z,
            DownLegRAngle = this._downLegR.Transform.eulerAngles.z,
            Spine1Position = this._spine.Transform.position,
            HeadPosition = this._head.Transform.position,
            UpArmLPosition = this._upArmL.Transform.position,
            UpArmRPosition = this._upArmR.Transform.position,
            DownArmLPosition = this._downArmL.Transform.position,
            DownArmRPosition = this._downArmR.Transform.position,
            UpLegLPosition = this._upLegL.Transform.position,
            UpLegRPosition = this._upLegR.Transform.position,
            DownLegLPosition = this._downLegL.Transform.position,
            DownLegRPosition = this._downLegR.Transform.position,
        };

    }

    public void SetPose(StickmanPose poseStart, StickmanPose poseEnd, float time)
    {
        this._spine.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.Spine1Angle, poseEnd.Spine1Angle, time));
        this._head.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.HeadAngle, poseEnd.HeadAngle, time));
        this._upArmL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpArmLAngle, poseEnd.UpArmLAngle, time));
        this._upArmR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpArmRAngle, poseEnd.UpArmRAngle, time));
        this._downArmL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownArmLAngle, poseEnd.DownArmLAngle, time));
        this._downArmR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownArmRAngle, poseEnd.DownArmRAngle, time));
        this._upLegL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpLegLAngle, poseEnd.UpLegLAngle, time));
        this._upLegR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpLegRAngle, poseEnd.UpLegRAngle, time));
        this._downLegL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownLegLAngle, poseEnd.DownLegLAngle, time));
        this._downLegR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownLegRAngle, poseEnd.DownLegRAngle, time));
        this._spine.Transform.position = Vector3.Lerp(poseStart.Spine1Position, poseEnd.Spine1Position, time);
        this._head.Transform.position = Vector3.Lerp(poseStart.HeadPosition, poseEnd.HeadPosition, time);
        this._upArmL.Transform.position = Vector3.Lerp(poseStart.UpArmLPosition, poseEnd.UpArmLPosition, time);
        this._upArmR.Transform.position = Vector3.Lerp(poseStart.UpArmRPosition, poseEnd.UpArmRPosition, time);
        this._downArmL.Transform.position = Vector3.Lerp(poseStart.DownArmLPosition, poseEnd.DownArmLPosition, time);
        this._downArmR.Transform.position = Vector3.Lerp(poseStart.DownArmRPosition, poseEnd.DownArmRPosition, time);
        this._upLegL.Transform.position = Vector3.Lerp(poseStart.UpLegLPosition, poseEnd.UpLegLPosition, time);
        this._upLegR.Transform.position = Vector3.Lerp(poseStart.UpLegRPosition, poseEnd.UpLegRPosition, time);
        this._downLegL.Transform.position = Vector3.Lerp(poseStart.DownLegLPosition, poseEnd.DownLegLPosition, time);
        this._downLegR.Transform.position = Vector3.Lerp(poseStart.DownLegRPosition, poseEnd.DownLegRPosition, time);
    }

    private void Update()
    {
 
    }

    private void FixedUpdate()
    {
        if (this._headRigid.velocity.magnitude > _maxVelocity)
            this._headRigid.velocity = this._headRigid.velocity.normalized * _maxVelocity;
    }

    private void UpdateJoints()
    {
        //for (int i = 0; i < base.transform.childCount; i++)
        //{
        //    HingeJoint component = base.transform.GetChild(i).GetComponent<HingeJoint>();
        //    if (component != null)
        //    {
        //        component.enablePreprocessing = false;
        //        if (component.name.Contains("Spine"))
        //        {
        //            JointSpring spring = new JointSpring
        //            {
        //                damper = this._damp,
        //                spring = this._spring * 1.5f
        //            };
        //            component.spring = spring;
        //        }
        //        else
        //        {
        //            JointSpring spring2 = new JointSpring
        //            {
        //                damper = this._damp,
        //                spring = this._spring
        //            };
        //            component.spring = spring2;
        //        }
        //    }
        //}
    }

    private void UpdateMass()
    {
        this._spine.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._head.GetComponent<Rigidbody2D>().mass = this._partMass * 2;
        this._upArmL.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._upArmR.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._downArmL.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._downArmR.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._upLegL.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._upLegR.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._downLegL.GetComponent<Rigidbody2D>().mass = this._partMass;
        this._downLegR.GetComponent<Rigidbody2D>().mass = this._partMass;
    }

    private void UpdateSize()
    {
        this._spine.Length = this._charPose.unit_length * this._charPose.spine_length;
        this._spine.Thickness =  this._charPose.unit_length*this._charPose.spine_thickness;
        this._spine.transform.parent = null;

        this._head.Length =  this._charPose.unit_length* this._charPose.head_length;
        this._head.Thickness =  this._charPose.unit_length* this._charPose.head_thickness;
        this._head.transform.parent = this._spine.transform;
        this._head.transform.localPosition = new Vector3(0f, this._spine.Length);
        this._head.GetComponent<HingeJoint2D>().anchor = new Vector3(0f, -this._head.Length * 0.33f);
        this._head.transform.localPosition -= new Vector3(this._head.GetComponent<HingeJoint2D>().anchor.x, this._head.GetComponent<HingeJoint2D>().anchor.y, 0f);

        this._upArmL.Length = this._charPose.unit_length * this._charPose.upArmL_length;
        this._upArmL.Thickness =  this._charPose.unit_length* this._charPose.upArmL_thickness;
        this._upArmL.transform.parent = this._spine.transform;
        this._upArmL.transform.localPosition = new Vector3(-this._spine.Thickness * 0.5f, this._spine.Length * 0.7f);

        this._upArmR.Length = this._charPose.unit_length * this._charPose.upArmR_length;
        this._upArmR.Thickness =  this._charPose.unit_length* this._charPose.upArmR_thickness;
        this._upArmR.transform.parent = this._spine.transform;
        this._upArmR.transform.localPosition = new Vector3(this._spine.Thickness * 0.5f, this._spine.Length * 0.7f);

        this._downArmL.Length = this._charPose.unit_length * this._charPose.downArmL_length;
        this._downArmL.Thickness =  this._charPose.unit_length* this._charPose.downArmL_thickness;
        this._downArmL.transform.parent = this._upArmL.transform;
        this._downArmL.transform.localPosition = new Vector3(0f, -this._upArmL.Length);

        this._downArmR.Length = this._charPose.unit_length * this._charPose.downArmR_length;
        this._downArmR.Thickness =  this._charPose.unit_length* this._charPose.downArmR_thickness;
        this._downArmR.transform.parent = this._upArmR.transform;
        this._downArmR.transform.localPosition = new Vector3(0f, -this._upArmR.Length);

        this._upLegL.Length = this._charPose.unit_length * this._charPose.upLegL_length;
        this._upLegL.Thickness =  this._charPose.unit_length* this._charPose.upLegL_thickness;
        this._upLegL.transform.parent = this._spine.transform;
        this._upLegL.transform.localPosition = new Vector3(-this._spine.Thickness * 0.25f, 0f); 

        this._upLegR.Length = this._charPose.unit_length * this._charPose.upLegR_length;
        this._upLegR.Thickness =  this._charPose.unit_length* this._charPose.upLegR_thickness;
        this._upLegR.transform.parent = this._spine.transform;
        this._upLegR.transform.localPosition = new Vector3(this._spine.Thickness * 0.25f, 0f);

        this._downLegL.Length = this._charPose.unit_length * this._charPose.downLegL_length;
        this._downLegL.Thickness =  this._charPose.unit_length* this._charPose.downLegL_thickness;
        this._downLegL.transform.parent = this._upLegL.transform;
        this._downLegL.transform.localPosition = new Vector3(0f, -this._upLegL.Length);

        this._downLegR.Length = this._charPose.unit_length * this._charPose.downLegR_length;
        this._downLegR.Thickness =  this._charPose.unit_length* this._charPose.downLegR_thickness;
        this._downLegR.transform.parent = this._upLegR.transform;
        this._downLegR.transform.localPosition = new Vector3(0f, -this._upLegR.Length);

        this._spine.transform.parent = base.transform;
        this._head.transform.parent = base.transform;
        this._upArmL.transform.parent = base.transform;
        this._upArmR.transform.parent = base.transform;
        this._downArmL.transform.parent = base.transform;
        this._downArmR.transform.parent = base.transform;
        this._upLegL.transform.parent = base.transform;
        this._upLegR.transform.parent = base.transform;
        this._downLegL.transform.parent = base.transform;
        this._downLegR.transform.parent = base.transform;
    }
    

    public void Move(Vector2 direction)
    {
        if (this.Hp > 0 && direction.magnitude > Mathf.Epsilon)
        {
            this._head.Move((Vector3)(direction * this.MovingForce));
        }
    }
 

    //public int Damage
    //{
    //    get
    //    {
    //        return this._damage;
    //    }
    //    set
    //    {
    //        this._damage = value;
    //    }
    //}

    public float Damper
    {
        get {
            return this._damp;
        }
        set
        {
            this._damp = value;
            this.UpdateJoints();
        }
    }

    public Vector2 SpawnPostion
    {
        get
        {
            return (Vector2)this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    public Vector2 Postion
    {
        get
        {
            return (Vector2)this._spine.transform.position;
        }
    }

    public float Hp
    {
        get
        {
            return  this._hp;
        }
        set
        {
            this._hp = value;
        }
    }

    public float UnitLength
    {
        get
        {
            return this._charPose.unit_length;
        }
        set
        {
            this._charPose.unit_length = value;
            this.UpdateSize();
        }
    }

    public int Id { get; private set; }

    public bool IsPlayer
    {
        get
        {
            return this._isPlayer;
        }
        set
        {
            this._isPlayer = value;
        }
    }


    public int MovingForce
    {
        get
        {
            return this._movingForce;
        }
        set
        {
            this._movingForce = value;
        }
    }



    //public bool IsUseRoll
    //{
    //    get
    //    {
    //        return this._useRoll;
    //    }
    //    set
    //    {
    //        this._useRoll = value;
    //    }
    //}

    //public float TimeToWait { get; set; }


    //public int Difficulty
    //{
    //    get
    //    {
    //        return this._difficulty;
    //    }
    //    set
    //    {
    //        this._difficulty = Mathf.Clamp(value, 0, 10);
    //    }
    //}
}