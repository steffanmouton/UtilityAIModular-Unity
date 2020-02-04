using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Swarming
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class FlockingEntityBehaviour : MonoBehaviour
    {
        private Vector3 mVelocity = new Vector3();
        private Rigidbody rb;
        
        [Header("Agent Behaviour Toggles")]
        public bool agentFlocks;
        public bool agentSeeks;
        public bool agentFlees;
        public bool agentArrives;
        public bool agentWanders;
        
        [Header("General Movement Settings")]
        public ScriptableFloat MaxSpeed;
        public ScriptableFloat InitialSpeed;
        
        [Header("Flocking References & Values")]
        public FlockGroupBehaviour flockGroup;
        public ScriptableFloat RadiusSquaredDistance;
        public ScriptableFloat SeparationWeight;
        public ScriptableFloat CohesionWeight;
        public ScriptableFloat AlignmentWeight;
        
        [Header("Arrival Values")]
        public ScriptableFloat ArrivalDistance;
        
        [Header("Wander Values")]
        public ScriptableFloat WanderDisplacerDistance;
        public ScriptableFloat WanderDisplacerRadius;
        public ScriptableFloat WanderInterval;
        
        [Header("Final Behaviour Weighting (1 is 100%)")]
        public ScriptableFloat FleeWeight;
        public ScriptableFloat SeekWeight;
        public ScriptableFloat WanderWeight;
        public ScriptableFloat FlockTotalWeight;
        
        
        
        [Header("Seek & Flee References")]
        public GameObject seekTarget;
        public GameObject fleeTarget;

        
        private Vector3 wanderTarget;
        private float wanderTimer;
        
        private void Start()
        {
            
            
            // Get reference to Rigidbody. Apply a starting force.
            rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * InitialSpeed.Value);
            
            // Randomize starting rotation.
            transform.LookAt(Random.insideUnitSphere);
            
            // Initialize the Wander Timer. Set a wander target in case entity is set to wander.
            wanderTimer = 0;
            wanderTarget = transform.position + transform.forward * WanderDisplacerDistance.Value;
            
            
            // If there is a referenced Flock Group, then add this object to the flock group's list of entities.
            if (!flockGroup)
                return;
            flockGroup.AddToFlock(this);
        }

        private void Update()
        {
            // Calculate all the forces to apply to the agent, then apply them.
            UpdateAgentMovement();
            
            // Sets the boundary for the flock. Comment this out if you want completely free movement.
            if(agentFlocks)
                ReturnToFlock();
            
            // If the object is moving, set its forward to face direction of movement.
            if (rb.velocity != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            
            // Controls the maximum speed of the entity.
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed.Value);
        }

        /// <summary>
        /// Checks to see what behaviours are set to true, then calculates forces from those active behaviours.
        /// Adds forces to the rigidbody, weighted. Each force is already normalized and clamped.
        /// </summary>
        private void UpdateAgentMovement()
        {
            if (agentSeeks)
                rb.AddForce(Seek(seekTarget) * SeekWeight.Value);
            
            if (agentFlees)
                rb.AddForce(Flee(fleeTarget) * FleeWeight.Value);
            
            if (agentWanders)
                rb.AddForce(Wander() * WanderWeight.Value);
            
            if (agentFlocks)
                rb.AddForce(FlockingBehaviour() * FlockTotalWeight.Value);

        }
        
        /// <summary>
        /// Steering behaviour. Calculates and returns a Vec3 for the force needed to seek a target.
        /// </summary>
        /// <param name="t">GameObject that is to be sought.</param>
        /// <returns>Vector3 of force to apply to the Rigidbody.</returns>
        private Vector3 Seek(GameObject t)
        {
            if (!t)
                return Vector3.zero;
            
            Vector3 desiredVel = t.transform.position - transform.position;
            
            desiredVel.Normalize();
            desiredVel *= MaxSpeed.Value;
            
            if (agentArrives)
            {
                float distanceToTarget = Vector3.Magnitude(t.transform.position - transform.position);
                desiredVel *= (distanceToTarget / ArrivalDistance.Value);
            }
            
            Vector3 steering = desiredVel - rb.velocity;

            steering = Vector3.ClampMagnitude(steering, MaxSpeed.Value);

            return steering;
        }
        
        /// <summary>
        /// Steering behaviour. Calculates and returns a Vec3 for the force needed to seek a target.
        /// </summary>
        /// <param name="t">Vector3 position to be sought.</param>
        /// <returns>Vector3 of force to apply to the Rigidbody.</returns>
        private Vector3 Seek(Vector3 t)
        {
            Vector3 desiredVel = t - transform.position;
            
            desiredVel.Normalize();
            desiredVel *= MaxSpeed.Value;
            
            if (agentArrives)
            {
                float distanceToTarget = Vector3.Magnitude(t - transform.position);
                desiredVel *= (distanceToTarget / ArrivalDistance.Value);
            }
            
            Vector3 steering = desiredVel - rb.velocity;

            steering = Vector3.ClampMagnitude(steering, MaxSpeed.Value);
            
            return steering;
        }
        
        /// <summary>
        /// Steering behaviour. Calculates and returns a Vec3 for the force needed to flee a target.
        /// </summary>
        /// <param name="t">Gameobject from which to flee.</param>
        /// <returns>Vector3 of force to apply to the Rigidbody.</returns>
        private Vector3 Flee(GameObject t)
        {
            Vector3 steering = Seek(t);
            return -steering;
        }
        
        /// <summary>
        /// Steering Behaviour. Calculates a random position to Seek ahead of this object.
        /// </summary>
        /// <returns>Vector3 of force to apply to the Rigidbody.</returns>
        private Vector3 Wander()
        {
            wanderTimer += Time.deltaTime;
            
            if (wanderTimer < WanderInterval.Value)
                return Seek(wanderTarget);
            
            wanderTimer = 0;
            
            Vector3 displacerPosition;
            displacerPosition = transform.position + transform.forward * WanderDisplacerDistance.Value;
            
            wanderTarget = displacerPosition + Random.insideUnitSphere * WanderDisplacerRadius.Value;
            
            return Seek(wanderTarget);
        }
        
        /// <summary>
        /// Forces the gameobject to adhere to the boundary defined by its referenced flockgroup.
        /// </summary>
        private void ReturnToFlock()
        {
            if (!flockGroup)
                return;
            
            Vector3 pos = transform.position;
            Vector3 flockPos = flockGroup.transform.position;

            float distance = Vector3.Distance(pos, flockPos);
            Vector3 vecToCenter = flockPos - pos;
            
            // If the entity crosses the boundary set by the flocking group, reverse current velocity
            if (flockGroup.UsesSphereBoundary)
            {
                if (distance > flockGroup.RadiusBoundary.Value)
                {
                    transform.LookAt(flockPos);
                    Vector3 steering = transform.forward * flockGroup.BoundaryReboundForce.Value;
                    //Vector3.ClampMagnitude(steering, rb.velocity.magnitude);
                    rb.AddForce(steering);
                }
            }
            else // if not using SphereBoundary, then using RectBoundary
            {
                if (pos.x >= flockGroup.RectBoundaryX.Value || pos.x <= -flockGroup.RectBoundaryX.Value)
                {
                    mVelocity *= -1;
                }
                if (pos.y >= flockGroup.RectBoundaryY.Value || pos.y <= -flockGroup.RectBoundaryY.Value)
                {
                    mVelocity *= -1;
                }
                if (pos.z >= flockGroup.RectBoundaryZ.Value || pos.z <= -flockGroup.RectBoundaryZ.Value)
                {
                    mVelocity *= -1;
                }
            }
        }
        
        /// <summary>
        /// Steering Behaviour. Views position and velocity of nearby objects in the same flock. Calculates movements
        /// for this gameobject to simulate flocking patterns based on weight values.
        /// </summary>
        /// <returns>Vector3 of force to apply to the Rigidbody.</returns>
        private Vector3 FlockingBehaviour()
        {
            Vector3 cohesion = new Vector3();
            Vector3 separation = new Vector3();
            Vector3 alignment = new Vector3();

            int count = 0;

            for (int i = 0; i < flockGroup.flockMembers.Count; i++)
            {
                if (this.gameObject != flockGroup.flockMembers[i].gameObject)
                {
                    float distance = (transform.position - flockGroup.flockMembers[i].transform.position).sqrMagnitude;

                    if (distance > 0 && distance < RadiusSquaredDistance.Value)
                    {
                        cohesion += flockGroup.flockMembers[i].transform.position;
                        separation += flockGroup.flockMembers[i].transform.position - transform.position;
                        alignment += flockGroup.flockMembers[i].transform.forward;

                        count++;
                    }
                }
            }

            if (count <= 0)
            {
                return Vector3.zero;
            }
            
            // revert vector
            // separation step
            separation /= count;
            separation *= -1;
            
            // forward step
            alignment /= count;
            
            // cohesion step
            cohesion /= count;
            cohesion -= transform.position;
            
            // Add 'em all up to figure out flocking vector
            Vector3 finalFlockingVector = (
                (separation.normalized * SeparationWeight.Value) +
                (cohesion.normalized * CohesionWeight.Value) +
                (alignment.normalized * AlignmentWeight.Value)
            );

            return finalFlockingVector;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            
            // Draws a ray to reflect current velocity.
            if (!rb)
                return;
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}
