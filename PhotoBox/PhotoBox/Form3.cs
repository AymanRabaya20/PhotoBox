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
    public partial class Form3 : Form
    {
        User user = new User();
        Form1 fr = new Form1();
        IFirebaseClient cln;

        public Form3(User u,Form1 f,IFirebaseClient cl)
        {
            
            InitializeComponent();
            fr = f;
            cln = cl;
            user = u;
        }
        private Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
        public Form3()
        {
            InitializeComponent();
        }
        IFirebaseConfig fbc = new FirebaseConfig()
        {
            BasePath = "https://test-9826f-default-rtdb.firebaseio.com/",
            AuthSecret = "LdQxWiOs2FPWsJZAsN70JwCnMsASJ6XG4kPdec8A",
        };
       

        private void Form3_Load(object sender, EventArgs e)
        {

            label1.Text = "Welcome " + user.UserName;int a,z ;string []s =new string[100] ;
            listView1.View = View.Details;

            imageList1.ImageSize = new Size(150, 150);
            


            var im = cln.Get("Data/" +user.UserName +"/" ).ResultAs<IDictionary<string,ImageForm>>();
            z = 0;
            foreach (ImageForm f in im.Values)
            {
                imageList1.Images.Add(byteArrayToImage(f.img));
                s[z] = f.imgName; z++;
            }
            listView1.SmallImageList = imageList1;
            a = 0;
            foreach (Image i in imageList1.Images)
            {
                listView1.Items.Add(s[a], a);
                a++; 
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fr.Show();
            this.Close();
            

        }
        string path;
       // File.ReadAllBytes(path)
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            path = openFileDialog1.FileName;

            this.Hide(); 
            Form5 f1 = new Form5(user,this,path);
            f1.Show();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string s=listView1.SelectedItems[0].SubItems[0].Text;
            MessageBox.Show(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImageForm im = cln.Get("Data/" + user.UserName + "/"+textBox1.Text).ResultAs<ImageForm>();

            if (im != null)
            {


                if (im.imgName == textBox1.Text)
                {
                    pictureBox1.Image = byteArrayToImage(im.img);
                }


            }else MessageBox.Show("There no photo with this name");

        }
    }
}
