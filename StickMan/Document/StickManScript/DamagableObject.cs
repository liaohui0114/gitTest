using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamagableObject : MonoBehaviour
{
    [SerializeField]
    private float _blowMultiplier = 1f;
    [SerializeField]
    private int _damage = 1;
    private Game _game;

    private void OnCollisionEnter(Collision collision)
    {
        StickmanPart component = collision.gameObject.GetComponent<StickmanPart>();
        if ((component != null) && (component.Stickman.CurrentHealth > 0))
        {
            Vector3 point = collision.contacts[0].point;
            if (this._blowMultiplier > 0f)
            {
                Vector3 vector2 = component.rigidbody.position - point;
                component.StartForce((Vector2) ((vector2.normalized * this._game.BlowForce) * this._blowMultiplier), point, this._game.BlowTime);
            }
            this._game.CreateHitParticles(collision.contacts[0].point, false);
            component.Stickman.Hit(this._damage);
            component.Blink();
            if (component.Stickman.CurrentHealth <= 0)
            {
                this._game.PlayDeathSound();
            }
            this._game.PlaySwordSound();
            if (component.name.Contains("Head"))
            {
                component.Stickman.ReleaseWeapons();
            }
            if (!this._game.IsOver)
            {
                this._game.SlowMo(component.Stickman.CurrentHealth <= 0);
            }
        }
    }

    private void Start()
    {
        this._game = UnityEngine.Object.FindObjectOfType<Game>();
    }
}

