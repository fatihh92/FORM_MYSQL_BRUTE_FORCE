using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace brute_force
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string sunucu { get; set; }
        public string kullanıcı { get; set; }
        public string parola { get; set; }
        public string server;
        public string user;
        public string pass;
        MySqlConnection mysqlbaglan;
        Stopwatch pastime = new Stopwatch();

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.ForeColor = Color.DarkGreen;
            label2.ForeColor = Color.DarkGreen;
            label3.ForeColor = Color.DarkGreen;
            label4.ForeColor = Color.DarkGreen;
            label5.ForeColor = Color.DarkGreen;
            label6.ForeColor = Color.DarkGreen;
            label7.ForeColor = Color.DarkGreen;
            button1.ForeColor = Color.DarkGreen;
            button2.ForeColor = Color.DarkGreen;
            button3.ForeColor = Color.DarkGreen;
            button4.ForeColor = Color.DarkGreen;
            button5.ForeColor = Color.DarkGreen;
            button6.ForeColor = Color.DarkGreen;
            listBox1.Items.Clear();
            listBox1.ForeColor = Color.DarkGreen;
            label4.Text = "YOUR QUERY:(Only SELECT Queries)";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = sunucu;
            server = textBox1.Text.ToString();
            textBox2.Text = kullanıcı;
            user = textBox2.Text.ToString();
            textBox3.Text = parola;
            pass = textBox3.Text.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            server = textBox1.Text.ToString();
            user = textBox2.Text.ToString();
            pass = textBox3.Text.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pastime.Reset();
            pastime.Start();
            listBox1.Items.Clear();
            int sayı = 1;
            try
            {
                mysqlbaglan = new MySqlConnection("Server=" + server + ";Uid=" + user.ToString() + ";Pwd=" + pass.ToString() + ";SslMode=none");
                MySqlCommand cmd = new MySqlCommand("SELECT distinct table_schema FROM information_schema.tables WHERE TABLE_TYPE = 'BASE TABLE'", mysqlbaglan);
                mysqlbaglan.Open();
                MySqlDataReader myreader = cmd.ExecuteReader();
                while(myreader.Read())
                {
                    if(listBox1.Items.Contains(myreader.ToString()) == false)
                    {
                        listBox1.Items.Add(sayı.ToString() + ")" + myreader.GetString(0));
                        sayı++;
                    }
                }
                pastime.Stop();
                label7.Text = pastime.Elapsed.Hours.ToString()+":"+pastime.Elapsed.Minutes.ToString()+":"+pastime.Elapsed.Seconds.ToString()+":"+(pastime.Elapsed.Milliseconds/10).ToString();
                label5.Text = "Information:Listed Databases";
                mysqlbaglan.Close();
            }
            catch
            {
                label5.Text = "Information: Couldn't Listed";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            pastime.Reset();
            if (textBox4.Text.Contains("SELECT") == true || textBox4.Text.Contains("select") == true || textBox4.Text.Contains("Select") == true)
            {
                try
                {
                    pastime.Start();
                    label5.Text = "Information:Query Worked";
                    label4.Text = "YOUR QUERY:";
                    int sayı = 1;
                    mysqlbaglan = new MySqlConnection("Server=" + server + ";Uid=" + user.ToString() + ";Pwd=" + pass.ToString() + ";SslMode=none");
                    MySqlCommand cmd = new MySqlCommand(textBox4.Text.ToString(), mysqlbaglan);
                    mysqlbaglan.Open();
                    MySqlDataReader myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        //listBox1.Items.Add(sayı.ToString() + ")" + myreader.GetString(0)+" - "+myreader.GetString(1)+" - "+myreader.GetString(2)+" - "+myreader.GetString(3)+" - "+myreader.GetString(4));
                        if (myreader.ToString().Contains("username"))
                        { listBox1.Items.Add(sayı.ToString() + ")" + myreader["username"] + " - " + myreader["password"]); }
                        else { listBox1.Items.Add(sayı.ToString() +")"+myreader[1]+" - "+myreader[2]); }
                        sayı++;
                    }
                }
                catch(Exception err)
                {
                    label5.Text = err.Message;
                }
            }
            else
            {
                label5.Text = "Information:No Select Query";
                label4.Text = "YOUR QUERY:(Only SELECT Queries)";
            }
            label7.Text = pastime.Elapsed.Hours.ToString() + ":" + pastime.Elapsed.Minutes.ToString() + ":" + pastime.Elapsed.Seconds.ToString() + ":" + (pastime.Elapsed.Milliseconds/10).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
