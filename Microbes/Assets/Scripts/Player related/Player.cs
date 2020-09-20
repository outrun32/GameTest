using System;
using UnityEngine;
using System.Collections;

public class Player : Entity
{
    [Header("Joystick")]
    public Joystick joystick;
    private float moveinputY;
    private float moveinputX;
    private Rigidbody2D rb;
    public override float Movementspeed => 5f;
    public override int ShootDamage => 10;
    public override int damageOverTime => 1;
    [Header("Components needed to shoot")]
    public GameObject projectilePrefab;
    public Transform barellpos;
    private GameObject projectile;
    [HideInInspector]public bool canImpale;
    [HideInInspector]public bool canDamageOverTime = true;
    [HideInInspector] public Vector2 ShootVector;
    
/// <summary>
/// instance of Player class
/// </summary>
    public static Player singleton { get; private set; }
    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Life = 100;
    }

    void FixedUpdate()
    {
        Movement();
        Rotate();
    }
    private void Update()
    {
        if (Life <= 0)
            Dead();
        ShootVector = barellpos.up;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot(projectilePrefab, ShootVector, barellpos, 1000f, ShootDamage);
        }
    }
    /// <summary>
    /// simple movement using axis of joystick
    /// </summary>
    void Movement()
    {
        moveinputY = joystick.Vertical;
        moveinputX = joystick.Horizontal;
        Vector2 moveVector = new Vector2(moveinputX, moveinputY);
        rb.AddForce(moveVector * Movementspeed);
    }
    /// <summary>
    /// roatation of an object(shod've been deal with in movement, but who does even care?)
    /// </summary>
    void Rotate()
    {
        Vector3 moveVector = (Vector3.up * moveinputY - Vector3.left * moveinputX);
        if(moveinputX != 0 || moveinputY != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
    }
    /// <summary>
    /// when the spike is in the enemy, enemy gets gradual damage
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="Enemy"></param>
    /// <param name="overtimedelay"></param>
    /// <returns></returns>
    ///ienumerator not needed yet
    public override IEnumerator _damageovertime(int damage, GameObject _enemy, float overtimedelay)
    {
        canDamageOverTime = false;
        Enemy en;
        en = _enemy.GetComponent<Enemy>();
        en.Life -= damage;
        yield return new WaitForSeconds(overtimedelay);
        canDamageOverTime = true;
    }
   /* void Attack(Collider2D enemyCollider, int impaleDamage)
    {
        Enemy en;
        en = enemyCollider.gameObject.GetComponent<Enemy>();
        en.Life -= impaleDamage;
    }*/
    void Shoot(GameObject projectileprefab, Vector3 Direction, Transform Barellpos, float Shotpower, int EntityDamage)
    {
        Projectile pr;
        Rigidbody2D rb;
        projectile = Instantiate(projectileprefab, Barellpos.position, Barellpos.rotation);
        pr = projectile.GetComponent<Projectile>();
        rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(Direction * Shotpower);
        pr.Damage = EntityDamage;
        Destroy(projectile, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile proj;
        if(collision.tag == "Bullet")
        {
            proj = collision.gameObject.GetComponent<Projectile>();
            Life -= proj.Damage;
            Destroy(collision.gameObject);
        }
    }
    public void UIButtonShoot()
    {
        Shoot(projectilePrefab, ShootVector, barellpos, 1000f, ShootDamage);
    }
}
