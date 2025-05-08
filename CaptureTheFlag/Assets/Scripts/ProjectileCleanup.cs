using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCleanup : MonoBehaviour
{
    public int countDown=3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,countDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
