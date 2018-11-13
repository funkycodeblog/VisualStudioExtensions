using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualStudioExtensions.GenerateClass
{
    public partial class GenerateClassForm : Form
    {
        public GenerateClassForm()
        {
            InitializeComponent();
        }

        public void SetData(string className, List<string> names)
        {
            if (!string.IsNullOrEmpty(className))
                textBox2.Text = className;

            if (null != names)
                textBox2.Lines = names.ToArray();
        }

        public GenerateClassInputData GetInputData()
        {
            return new GenerateClassInputData
            {
                ClassName = textBox2.Text,
                PropertyNames = textBox1.Lines.ToList()
            };

        }
    }
}
