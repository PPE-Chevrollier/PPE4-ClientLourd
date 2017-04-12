using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChevLoc
{
    class cbItem
    {
        public cbItem(string Text, object Value)
        {
            this.Text = Text;
            this.Value = Value;
        }
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
        public int GetValueAsInt()
        {
            if (Value == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(string.Format("{0}", Value));
            }
        }
    }
}
