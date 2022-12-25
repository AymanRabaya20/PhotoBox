using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using System.IO;

namespace PhotoBox
{
    public partial class Form2 : Form
    {
        Form1 f = new Form1();
        public Form2(Form1 fr)
        {
            f = fr;
            InitializeComponent();
        }
        IFirebaseConfig fbc = new FirebaseConfig()
        {
            BasePath = "https://test-9826f-default-rtdb.firebaseio.com/",
            AuthSecret = "LdQxWiOs2FPWsJZAsN70JwCnMsASJ6XG4kPdec8A",
        };
        IFirebaseClient cln;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            f.Show();
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. ..... etc \n" + "2............etc\n" + "3............etc\n" + "4............etc\n");
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            User u = cln.Get("Users/" + textBox1.Text).ResultAs<User>();
            bool flag = true;
            if (textBox1.Text == null || u!=null)
            { MessageBox.Show("the User name par empty or you used an used one try anthor \n"); flag = false; }
            else if (textBox2.Text == null)
            { MessageBox.Show("Write your email please \n"); flag = false; }
            else if (textBox3.Text == null)
            { MessageBox.Show("Write the password \n"); flag = false; }
            else if (textBox4.Text == null)
            { MessageBox.Show("Write the password confirm \n"); flag = false; }
            else if (textBox3.Text != textBox4.Text) { MessageBox.Show("Reconfirm the password\n");flag = false; textBox4.Text = null;
            }
            else if (checkBox1.Checked==false) { MessageBox.Show("you need to accept our terms \n"); flag = false; }
            
            if (flag == true)
            {
                User user = new User()
                {
                    UserName = textBox1.Text,
                    email = textBox2.Text,
                    password = textBox3.Text,
                };

                cln.Set("Users/" + textBox1.Text, user);
                textBox1.Text=null; textBox2.Text = null; textBox3.Text = null; textBox4.Text = null;
                MessageBox.Show("Sing UP Successfully");
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                cln = new FireSharp.FirebaseClient(fbc);
            }
            catch (Exception)
            {

                MessageBox.Show("Check your Connection !");
            }
        }
    }
}
