using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_kategori : System.Web.UI.Page
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

        RepeaterRedProd.DataSource = reader;
        RepeaterRedProd.DataBind();

        // Luk for forbindelsen til databasen
        // reader.Close();
        conn.Close();

    }

    protected void opretProduktBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("opretVare.aspx?id=" + Request.QueryString["id"] + "&overskrift=" + Request.QueryString["fkCategoryId"]);
    }

    protected void sortProducentBtn_Click(object sender, EventArgs e)
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
        WHERE p.fkCategoryId = @fkCategoryId 
        ORDER BY BrandsName, productPrice";

        cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];

        //åben for forbindelsen til databasen
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterRedProd.DataSource = reader;
        RepeaterRedProd.DataBind();

        // Luk for forbindelsen til databasen
        // reader.Close();
        conn.Close();
    }

    protected void sortModelnameBtn_Click(object sender, EventArgs e)
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
        WHERE p.fkCategoryId = @fkCategoryId 
        ORDER BY productModel";

        cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];

        //åben for forbindelsen til databasen
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterRedProd.DataSource = reader;
        RepeaterRedProd.DataBind();

        // Luk for forbindelsen til databasen
        // reader.Close();
        conn.Close();
    }

    protected void sortPriceBtn_Click(object sender, EventArgs e)
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
        WHERE p.fkCategoryId = @fkCategoryId 
        ORDER BY productPrice";

        cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];

        //åben for forbindelsen til databasen
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        RepeaterRedProd.DataSource = reader;
        RepeaterRedProd.DataBind();

        // Luk for forbindelsen til databasen
        // reader.Close();
        conn.Close();
    }

}