using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicForm
{
    public partial class Form1 : Form
    {
        public string ConString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt16(txtId.Text);
            string c = "Data source = KRISHNA\\sqlexpress; Initial catalog = SagarDB; Integrated security = true";
            SqlConnection scon = new SqlConnection(c);
            
            try
            {
                // Convert Image to byte array
                byte[] imageData = ImageToByteArray(pictureBox1.Image);

                // Open the connection
                scon.Open();

                // SQL command to insert binary data into database
                string sql = "INSERT INTO tbl_picture (id, picture) VALUES ("+id+", @picture)";
                SqlCommand cmd = new SqlCommand(sql, scon);
                cmd.Parameters.AddWithValue("@picture", imageData);

                // Execute the command
                cmd.ExecuteNonQuery();
                MessageBox.Show("Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading image: {ex.Message}");
            }
            finally
            {
                // Close the connection
                scon.Close();
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Change format as per your requirement
                return ms.ToArray();
            }
        }

        private void btnGetImage_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(txtId.Text);
            string c = "Data source = KRISHNA\\sqlexpress; Initial catalog = SagarDB; Integrated security = true";
            SqlConnection scon = new SqlConnection(c);

            try
            {
   
                // Open the connection
                scon.Open();

                // SQL command to insert binary data into database
                string sql = "select picture from tbl_picture where  id =  " + id + "";
                SqlCommand cmd = new SqlCommand(sql, scon);
                //cmd.Parameters.AddWithValue("@picture", imageData);

                // Execute the command
                SqlDataAdapter adp = new SqlDataAdapter(cmd.CommandText, scon);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                byte[] image = (byte[])dt.Rows[0][0];
                Image img;
                using (MemoryStream ms = new MemoryStream(image))
                {
                    img = Image.FromStream(ms);
                }
                pictureBox1.Image = img;

                MessageBox.Show("Image retrieved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading image: {ex.Message}");
            }
            finally
            {
                // Close the connection
                scon.Close();
            }

        }
    }
}
