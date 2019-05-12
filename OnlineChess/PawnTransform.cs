using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineChess
{
    public partial class PawnTransform : Form
    {
        public byte chosenFigure = 5;

        public PawnTransform()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void rbtn_Bishop_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                chosenFigure = 3;
        }

        private void rbtn_Rook_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                chosenFigure = 2;
        }

        private void rbtn_Queen_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                chosenFigure = 1;
        }

        private void rbtn_Knight_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                chosenFigure = 5;
        }
    }
}
