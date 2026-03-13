using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float Gravity;
    public float effectionRadius = 10;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;
    void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }
    void Update()
    {
        SetAttractedObjects();
    }
    void FixedUpdate()
    {
        AttractObjects();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }
    void SetAttractedObjects()
    {
        AttractedObjects = Physics2D.OverlapCircleAll(transform.position, effectionRadius, AttractionLayer).ToList();
    }
    void AttractObjects()
    {
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
            AttractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }
}
