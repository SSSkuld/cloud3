using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloud3
{
    public class FNode
    {
        public bool is_file;
        public string name;

        public FNode(string _name, bool _is_file)
        {
            is_file = _is_file;
            name = _name;
        }

    }

}
