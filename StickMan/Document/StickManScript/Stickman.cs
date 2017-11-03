using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[ExecuteInEditMode]
public class Stickman : MonoBehaviour
{
    [SerializeField]
    private bool _canHoldWeapon = true;
    [SerializeField]
    private UnityEngine.Color _color = UnityEngine.Color.white;
    [SerializeField]
    private int _damage = 10;
    [SerializeField]
    private float _damp = 20f;
    [SerializeField]
    private int _difficulty = 10;
    private StickmanPart _downArmL;
    private StickmanPart _downArmR;
    private StickmanPart _downLegL;
    private StickmanPart _downLegR;
    private StickmanPart _head;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private float _height = 15f;
    [SerializeField]
    private bool _isPlayer;
    private GameObject _leftLife;
    private Mesh _leftLifeMesh;
    [SerializeField]
    private int _money = 10;
    [SerializeField]
    private int _movingForce = 0xbb8;
    [SerializeField]
    private int _partMass = 5;
    private GameObject _rightLife;
    private Mesh _rightLifeMesh;
    private StickmanPart _spine1;
    private StickmanPart _spine2;
    private StickmanPart _spine3;
    [SerializeField]
    private float _spring = 3500f;
    [SerializeField]
    private float _thickness = 1.25f;
    private StickmanPart _upArmL;
    private StickmanPart _upArmR;
    private StickmanPart _upLegL;
    private StickmanPart _upLegR;
    [SerializeField]
    private bool _useRoll = true;
    private const float ARM_COEFF = 1.25f;
    private const float HEAD_COEFF = 2.5f;
    private const float LEG_COEFF = 2f;

    private void Awake()
    {
        this._spine3 = base.transform.FindChild("Spine3").GetComponent<StickmanPart>();
        this._spine2 = base.transform.FindChild("Spine2").GetComponent<StickmanPart>();
        this._spine1 = base.transform.FindChild("Spine1").GetComponent<StickmanPart>();
        this._head = base.transform.FindChild("Head").GetComponent<StickmanPart>();
        this._upArmL = base.transform.FindChild("UpArmL").GetComponent<StickmanPart>();
        this._upArmR = base.transform.FindChild("UpArmR").GetComponent<StickmanPart>();
        this._downArmL = base.transform.FindChild("DownArmL").GetComponent<StickmanPart>();
        this._downArmR = base.transform.FindChild("DownArmR").GetComponent<StickmanPart>();
        this._upLegL = base.transform.FindChild("UpLegL").GetComponent<StickmanPart>();
        this._upLegR = base.transform.FindChild("UpLegR").GetComponent<StickmanPart>();
        this._downLegL = base.transform.FindChild("DownLegL").GetComponent<StickmanPart>();
        this._downLegR = base.transform.FindChild("DownLegR").GetComponent<StickmanPart>();
        Physics.IgnoreCollision(this._upLegL.collider, this._upLegR.collider);
        Physics.IgnoreCollision(this._upArmL.collider, this._head.collider);
        Physics.IgnoreCollision(this._upArmR.collider, this._head.collider);
        Physics.IgnoreCollision(this._upArmL.collider, this._spine2.collider);
        Physics.IgnoreCollision(this._upArmR.collider, this._spine2.collider);
        Physics.IgnoreCollision(this._downArmL.collider, this._head.collider);
        Physics.IgnoreCollision(this._downArmR.collider, this._head.collider);
        Physics.IgnoreCollision(this._upArmL.collider, this._upArmR.collider);
        this.CurrentHealth = this.Health;
        if (Application.isPlaying)
        {
            this._leftLife = GameObject.CreatePrimitive(PrimitiveType.Quad);
            UnityEngine.Object.DestroyImmediate(this._leftLife.collider);
            this._leftLife.name = "leftLife";
            this._leftLife.transform.localScale = (Vector3) (base.transform.lossyScale * this._thickness);
            this._leftLife.transform.parent = this._downArmL.transform;
            this._leftLife.transform.localPosition = new Vector3(0f, -this._downArmL.Length * 0.7f, -0.01f);
            this._leftLife.renderer.material = this._spine3.QuadMaterial;
            this._leftLifeMesh = this._leftLife.GetComponent<MeshFilter>().mesh;
            this._leftLifeMesh.colors = new UnityEngine.Color[] { this.Color, this.Color, this.Color, this.Color };

            this._rightLife = GameObject.CreatePrimitive(PrimitiveType.Quad);
            UnityEngine.Object.DestroyImmediate(this._rightLife.collider);
            this._rightLife.name = "rightLife";
            this._rightLife.transform.localScale = (Vector3) (base.transform.lossyScale * this._thickness);
            this._rightLife.transform.parent = this._downArmR.transform;
            this._rightLife.transform.localPosition = new Vector3(0f, -this._downArmR.Length * 0.7f, -0.01f);
            this._rightLife.renderer.material = this._spine3.QuadMaterial;
            this._rightLifeMesh = this._rightLife.GetComponent<MeshFilter>().mesh;
            this._rightLifeMesh.colors = new UnityEngine.Color[] { this.Color, this.Color, this.Color, this.Color };

            this.WeaponLTransform = new GameObject("weaponL").transform;
            this.WeaponLTransform.parent = this._downArmL.transform;
            this.WeaponLTransform.localPosition = new Vector3(0f, -this._downArmL.Length * 0.85f, 0.01f);
            this.WeaponLTransform.localEulerAngles = new Vector3(0f, 180f, -90f);

            this.WeaponRTransform = new GameObject("weaponR").transform;
            this.WeaponRTransform.parent = this._downArmR.transform;
            this.WeaponRTransform.localPosition = new Vector3(0f, -this._downArmR.Length * 0.85f, 0.01f);
            
            float num = ((float) PlayerSettings.Flexibility.Value) * 0.1f;
            this._spring *= num;
            this._damp *= num * 0.5f;
            this.UpdateJoints();
            this.Id = base.GetInstanceID();
        }
    }

