using System;
using System.Collections;
using System.Collections.Generic;
using Swarming;
using UnityEngine;

public class FlockGroupBehaviour : MonoBehaviour
{
    public List<FlockingEntityBehaviour> flockMembers;
    
    public bool UsesSphereBoundary = true;
    public ScriptableFloat RadiusBoundary;
    
    public ScriptableFloat RectBoundaryX;
    public ScriptableFloat RectBoundaryY;
    public ScriptableFloat RectBoundaryZ;
    
    
    
    void Awake()
    {
        flockMembers = new List<FlockingEntityBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToFlock(FlockingEntityBehaviour entity)
    {
        flockMembers.Add(entity);
    }
    
    public void RemoveFromFlock(FlockingEntityBehaviour entity)
    {
        flockMembers.Remove(entity);
    }


    private void OnDrawGizmosSelected()
    {
        if (!UsesSphereBoundary) return;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, RadiusBoundary.Value);
    }
}
