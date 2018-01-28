using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class BeerTree
    {
        public SwitchNode start;
        private List<Node> nodes = new List<Node>();

        public BeerTree(SwitchNode start)
        {
            this.start = start;
            this.start.switchObject.state = StateFactory.Create(2);
            nodes = new List<Node>();
            nodes.Add(start);
        }
        public void AddHouse(HouseNode house, string nodeId)
        {
            SwitchNode parentSwitch = (SwitchNode) nodes.Where(s => s.nodeId.Equals(nodeId)).FirstOrDefault();
            parentSwitch.childs.Add(house);
            nodes.Add(house);
        }

        public void AddSwitch(SwitchNode swich, string nodeId)
        {
            SwitchNode parentSwitch = (SwitchNode)nodes.Where(s => s.nodeId.Equals(nodeId)).FirstOrDefault();
            parentSwitch.childs.Add(swich);
            nodes.Add(swich);

        }

        public void SetNodeState(string nodeId, int status)
        {
            ((SwitchNode)nodes.FirstOrDefault
                (n => n.nodeId.Equals(nodeId))).switchObject.state = StateFactory.Create(status);
        }

        public SwitchNode getRootNode()
        {
            return start;
        }
    }
}