    public static Stickman Create(string name = "Stickman")
    {
        Stickman stickman = new GameObject(name).AddComponent<Stickman>();
        float length = stickman._height / 8f;
        stickman._spine3 = stickman.CreatePart("Spine3", null, length, 1f, 5f, 30, true, 180);
        stickman._spine2 = stickman.CreatePart("Spine2", stickman._spine3, length, 1f, 5f, 30, true, 0);
        stickman._spine1 = stickman.CreatePart("Spine1", stickman._spine2, length, 1f, 5f, 30, true, 0);
        stickman._head = stickman.CreatePart("Head", stickman._spine1, 0f, 2.5f, 10f, 0x2d, false, 0);
        stickman._upLegL = stickman.CreatePart("UpLegL", stickman._spine3, length * 2f, 1f, 5f, 0x2d, true, 0x91);
        stickman._upLegR = stickman.CreatePart("UpLegR", stickman._spine3, length * 2f, 1f, 5f, 0x2d, true, -145);
        stickman._downLegL = stickman.CreatePart("DownLegL", stickman._upLegL, length * 2f, 1f, 5f, 0x2d, true, 0);
        stickman._downLegR = stickman.CreatePart("DownLegR", stickman._upLegR, length * 2f, 1f, 5f, 0x2d, true, 0);
        stickman._upArmL = stickman.CreatePart("UpArmL", stickman._spine1, length * 1.25f, 1f, 5f, 180, true, 90);
        stickman._upArmR = stickman.CreatePart("UpArmR", stickman._spine1, length * 1.25f, 1f, 5f, 180, true, -90);
        stickman._downArmR = stickman.CreatePart("DownArmR", stickman._upArmR, length * 1.25f, 1f, 5f, 90, true, 0);
        stickman._downArmL = stickman.CreatePart("DownArmL", stickman._upArmL, length * 1.25f, 1f, 5f, 90, true, 0);
        bool flag = true;
        stickman._head.CanHit = flag;
        stickman._head.CanGetHit = flag;
        flag = true;
        stickman._spine3.CanGetHit = flag;
        stickman._spine2.CanGetHit = flag;
        stickman._spine1.CanGetHit = flag;
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
        stickman.UpdateMass();
        stickman.UpdateSize();
        stickman.UpdateJoints();
        return stickman;
    }

