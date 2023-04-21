using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_script : MonoBehaviour
{
    [SerializeField] float rot_speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigid_2D = GetComponent<Rigidbody2D>();
        rigid_2D.angularVelocity = rot_speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
