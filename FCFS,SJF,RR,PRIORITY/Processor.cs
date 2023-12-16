using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FCFS_SJF_RR_PRIORITY
{
    public class ProcessorTask
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Priority { get; set; }
        public string Quantum { get; set; }

        public override string ToString()
        {
            return $"Process:: {Text}";
        }
    }

    public interface IProcessor
    {
        ArrayList Process();
    }

    public enum ProcessorTypes
    {
        FCFS,
        SJF,
        PRIOTITY,
        ROUND_ROBIN
    }

    public class ProcessorBuilder
    {
        public static IProcessor Create(string type, ProcessorTask[] tasks)
        {
            Enum.TryParse(type, true, out ProcessorTypes processorType);

            switch (processorType)
            {
                case ProcessorTypes.FCFS: return new FCFS(tasks);
                case ProcessorTypes.SJF: return new SJF(tasks);
                case ProcessorTypes.PRIOTITY: return new PRIOTITY(tasks);
                case ProcessorTypes.ROUND_ROBIN: return new ROUND_ROBIN(tasks);
                default: throw new IndexOutOfRangeException();
            }
        }
    }

    public class FCFS : IProcessor
    {
        private ProcessorTask[] tasks;

        public FCFS(ProcessorTask[] tasks)
        {
            this.tasks = tasks;
        }

        public ArrayList Process()
        {
            var array = new ArrayList();

            array.AddRange(tasks);

            return array;
        }
    }

    public class SJF : IProcessor
    {
        private ProcessorTask[] tasks;

        public SJF(ProcessorTask[] tasks)
        {
            this.tasks = tasks;
        }

        public ArrayList Process()
        {
            var array = new ArrayList();

            var orderdTasks = tasks.OrderBy(c => c.Value).ToArray();

            array.AddRange(orderdTasks);

            return array;
        }
    }

    public class PRIOTITY : IProcessor
    {
        private ProcessorTask[] tasks;

        public PRIOTITY(ProcessorTask[] tasks)
        {
            this.tasks = tasks;
        }

        public ArrayList Process()
        {
            var array = new ArrayList();

            var orderdTasks = tasks.OrderBy(c => c.Priority).ToArray();

            array.AddRange(orderdTasks);

            return array;
        }
    }

    public class ROUND_ROBIN : IProcessor
    {
        private ProcessorTask[] tasks;

        public ROUND_ROBIN(ProcessorTask[] tasks)
        {
            this.tasks = tasks;
        }

        public ArrayList Process()
        {
            var array = new ArrayList();

            var taskQueue = new Queue<ProcessorTask>(tasks);

            while (taskQueue.Count > 0)
            {
                var currentItem = taskQueue.Dequeue();

                if (int.Parse(currentItem.Value) <= int.Parse(currentItem.Quantum))
                {
                    array.Add(currentItem);
                }
                else
                {
                    taskQueue.Enqueue(new ProcessorTask
                    {
                        Text = currentItem.Text,
                        Value = (int.Parse(currentItem.Value) - int.Parse(currentItem.Quantum)).ToString(),
                        Quantum = currentItem.Quantum,
                        Priority = currentItem.Priority
                    });

                    array.Add(new ProcessorTask
                    {
                        Text = currentItem.Text,
                        Value = currentItem.Quantum,
                        Quantum = currentItem.Quantum,
                        Priority = currentItem.Priority
                    });
                }
            }

            return array;
        }
    }
}