    private StickmanPart CreatePart(string partName, StickmanPart parent, float length, float thickness, float mass, int limits, bool twoSides = true, int angle = 0)
    {
        StickmanPart part = StickmanPart.Create(partName, length, thickness, mass, twoSides);
        if (parent == null)
        {
            part.transform.parent = base.transform;
            part.transform.localPosition = Vector3.zero;
        }
        else
        {
            part.transform.parent = parent.transform;
            part.transform.localPosition = new Vector3(0f, -parent.Length);
            HingeJoint joint = part.gameObject.AddComponent<HingeJoint>();
            joint.connectedBody = parent.rigidbody;
            joint.axis = Vector3.forward;
            joint.useLimits = true;
            JointLimits limits2 = new JointLimits {
                max = limits,
                min = -limits,
                maxBounce = 0.5f,
                minBounce = 0.5f
            };
            joint.limits = limits2;
            joint.useSpring = true;
        }
        part.transform.localEulerAngles = new Vector3(0f, 0f, (float) angle);
        return part;
    }

    public StickmanPose GetPose() => 
        new StickmanPose { 
            Spine3Angle = this._spine3.Transform.eulerAngles.z,
            Spine2Angle = this._spine2.Transform.eulerAngles.z,
            Spine1Angle = this._spine1.Transform.eulerAngles.z,
            HeadAngle = this._head.Transform.eulerAngles.z,
            UpArmLAngle = this._upArmL.Transform.eulerAngles.z,
            UpArmRAngle = this._upArmR.Transform.eulerAngles.z,
            DownArmLAngle = this._downArmL.Transform.eulerAngles.z,
            DownArmRAngle = this._downArmR.Transform.eulerAngles.z,
            UpLegLAngle = this._upLegL.Transform.eulerAngles.z,
            UpLegRAngle = this._upLegR.Transform.eulerAngles.z,
            DownLegLAngle = this._downLegL.Transform.eulerAngles.z,
            DownLegRAngle = this._downLegR.Transform.eulerAngles.z,
            Spine3Position = this._spine3.Transform.position,
            Spine2Position = this._spine2.Transform.position,
            Spine1Position = this._spine1.Transform.position,
            HeadPosition = this._head.Transform.position,
            UpArmLPosition = this._upArmL.Transform.position,
            UpArmRPosition = this._upArmR.Transform.position,
            DownArmLPosition = this._downArmL.Transform.position,
            DownArmRPosition = this._downArmR.Transform.position,
            UpLegLPosition = this._upLegL.Transform.position,
            UpLegRPosition = this._upLegR.Transform.position,
            DownLegLPosition = this._downLegL.Transform.position,
            DownLegRPosition = this._downLegR.Transform.position,
            Spine3Color = this._spine3.Color,
            Spine2Color = this._spine2.Color,
            Spine1Color = this._spine1.Color,
            HeadColor = this._head.Color,
            UpArmLColor = this._upArmL.Color,
            UpArmRColor = this._upArmR.Color,
            DownArmLColor = this._downArmL.Color,
            DownArmRColor = this._downArmR.Color,
            UpLegLColor = this._upLegL.Color,
            UpLegRColor = this._upLegR.Color,
            DownLegLColor = this._downLegL.Color,
            DownLegRColor = this._downLegR.Color
        };

