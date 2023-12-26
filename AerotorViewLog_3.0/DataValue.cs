using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerotorViewLog_3._0
{
    internal class DataValue
    {
        public string valName { get; set; }
        public bool Left_Axis { get; set; }
        public bool Right_Axis { get; set; }
        public bool Re_Scale { get; set; }

        public DataValue()
        {
            this.Left_Axis = false;
            this.Right_Axis = false;
            this.Re_Scale = false;
            this.valName = "";
        }

        

        
    }
}
