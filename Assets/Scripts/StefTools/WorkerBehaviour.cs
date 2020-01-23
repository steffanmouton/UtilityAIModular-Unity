using System;
using UnityEngine;

namespace StefTools
{
    
    public class WorkerBehaviour : MonoBehaviour, IWorker
    {
        public float moveSpeed = 1.0f;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void MoveTo(Vector3 position, Action onArrival)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, position, step);
        }
    }
}
