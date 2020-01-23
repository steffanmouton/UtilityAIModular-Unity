﻿using System.Collections.Generic;
using UnityEngine;

namespace StefTools
{
    public class TaskSystem 
    {
        public class Task
        {
            public Vector3 targetPosition;
        }

        private List<Task> taskList;
        public TaskSystem()
        {
            taskList = new List<Task>();
            
        }

        public Task RequestNextTask()
        {
            // Worker requesting a new task
            if (taskList.Count > 0)
            {
                Task task = taskList[0];
                taskList.RemoveAt(0);
                return task;
            }
            else
            {
                return null;
            }
        }

        public void AddTask(Task task)
        {
            taskList.Add(task);
        }
        
        
    }
}