    public void Heal()
    {
        if (this.CurrentHealth > 0)
        {
            this.CurrentHealth = this.Health;
            this._leftLifeMesh.colors = new UnityEngine.Color[] { this.Color, this.Color, this.Color, this.Color };
            this._rightLifeMesh.colors = new UnityEngine.Color[] { this.Color, this.Color, this.Color, this.Color };
        }
    }

    public void Hit(int damage)
    {
        this.CurrentHealth -= damage;
        UnityEngine.Color color = UnityEngine.Color.Lerp(UnityEngine.Color.red, this.Color, ((float) this.CurrentHealth) / ((float) this.Health));
        this._leftLifeMesh.colors = new UnityEngine.Color[] { color, color, color, color };
        this._rightLifeMesh.colors = new UnityEngine.Color[] { color, color, color, color };
        if (this.CurrentHealth <= 0)
        {
            this.ReleaseWeapons();
            Joint component = this._spine3.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._spine2.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._spine1.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._head.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._upArmL.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._upArmR.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._downArmL.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._downArmR.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._upLegL.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._upLegR.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._downLegL.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
            component = this._downLegR.GetComponent<Joint>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component);
            }
        }
    }

    public void Move(Vector2 direction)
    {
        if (this.CurrentHealth > 0)
        {
            Vector2 vector = new Vector2();
            if (direction.x > 0f)
            {
                vector.x = 1f;
            }
            else if (direction.x < 0f)
            {
                vector.x = -1f;
            }
            if (direction.y > 0f)
            {
                vector.y = 1f;
            }
            else if (direction.y < 0f)
            {
                vector.y = -1f;
            }
            this._head.rigidbody.AddForce((Vector3) (vector * this.MovingForce));
        }
    }

    public void PrepareToReplay()
    {
        for (int i = 0; i < base.transform.childCount; i++)
        {
            StickmanPart component = base.transform.GetChild(i).GetComponent<StickmanPart>();
            if (component != null)
            {
                UnityEngine.Object.Destroy(component.hingeJoint);
                UnityEngine.Object.Destroy(component.rigidbody);
                UnityEngine.Object.Destroy(component.collider);
            }
        }
    }

    public void ReleaseWeapons()
    {
        if ((this.WeaponL != null) && this.WeaponL.CanBeReleased)
        {
            this.WeaponL.Release();
        }
        if ((this.WeaponR != null) && this.WeaponR.CanBeReleased)
        {
            this.WeaponR.Release();
        }
        BaseWeapon weapon = null;
        this.WeaponR = weapon;
        this.WeaponL = weapon;
    }

    public void SetPose(StickmanPose poseStart, StickmanPose poseEnd, float time)
    {
        this._spine3.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.Spine3Angle, poseEnd.Spine3Angle, time));
        this._spine2.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.Spine2Angle, poseEnd.Spine2Angle, time));
        this._spine1.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.Spine1Angle, poseEnd.Spine1Angle, time));
        this._head.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.HeadAngle, poseEnd.HeadAngle, time));
        this._upArmL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpArmLAngle, poseEnd.UpArmLAngle, time));
        this._upArmR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpArmRAngle, poseEnd.UpArmRAngle, time));
        this._downArmL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownArmLAngle, poseEnd.DownArmLAngle, time));
        this._downArmR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownArmRAngle, poseEnd.DownArmRAngle, time));
        this._upLegL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpLegLAngle, poseEnd.UpLegLAngle, time));
        this._upLegR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.UpLegRAngle, poseEnd.UpLegRAngle, time));
        this._downLegL.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownLegLAngle, poseEnd.DownLegLAngle, time));
        this._downLegR.Transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(poseStart.DownLegRAngle, poseEnd.DownLegRAngle, time));
        this._spine3.Transform.position = (Vector3) Vector2.Lerp(poseStart.Spine3Position, poseEnd.Spine3Position, time);
        this._spine2.Transform.position = Vector3.Lerp(poseStart.Spine2Position, poseEnd.Spine2Position, time);
        this._spine1.Transform.position = Vector3.Lerp(poseStart.Spine1Position, poseEnd.Spine1Position, time);
        this._head.Transform.position = Vector3.Lerp(poseStart.HeadPosition, poseEnd.HeadPosition, time);
        this._upArmL.Transform.position = Vector3.Lerp(poseStart.UpArmLPosition, poseEnd.UpArmLPosition, time);
        this._upArmR.Transform.position = Vector3.Lerp(poseStart.UpArmRPosition, poseEnd.UpArmRPosition, time);
        this._downArmL.Transform.position = Vector3.Lerp(poseStart.DownArmLPosition, poseEnd.DownArmLPosition, time);
        this._downArmR.Transform.position = Vector3.Lerp(poseStart.DownArmRPosition, poseEnd.DownArmRPosition, time);
        this._upLegL.Transform.position = Vector3.Lerp(poseStart.UpLegLPosition, poseEnd.UpLegLPosition, time);
        this._upLegR.Transform.position = Vector3.Lerp(poseStart.UpLegRPosition, poseEnd.UpLegRPosition, time);
        this._downLegL.Transform.position = Vector3.Lerp(poseStart.DownLegLPosition, poseEnd.DownLegLPosition, time);
        this._downLegR.Transform.position = Vector3.Lerp(poseStart.DownLegRPosition, poseEnd.DownLegRPosition, time);
        this._spine3.Color = UnityEngine.Color.Lerp(poseStart.Spine3Color, poseEnd.Spine3Color, time);
        this._spine2.Color = UnityEngine.Color.Lerp(poseStart.Spine2Color, poseEnd.Spine2Color, time);
        this._spine1.Color = UnityEngine.Color.Lerp(poseStart.Spine1Color, poseEnd.Spine1Color, time);
        this._head.Color = UnityEngine.Color.Lerp(poseStart.HeadColor, poseEnd.HeadColor, time);
        this._upArmL.Color = UnityEngine.Color.Lerp(poseStart.UpArmLColor, poseEnd.UpArmLColor, time);
        this._upArmR.Color = UnityEngine.Color.Lerp(poseStart.UpArmRColor, poseEnd.UpArmRColor, time);
        this._downArmL.Color = UnityEngine.Color.Lerp(poseStart.DownArmLColor, poseEnd.DownArmLColor, time);
        this._downArmR.Color = UnityEngine.Color.Lerp(poseStart.DownArmRColor, poseEnd.DownArmRColor, time);
        this._upLegL.Color = UnityEngine.Color.Lerp(poseStart.UpLegLColor, poseEnd.UpLegLColor, time);
        this._upLegR.Color = UnityEngine.Color.Lerp(poseStart.UpLegRColor, poseEnd.UpLegRColor, time);
        this._downLegL.Color = UnityEngine.Color.Lerp(poseStart.DownLegLColor, poseEnd.DownLegLColor, time);
        this._downLegR.Color = UnityEngine.Color.Lerp(poseStart.DownLegRColor, poseEnd.DownLegRColor, time);
    }

    public void SetWeaponL(BaseWeapon weapon)
    {
        if (this.WeaponL == null)
        {
            weapon.SetToStickman(this, this._downArmL);
        }
    }

    public void SetWeaponR(BaseWeapon weapon)
    {
        if (this.WeaponR == null)
        {
            weapon.SetToStickman(this, this._downArmR);
        }
    }

    private void Update()
    {
        if (this.TimeToWait > 0f)
        {
            this.TimeToWait -= Time.deltaTime;
            if (this.TimeToWait < 0f)
            {
                this.TimeToWait = 0f;
            }
        }
    }

    private void UpdateJoints()
    {
        for (int i = 0; i < base.transform.childCount; i++)
        {
            HingeJoint component = base.transform.GetChild(i).GetComponent<HingeJoint>();
            if (component != null)
            {
                if (component.name.Contains("Spine"))
                {
                    JointSpring spring = new JointSpring {
                        damper = this._damp,
                        spring = this._spring * 1.5f
                    };
                    component.spring = spring;
                }
                else
                {
                    JointSpring spring2 = new JointSpring {
                        damper = this._damp,
                        spring = this._spring
                    };
                    component.spring = spring2;
                }
            }
        }
    }

    private void UpdateMass()
    {
        this._spine3.rigidbody.mass = this._partMass;
        this._spine2.rigidbody.mass = this._partMass;
        this._spine1.rigidbody.mass = this._partMass;
        this._head.rigidbody.mass = this._partMass * 2;
        this._upArmL.rigidbody.mass = this._partMass;
        this._upArmR.rigidbody.mass = this._partMass;
        this._downArmL.rigidbody.mass = this._partMass;
        this._downArmR.rigidbody.mass = this._partMass;
        this._upLegL.rigidbody.mass = this._partMass;
        this._upLegR.rigidbody.mass = this._partMass;
        this._downLegL.rigidbody.mass = this._partMass;
        this._downLegR.rigidbody.mass = this._partMass;
    }

    private void UpdateSize()
    {
        float num = this._height / 8f;
        this._spine3.Length = num;
        this._spine3.Thickness = this._thickness;
        this._spine2.Length = num;
        this._spine2.Thickness = this._thickness;
        this._spine2.transform.parent = this._spine3.transform;
        this._spine2.transform.localPosition = new Vector3(0f, -this._spine3.Length);
        this._spine1.Length = num;
        this._spine1.Thickness = this._thickness;
        this._spine1.transform.parent = this._spine2.transform;
        this._spine1.transform.localPosition = new Vector3(0f, -this._spine2.Length);
        this._head.Length = 0f;
        this._head.Thickness = this._thickness * 2.5f;
        this._head.transform.parent = this._spine1.transform;
        this._head.transform.localPosition = new Vector3(0f, -this._spine1.Length);
        this._head.GetComponent<HingeJoint>().anchor = new Vector3(0f, this._head.Thickness * 0.33f, 0f);
        Transform transform = this._head.transform;
        transform.localPosition -= this._head.GetComponent<HingeJoint>().anchor;
        this._upArmL.Length = num * 1.25f;
        this._upArmL.Thickness = this._thickness;
        this._upArmL.transform.parent = this._spine1.transform;
        this._upArmL.transform.localPosition = new Vector3(this._spine1.Thickness * 0.5f, -this._spine1.Length * 0.3f);
        this._upArmR.Length = num * 1.25f;
        this._upArmR.Thickness = this._thickness;
        this._upArmR.transform.parent = this._spine1.transform;
        this._upArmR.transform.localPosition = new Vector3(-this._spine1.Thickness * 0.5f, -this._spine1.Length * 0.3f);
        this._downArmL.Length = num * 1.25f;
        this._downArmL.Thickness = this._thickness;
        this._downArmL.transform.parent = this._upArmL.transform;
        this._downArmL.transform.localPosition = new Vector3(0f, -this._upArmL.Length);
        this._downArmR.Length = num * 1.25f;
        this._downArmR.Thickness = this._thickness;
        this._downArmR.transform.parent = this._upArmR.transform;
        this._downArmR.transform.localPosition = new Vector3(0f, -this._upArmR.Length);
        this._upLegL.Length = num * 2f;
        this._upLegL.Thickness = this._thickness;
        this._upLegL.transform.parent = this._spine3.transform;
        this._upLegL.transform.localPosition = Vector3.zero;
        this._upLegR.Length = num * 2f;
        this._upLegR.Thickness = this._thickness;
        this._upLegR.transform.parent = this._spine3.transform;
        this._upLegR.transform.localPosition = Vector3.zero;
        this._downLegL.Length = num * 2f;
        this._downLegL.Thickness = this._thickness;
        this._downLegL.transform.parent = this._upLegL.transform;
        this._downLegL.transform.localPosition = new Vector3(0f, -this._upLegL.Length);
        this._downLegR.Length = num * 2f;
        this._downLegR.Thickness = this._thickness;
        this._downLegR.transform.parent = this._upLegR.transform;
        this._downLegR.transform.localPosition = new Vector3(0f, -this._upLegR.Length);
        this._spine3.transform.parent = base.transform;
        this._spine2.transform.parent = base.transform;
        this._spine1.transform.parent = base.transform;
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

    public bool CanHoldWeapon
    {
        get => 
            this._canHoldWeapon;
        set
        {
            this._canHoldWeapon = value;
        }
    }

    public UnityEngine.Color Color
    {
        get => 
            this._color;
        set
        {
            this._color = value;
            this._spine3.Color = this._color;
            this._spine2.Color = this._color;
            this._spine1.Color = this._color;
            this._head.Color = this._color;
            this._upArmL.Color = this._color;
            this._upArmR.Color = this._color;
            this._downArmL.Color = this._color;
            this._downArmR.Color = this._color;
            this._upLegL.Color = this._color;
            this._upLegR.Color = this._color;
            this._downLegL.Color = this._color;
            this._downLegR.Color = this._color;
            if (Application.isPlaying)
            {
                this._leftLifeMesh.colors = new UnityEngine.Color[] { this.Color, this.Color, this.Color, this.Color };
                this._rightLifeMesh.colors = new UnityEngine.Color[] { this.Color, this.Color, this.Color, this.Color };
            }
        }
    }

    public int CurrentHealth { get; private set; }

    public int Damage
    {
        get => 
            this._damage;
        set
        {
            this._damage = value;
        }
    }

    public float Damper
    {
        get => 
            this._damp;
        set
        {
            this._damp = value;
            this.UpdateJoints();
        }
    }

    public int Difficulty
    {
        get => 
            this._difficulty;
        set
        {
            this._difficulty = Mathf.Clamp(value, 0, 10);
        }
    }

    public int Health
    {
        get => 
            this._health;
        set
        {
            this._health = value;
            if (this.CurrentHealth > this._height)
            {
                this.CurrentHealth = this._health;
            }
        }
    }

    public float Height
    {
        get => 
            this._height;
        set
        {
            this._height = value;
            this.UpdateSize();
        }
    }

    public int Id { get; private set; }

    public bool IsPlayer
    {
        get => 
            this._isPlayer;
        set
        {
            this._isPlayer = value;
        }
    }

    public bool IsUseRoll
    {
        get => 
            this._useRoll;
        set
        {
            this._useRoll = value;
        }
    }

    public int Money
    {
        get => 
            this._money;
        set
        {
            this._money = value;
        }
    }

    public int MovingForce
    {
        get => 
            this._movingForce;
        set
        {
            this._movingForce = value;
        }
    }

    public int PartMass
    {
        get => 
            this._partMass;
        set
        {
            this._partMass = value;
            this.UpdateMass();
        }
    }

    public Vector2 Position =>
        this._spine3.Transform.position;

    public float Spring
    {
        get => 
            this._spring;
        set
        {
            this._spring = value;
            this.UpdateJoints();
        }
    }

    public float Thickness
    {
        get => 
            this._thickness;
        set
        {
            this._thickness = value;
            this.UpdateSize();
        }
    }

    public float TimeToWait { get; set; }

    public Vector2 Velocity =>
        this._spine3.rigidbody.velocity;

    public BaseWeapon WeaponL { get; set; }

    public Transform WeaponLTransform { get; private set; }

    public BaseWeapon WeaponR { get; set; }

    public Transform WeaponRTransform { get; private set; }
}

