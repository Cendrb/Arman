using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Tasker
    {
        EntityGComponent entity;
        SortedList<int, AITask> tasks = new SortedList<int, AITask>();
        Stack<AITask> pendingTasks;
        public Tasker(EntityGComponent entity)
        {
            this.entity = entity;
        }
        public void AddAITask(int priority, AITask task)
        {
            tasks.Add(priority, task);
        }
        public void Update(GameTime time)
        {
            if (!entity.Navigator.Busy && tasks.Count != 0)
            {
                pendingTasks = new Stack<AITask>(from task in tasks
                                                 where task.Value.Activate()
                                                 select task.Value);
                while (!entity.Navigator.Busy && pendingTasks.Count != 0)
                {
                    AITask task = pendingTasks.Pop();
                    task.Execute();
                }
            }
        }
    }
}
