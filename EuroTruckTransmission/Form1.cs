using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroTruckTransmission
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyboardListening.Init();
            Emulation.Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = KeyboardListening.TransmissionUpKey.ToString();
            textBox2.Text = KeyboardListening.TransmissionDownKey.ToString();

            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = GamepadListening.Connect();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Emulation.RealisticTransmission = checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Text = GamepadListening.SelectNextController();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Emulation.ExtraAxisEnabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Emulation.RbAxisMode = checkBox3.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Emulation.CloseAndExit();
            GamepadListening.CloseAndExit();
            KeyboardListening.CloseAndExit();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = e.KeyCode.ToString();

            KeyboardListening.TransmissionUpKey = e.KeyCode;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBox2.Text = e.KeyCode.ToString();

            KeyboardListening.TransmissionDownKey = e.KeyCode;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Text = GamepadListening.SelectPreviousController();
        }
    }
}
