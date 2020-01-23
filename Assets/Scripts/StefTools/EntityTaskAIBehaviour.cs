using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StefTools
{
    public class EntityTaskAIBehaviour : MonoBehaviour
    {
        private enum WorkState
        {
            WaitingForTask,
            WorkingOnTask,
        }

        private WorkState workstate;
        private TaskSystem taskSystem;
        private IWorker worker;
        private float waitingTimer;
        
        public void Setup(IWorker worker, TaskSystem taskSystem)
        {
            this.worker = worker;
            this.taskSystem = taskSystem;
            workstate = WorkState.WaitingForTask;
        }
        

        // Update is called once per frame
        void Update()
        {
            switch (workstate)
            {
                case WorkState.WaitingForTask:
                    // Waiting to request a new task
                    waitingTimer -= Time.deltaTime;
                    if (waitingTimer <= 0)
                    {
                        float waitingTimerMax = .2f;
                        waitingTimer = waitingTimerMax;
                        RequestNextTask();
                    }

                    break;
            }
        }

        private void RequestNextTask()
        {
            Debug.Log("RequestingTaskPls");
            TaskSystem.Task task = taskSystem.RequestNextTask();
            if (task == null)
            {
                workstate = WorkState.WaitingForTask;
            }
            else
            {
                ExecuteTask(task);
            }
        }

        private void ExecuteTask(TaskSystem.Task task)
        {
            Debug.Log("DoingMyTaskNow");
            worker.MoveTo(task.targetPosition);
        }
    }
}
