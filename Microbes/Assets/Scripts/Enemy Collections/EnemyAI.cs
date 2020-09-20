using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private Enemy thisenem;
    private bool isAttacking = false;
    [SerializeField]private Transform Attacable = null;
    private bool ReachedRandomPosition = false;
    private Vector3 RoamPosition;
    private readonly bool Alive = true;

    private EnemyFOV FOV;

    StateEnum.State state;

    private void Start()
    {
        FOV = GetComponent<EnemyFOV>();
        player = Player.singleton.transform;
        thisenem = GetComponent<Enemy>();
        StartCoroutine(FSM());
        RoamPosition = new Vector3(UnityEngine.Random.Range(transform.position.x - 5f, transform.position.x + 5f), UnityEngine.Random.Range(transform.position.y - 5f, transform.position.y + 5f), 0);
    }
    private void Update()
    {
        EnemyLogic();
    }
    void Attacking(float delay, Transform _Attacable)
    {
        Debug.Log(Attacable);
        LookAtTarget(_Attacable.position);
        if(thisenem.canShoot)
            StartCoroutine(Attack(delay));
    }
    IEnumerator Attack(float delay)
    {
        thisenem.canShoot = false;
        Shoot(thisenem.projectilePrefab, thisenem.ShootVector, thisenem.Barellpos, thisenem.shotPower, thisenem.ShootDamage);
        yield return new WaitForSeconds(delay);
        thisenem.canShoot = true;
    }
    void LookAtTarget(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    
    public void Shoot(GameObject projectileprefab, Vector3 Direction, Transform Barellpos, float Shotpower, int EntityDamage)
    {
        Projectile pr;
        GameObject projectile = Instantiate(projectileprefab, Barellpos.position, Barellpos.rotation);
        pr = projectile.GetComponent<Projectile>();
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(Direction * Shotpower);
        pr.Damage = EntityDamage;
        Destroy(projectile, 2f);
    }
    //TODO: Rewrite Activity into Finite State Machine 
    void FindAttacableObject()
    {
        //player is also under influence of method, so it is must be attacked tho
        foreach (Transform enemy in FOV.visibleTargets)
        {
            if (name != enemy.name && !isAttacking)
            {
                 Attacable = enemy;
                 break;
            }
        }
        if (!FOV.visibleTargets.Contains(Attacable))
        {
            Attacable = null;
        }
    }
    IEnumerator FSM()
    {
        while (Alive)
        {
            switch (state)
            {
                case StateEnum.State.Attaking:
                    try
                    {
                        Attacking(60 / thisenem.FireRate, Attacable);
                    }
                    catch (Exception)
                    {
                        state = StateEnum.State.Searching;
                    }
                    break;
                case StateEnum.State.Searching:
                    Search();
                    break;
            }
            yield return null;
        }
    }
    bool ConvertFlag(Vector2 RandomPoint) => Vector2.Distance(transform.position, RandomPoint) <= 1f;
    void Search()
    {
        if (ReachedRandomPosition)
        {
            RoamPosition = new Vector2(UnityEngine.Random.Range(transform.position.x - 5f, transform.position.x + 5f), UnityEngine.Random.Range(transform.position.y - 5f, transform.position.y + 5f));
        }
        LookAtTarget(RoamPosition);
        transform.position = Vector2.Lerp(transform.position, RoamPosition, Time.deltaTime);
    }
    void EnemyLogic()
    {
        FindAttacableObject();
        ReachedRandomPosition = ConvertFlag(RoamPosition);
        if (Attacable != null)
        {
            state = StateEnum.State.Attaking;
        }
        if(Attacable == null && !ReachedRandomPosition)
        {
            state = StateEnum.State.Searching;
            Search();
        }
    }
}
class StateEnum
{
    public enum State
    {
        Staying,
        Attaking,
        Searching
    }
}
