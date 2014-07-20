using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_sletVare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // opret forbindelse til databasen
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        // SQL strengen
        cmd.CommandText = @"

        SELECT p.*, c.*, b.* 
        FROM Products p INNER JOIN Categories c ON p.fkCategoryId = c.categoriesId 
        INNER JOIN Brands b ON p.fkBrandId = b.BrandsId 
        WHERE p.productId = @fkCategoryId 
        ORDER BY BrandsName, productPrice";

        cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];

        //åben for forbindelsen til databasen
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterSletProd.DataSource = reader;
        RepeaterSletProd.DataBind();

        if (reader.Read())
        {
            //gemmer billednavnet fra databasen/readeren, som skal bruges senere
            ViewState["productImg"] = reader["productImg"].ToString();
        }


        // Luk for forbindelsen til databasen
        // reader.Close();
        conn.Close();

    }

    protected void sletProdBtn_Click(object sender, EventArgs e)
    {


        //if (!String.IsNullOrEmpty(imageName))
        //{

        //Find gammelt filnavn og slet fil
        //try
        //{
        //Bruger billednavnet fra databasen, som er overført fra readeren
        string imageName = ViewState["productImg"].ToString();

        // slet Originalfilen i image/upload/Original/ mappen
        File.Delete(Server.MapPath("/img/products/originals/" + imageName));

        // Slet Thumbsfilen i /Images/Upload/Thumbs/
        File.Delete(Server.MapPath("/img/products/thumbs/" + imageName));
        //}

        ////Sender en fejlmeddelelse, hvis den gamle fil ikke kunne slettes
        //catch (Exception)
        //{
        //    Label_besked.Text = "<div>Der skete en fejl. Filen kunne ikke slettes</div>";
        //}

        //}

        //Opretter forbindelse til databasen
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        // slet filen i databasen
        cmd.CommandText = "DELETE FROM Products WHERE productId = @id";
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"].ToString();
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}