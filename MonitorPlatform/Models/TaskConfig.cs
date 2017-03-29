using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Models
{
    /// <summary>
    /// 任务配置
    /// </summary>
    public class TaskConfig
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string taskname { get; set; }
        /// <summary>
        /// 开关
        /// </summary>
        public bool qswitch { get; set; }
        /// <summary>
        /// 执行频次
        /// </summary>
        public TimingTaskType execfrequency { get; set; }
        /// <summary>
        /// 0：串行执行，1：并行执行
        /// </summary>
        public int serialorparallel { get; set; }

        public List<TackServiceConfig> taskService { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public int exectimes { get; set; }

        public string execfrequencyName
        {
            get
            {
                switch (execfrequency)
                {
                    case TimingTaskType.PerHour:
                        return "每小时一次";
                    case TimingTaskType.PerDay:
                        return "每天一次";
                    case TimingTaskType.PerWeek:
                        return "每周一次";
                    case TimingTaskType.PerMonth:
                        return "每月一次";
                }

                return "";
            }
        }

        public string serialorparallelName
        {
            get
            {
                if (serialorparallel == 0)
                {
                    return "串行";
                }
                else if (serialorparallel == 1)
                {
                    return "并行";
                }
                return "";
            }
        }

        public string shorttimeName
        {
            get; set;
        }
    }

    public class TackServiceConfig
    {
        public string pluginname { get; set; }
        public string controller { get; set; }
        public string method { get; set; }
        public string argument { get; set; }
    }

    /// <summary>
    /// TimingTaskType 定时任务的类型 
    /// </summary>
    public enum TimingTaskType
    {
        PerHour = 0,
        PerDay = 1,
        PerWeek = 2,
        PerMonth = 3
    }
}
