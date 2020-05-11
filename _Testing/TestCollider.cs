using System;
using UnityEngine;

namespace Bnyx.AI
{
    public class TestCollider : MonoBehaviour
    {
        [SerializeField]
        private float mRadius = 50f;
        
        private void Start()
        {
            var colliders = Physics.OverlapSphere(transform.position, mRadius);
            foreach (var collider in colliders)
            {
                Debug.LogFormat($"collider:{collider.gameObject}");
            }
            
        }
        
        // private Collider[] overlapResults = new Collider[10];
        //
        // private void Update() {
        //     int numFound = Physics.OverlapSphereNonAlloc(transform.position, 10f, overlapResults);
        //
        //     for (int i = 0; i < numFound; i++) {
        //         Debug.DrawLine(transform.position, overlapResults[i].transform.position, Color.red);
        //     }
        // }
        
        

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Start();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, mRadius);
        }
    }
}