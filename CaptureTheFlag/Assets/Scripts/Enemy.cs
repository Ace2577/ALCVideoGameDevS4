using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;// add AI namespace
using System.Linq; //add Linq namespace

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int curHP;
    public int maxHP;
    public int scoreToGive;
    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    [Header("Path Info")]
    public float yPathOffset;
    private List<Vector3> path;
    [Header("Weapon")]
    private ProjectileWeapon weapon;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        //gathers components
        weapon=GetComponent<ProjectileWeapon>();
        target=FindObjectOfType<PlayerControllerFPS>().gameObject;
        InvokeRepeating("UpdatePath",0.0f,0.5f);
    }

    void UpdatePath()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath); //calculates a path to player
        path=navMeshPath.corners.ToList(); //saves path to a list
    }

    void ChaseTarget()
    {
        if(path.Count==0)
            return;
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);
        if(transform.position==path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);
    }

    public void TakeDamage(int damageAmount)
    {
      //  curHP -= damage;
        if(curHP<=0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        //look at target
        Vector3 dir = (target.transform.position-transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z)*Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up*angle;
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if(dist <= attackRange)
        {
            if(weapon.CanShoot())
                weapon.Shoot();
        }
        else
        {
            ChaseTarget();
        }
    }
}
