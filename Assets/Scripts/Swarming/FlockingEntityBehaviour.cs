using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Swarming
{
    public class FlockingEntityBehaviour : MonoBehaviour
    {
        private Vector3 mVelocity = new Vector3();
        private FlockGroupBehaviour flockGroup;

        public ScriptableFloat RadiusSquaredDistance;
        public ScriptableFloat MaxVelocity;
        public ScriptableFloat SeparationWeight;
        public ScriptableFloat CohesionWeight;
        public ScriptableFloat AlignmentWeight;
        
        private void Awake()
        {
            flockGroup = GetComponentInParent<FlockGroupBehaviour>();
        }

        private void Update()
        {
            UpdateAgentMovement();
            
            
        }

        private void UpdateAgentMovement()
        {
            mVelocity += FlockingBehaviour();
            mVelocity = Vector3.ClampMagnitude(mVelocity, MaxVelocity.Value);
            transform.position += mVelocity * Time.deltaTime;
            transform.forward = mVelocity.normalized;
            
            // If you want to make final adjustments, do them here.
            // TODO: Add some Avoidant/fleeing behaviour
        }

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
    }
}
