using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oak_script : MonoBehaviour
{   
    bool has_acorns;
    [SerializeField] GameObject acorn;

    private void OnCollisionEnter2D(Collision2D other) {
        if (has_acorns && other.gameObject.tag == "Projectile"){
            Instantiate<GameObject>(acorn, new Vector3(0.5f, 1.5f, 0f), transform.rotation);
            has_acorns = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        has_acorns = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
