using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBabyControlar : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Collider colliderr;
    private Rigidbody rb;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliderr = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void stack()
    {
        rb.isKinematic = false;
        colliderr.enabled = false;
        Vector3 forcpoint = transform.parent.position;
        float ParentXpos = transform.parent.position.x;
        float Xpos = meshRenderer.bounds.center.x;

        Vector3 subdir = (ParentXpos - Xpos <0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up *1.5f + subdir).normalized;

        float force = Random.Range(20, 35);
        float Rota = Random.Range(110, 0180);

        rb.AddForceAtPosition(dir * force,forcpoint,ForceMode.Impulse);
        rb.AddTorque(Vector3.left*Rota);
        rb.velocity = Vector3.down;
        
    }



}
