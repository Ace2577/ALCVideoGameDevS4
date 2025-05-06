using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
        public int damage = 1;
        
        void OnColliderEnter(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(other.gameObject.CompareTag("Enemy"));
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
}
