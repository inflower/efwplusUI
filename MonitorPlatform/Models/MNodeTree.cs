using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WcfFrame.DataSerialize;

namespace MonitorPlatform.Models
{
    public class MNodeTree
    {
        /// <summary>
        /// 根中间件节点
        /// </summary>
        public MNodeObject RootMNode
        {
            get
            {
                if (_allMNodeList != null)
                {
                    return _allMNodeList.Find(x => x.ServerIdentify == x.PointToMNode);
                }
                return null;
            }
        }
        private List<MNodeObject> _allMNodeList;
        /// <summary>
        /// 所有中间件节点
        /// </summary>
        public List<MNodeObject> AllMNodeList
        {
            get { return _allMNodeList; }
            set { _allMNodeList = value; }
        }
    }
}
