using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResultforOutput
    {

        private string  _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _points;

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public ResultforOutput(string name, int points)
        {
            Name = name;
            Points = points;
        }
    }
}
