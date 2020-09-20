using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public virtual int ShootDamage { get; set; }
    /// <summary>
    /// basically shots per minute
    /// </summary>
    public virtual float FireRate { get; set; }
    public virtual int damageOverTime { get; set; }
    public virtual float Movementspeed { get; set; }
    public virtual float ShootPower { get; set; }
    public int Life;
    public void Dead()
    {
        Destroy(this.gameObject);
    }
    public abstract IEnumerator _damageovertime(int damage, GameObject Enemy, float overtimedelay);
}
