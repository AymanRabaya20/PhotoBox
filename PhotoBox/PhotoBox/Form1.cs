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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f = new Form4(this);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                User user = cln.Get("Users/" + textBox1.Text).ResultAs<User>();
                if (user != null)
                {
                    if (user.password == textBox2.Text)
                    {
                        textBox2.Text = null;
                        this.Hide();
                        Form3 f = new Form3(user, this,cln);
                        f.Show();
                    }
                    else MessageBox.Show("Wrong Password ");
                }
                else MessageBox.Show("There is no such a user with this name ! \n");

            }
            catch (Exception)
            {

                MessageBox.Show("Check your counction !");
            }
            
        }
        IFirebaseConfig fbc = new FirebaseConfig()
        {
            BasePath = "https://test-9826f-default-rtdb.firebaseio.com/",
            AuthSecret = "LdQxWiOs2FPWsJZAsN70JwCnMsASJ6XG4kPdec8A",
        };
        IFirebaseClient cln;
        private void Form1_Load(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
