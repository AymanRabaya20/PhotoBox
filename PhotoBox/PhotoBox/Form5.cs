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

    public partial class Form5 : Form
    {

        User user = new User();
        Form3 f = new Form3();
        string path;
        IFirebaseConfig fbc = new FirebaseConfig()
        {
            BasePath = "https://test-9826f-default-rtdb.firebaseio.com/",
            AuthSecret = "LdQxWiOs2FPWsJZAsN70JwCnMsASJ6XG4kPdec8A",
        };
        IFirebaseClient cln;
        public Form5(User u ,Form3 r,string p)
        {
            path = p;
            f = r;
            user = u;
            InitializeComponent();
        }
        private Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ImageForm im = cln.Get("Data/" + user.UserName).ResultAs<ImageForm>();

            if (im != null)
            {
                if (textBox1.Text != "" && textBox1.Text != im.imgName)
                {

                    ImageForm img = new ImageForm()
                    {
                        imgName = textBox1.Text,
                        img = File.ReadAllBytes(path),
                    };
                    cln.Set("Data/" + user.UserName + "/" + textBox1.Text, img);
                    MessageBox.Show("The Photo saved Successfully");
                    this.Close();
                    f.Show();
                }
                else MessageBox.Show("This name is alredy used try anthor or null value");
            } else
            {
                ImageForm img = new ImageForm()
                {
                    imgName = textBox1.Text,
                    img = File.ReadAllBytes(path),
                };
                cln.Set("Data/" + user.UserName + "/" + textBox1.Text, img);
                MessageBox.Show("The Photo saved Successfully");
                this.Close();
                f.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            f.Show();
            this.Close();
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                cln = new FireSharp.FirebaseClient(fbc);
            }
            catch (Exception)
            {

                MessageBox.Show("Check your Connection !");
            }
            pictureBox1.Image = byteArrayToImage( File.ReadAllBytes(path));
        }
    }
}
