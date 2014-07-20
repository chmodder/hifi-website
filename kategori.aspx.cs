using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class kategori : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Finder labelcontrolleren LabelOverskrift på den side den er og henter variablen fra url og putter den ind i MinOverskrift.Text
        Label MinOverskrift = (Label)Master.FindControl("LabelOverskrift");
        MinOverskrift.Text = Request.QueryString["overskrift"].ToString();

        // opret forbindelse til databasen
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        // SQL strengen
        cmd.CommandText = @"

        SELECT p.*, c.*, b.* 
        FROM Products p INNER JOIN Categories c ON p.fkCategoryId = c.categoriesId 
        INNER JOIN Brands b ON p.fkBrandId = b.BrandsId 
        WHERE p.fkCategoryId = @fkCategoryId 
        ORDER BY BrandsName, productPrice";

        cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];

        //åben for forbindelsen til databasen
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();



        RepeaterProdukter.DataSource = reader;
        RepeaterProdukter.DataBind();




        // Luk for forbindelsen til databasen
        // reader.Close();
        conn.Close();

    }

}