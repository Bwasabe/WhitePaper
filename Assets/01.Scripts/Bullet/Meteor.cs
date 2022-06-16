using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : BaseBulletMove
{
    [SerializeField]
    private LayerMask _hitLayer;

    [SerializeField]
    private float _radius = 5f;

    [SerializeField]
    private float _force = 10f;

    [SerializeField]
    private float _flyingSize = 1f;

    [SerializeField]
    private GameObject _particle = null;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        Collider[] cols = Physics.OverlapSphere(transform.position, _radius, _hitLayer);

        foreach(Collider c in cols){
           if(c.attachedRigidbody != null) 
                c.attachedRigidbody.AddExplosionForce(_force, transform.position, _radius, _flyingSize, ForceMode.Impulse);
        }
        GameObject g = Instantiate(_particle, transform.position, Quaternion.identity);
        g.SetActive(true);
        Destroy(gameObject);
    }

}
