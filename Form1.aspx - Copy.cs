using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Net.Mail;

namespace farewell
{
    public partial class Form1 : System.Web.UI.Page
    {
        SqlCommand cmd = new SqlCommand();
        //SqlCommand cmd1 = new SqlCommand();

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VS\FAREWELL\farewell\farewell\App_Data\Databasecp.mdf;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            int n=0,m=0;
            SqlCommand check_User_Name = new SqlCommand("select * from [dbo].[validation] where en ='" + TextBox2.Text + "'", con);
            try
            {
                 n = (int)check_User_Name.ExecuteScalar();
            }
            catch(NullReferenceException ex)
            {
                Response.Redirect("WebForm2.aspx");
            }

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }

            }
            Label1.Text = sMacAddress;
            TextBox13.Text= sMacAddress;
            SqlCommand check_mac = new SqlCommand("select * from [dbo].[userdata] where mac ='" + TextBox13.Text + "'", con);
            try
            {
                m = (int)check_mac.ExecuteScalar();
            }
            catch (InvalidCastException ex)
            {
                Response.Redirect("WebForm4.aspx");
            }

            catch (NullReferenceException ex)
            {

                if (n > 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into [dbo].[userdata]" + "(name,enrollment_no,Handsome_hunk,Fashion_queen,Library_user,Foodie,Introvert,Tiktok,Pubg,Obedient,Fire_brigade,Danveer,mac) values(@name,@enrollment_no,@Handsome_hunk,@Fashion_queen,@Library_user,@Foodie,@Introvert,@Tiktok,@Pubg,@Obedient,@Fire_brigade,@Danveer,@mac)", con);

                    cmd.Parameters.AddWithValue("@name", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@enrollment_no", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Handsome_hunk", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@Fashion_queen", TextBox4.Text);
                    cmd.Parameters.AddWithValue("@Library_user", TextBox5.Text);
                    cmd.Parameters.AddWithValue("@Foodie", TextBox6.Text);
                    cmd.Parameters.AddWithValue("@Introvert", TextBox7.Text);
                    cmd.Parameters.AddWithValue("@Tiktok", TextBox8.Text);
                    cmd.Parameters.AddWithValue("@Pubg", TextBox9.Text);
                    cmd.Parameters.AddWithValue("@Obedient", TextBox10.Text);
                    cmd.Parameters.AddWithValue("@Fire_brigade", TextBox11.Text);
                    cmd.Parameters.AddWithValue("@Danveer", TextBox12.Text);
                    cmd.Parameters.AddWithValue("@mac", Label1.Text);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException )
                    {
                        Response.Redirect("WebForm3.aspx");
                    }



                    Response.Redirect("WebForm1.aspx");


                }
            }
         
            
        }
    }
}