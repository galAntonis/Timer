using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Media;

namespace Timer
{
    public partial class Form1 : Form
    {
        private int totalSeconds;
        PrivateFontCollection pfc = new PrivateFontCollection();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            pfc.AddFontFile("digital-7.ttf");
            label3.Font = new Font(pfc.Families[0], 60, FontStyle.Regular);
            label3.ForeColor = Color.Red;
            button2.Enabled = false;
            button3.Enabled = false;
           
            for (int i =0; i<60; i++)
            {
                comboBox1.Items.Add(i.ToString());
                comboBox2.Items.Add(i.ToString());
            }
            comboBox1.SelectedIndex = 59;
            comboBox2.SelectedIndex = 59;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            int minutes = int.Parse(comboBox1.SelectedItem.ToString());
            int seconds = int.Parse(comboBox2.SelectedItem.ToString());
            totalSeconds = (minutes * 60) + seconds;
            timer1.Enabled = true;
            setTime(label3, minutes, seconds);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            label3.Text = "00:00";
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pfc.AddFontFile("digital-7.ttf");
            label3.Font = new Font(pfc.Families[0], 60, FontStyle.Regular);
            label3.ForeColor = Color.Red;
            if (totalSeconds > 0)
            {
                totalSeconds--;
                int minutes = totalSeconds / 60;
                int seconds = totalSeconds - (minutes * 60);
                setTime(label3, minutes, seconds);
            }
            else
            {
                timer1.Stop();
                using (var soundPlayer = new SoundPlayer(@"197713__a10fjet__proximity.wav"))
                {
                    soundPlayer.Play(); // can also use soundPlayer.PlaySync()
                }
                MessageBox.Show("Time's up!");
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void setTime(Label label, int minutes, int seconds)
        {
            if (minutes <= 9 && seconds <= 9)
                label.Text = "0" + minutes.ToString() + ":0" + seconds.ToString();
            else if (seconds <= 9)
                label.Text = minutes.ToString() + ":0" + seconds.ToString();
            else if (minutes <= 9)
                label.Text = "0" + minutes.ToString() + ":" + seconds.ToString();
            else if (seconds > 9)
                label.Text = minutes.ToString() + ":" + seconds.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                button3.Text = "CONTINUE";
            } else if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                button3.Text = "PAUSE";
            }
        }
    }
}
