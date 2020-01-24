using System.Collections;
using System.Collections.Generic;
using Swarming;
using UnityEngine;

public class FlockGroupBehaviour : MonoBehaviour
{
    public List<FlockingEntityBehaviour> flockMembers;
    // Start is called before the first frame update
    void Start()
    {
        flockMembers = new List<FlockingEntityBehaviour>();
        foreach (var entity in GetComponentsInChildren<FlockingEntityBehaviour>())
        {
            flockMembers.Add(entity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToFlock(FlockingEntityBehaviour entity)
    {
        flockMembers.Add(entity);
    }
    
    void RemoveFromFlock(FlockingEntityBehaviour entity)
    {
        flockMembers.Remove(entity);
    }
    
    // This allows us to modify the Resolution in editor at runtime without putting it in Update()
    
}
