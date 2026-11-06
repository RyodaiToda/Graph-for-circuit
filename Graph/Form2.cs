using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form2 : Form
    {

        Matrix I_result;
        Label lb = new Label();

        public Form2(Matrix m)
        {
            lb.Top = 0;
            lb.Left = 0;
            lb.Width = this.Width;
            lb.Height = this.Height;
            lb.Font = new Font("MS UI Gothic", 15);
            Controls.Add(lb);


            InitializeComponent();

            this.Size = new Size(400, 200);
            I_result = m;

            String s = "";

            for(int i = 0; i < I_result.getRow(); i++)
            {
                s += "I" + i.ToString() + " = " + I_result.getElement(i, 0).ToString() + "[A]\n";
            }

            lb.Text = s;

        }
    }
}
