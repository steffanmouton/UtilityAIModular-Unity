using System;
using System.Collections;
using System.Collections.Generic;
using Swarming;
using UnityEngine;

public class FlockGroupBehaviour : MonoBehaviour
{
    // List of entities that belong to this flock group.
    public List<FlockingEntityBehaviour> flockMembers;
    
    // Dictates whether this group uses a spherical boundary and the radius of that boundary.
    public bool UsesSphereBoundary = true;
    public ScriptableFloat RadiusBoundary;
    
    // How much force to apply to an object that has left the boundary.
    public ScriptableFloat BoundaryReboundForce;
    
    // Limits for Rectangular Boundaries.
    // TODO: Clean this, make it work better.
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
    
    /// <summary>
    /// Add a flocking entity to the flockMembers list.
    /// </summary>
    /// <param name="entity">FlockingEntity</param>
    public void AddToFlock(FlockingEntityBehaviour entity)
    {
        flockMembers.Add(entity);
    }
    
    /// <summary>
    /// Remove a flocking entity from the flockMembers list.
    /// </summary>
    /// <param name="entity">FlockingEntity</param>
    public void RemoveFromFlock(FlockingEntityBehaviour entity)
    {
        flockMembers.Remove(entity);
    }


    private void OnDrawGizmosSelected()
    {
        if (!UsesSphereBoundary) return;
        
        // Draw a sphere to show the radial boundary of this flockgroup.
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, RadiusBoundary.Value);
    }
}
