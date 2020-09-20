using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    public override int ShootDamage => 10; 
    public override int damageOverTime => 1;
    public override float Movementspeed => 5f;
    public override float ShootPower => 500f;
    public override float FireRate => 60f;
    public GameObject projectilePrefab;
    public Transform Barellpos;
    [HideInInspector]
    public bool canShoot = true;
    [HideInInspector]
    public Vector3 ShootVector;
    public float shotPower = 500f;
    public int MaxLife = 100;
    public GameObject healthbar;
    public GameObject bar;
    private Quaternion healthBarRotation;


    public static Enemy singletonEnemy { get; private set; }
    public override IEnumerator _damageovertime(int damage, GameObject Enemy, float overtimedelay)
    {
        throw new System.NotImplementedException();
    }
    private void Awake()
    {
        singletonEnemy = this;
    }
    private void Start()
    {
        Life = MaxLife;
        healthBarRotation = healthbar.transform.rotation;
    }
    void Update()
    {
        if (Life <= 0)
            Dead();
        ShootVector = Barellpos.up;

        healthbar.transform.rotation = healthBarRotation;
        healthbar.transform.position = new Vector3(transform.position.x-5f, transform.position.y + 2f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj;
        if (collision.tag == "Bullet")
        {
            proj = collision.gameObject.GetComponent<Projectile>();
            Life -= proj.Damage;
            Destroy(collision.gameObject);

            bar.transform.localScale = new Vector3(Life / 100, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        }
    }
}