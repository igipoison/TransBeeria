using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class BeerTree
    {
        public Node start;
        private List<Node> nodes;

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

    }
}
