using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class kontakt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label MinOverskrift = (Label)Master.FindControl("LabelOverskrift");
        MinOverskrift.Text = "Kontakt";
    }
    protected void button_Click(object sender, EventArgs e)
    {
        if ((tlf.Text == "")||(tlf.Text.Length != 8))
        {
            tlf.Text = "00000000";
        }

        // opret forbindelse til databasen

        // FELTET MELLEM [] SKAL ÆNDRES SÅ DET PASSER TIL NAVNET PÅ DIN CONNECTIONSTRING - KAN FINDES I WEB.CONFIG FILEN
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        // SQL strengen
        cmd.CommandText = "INSERT INTO Beskeder (fornavn, efternavn, adresse, postnr, bynavn, email, tlf, besked, oprettetDen) VALUES (@fornavn, @efternavn, @adresse, @postnr, @bynavn, @email, @tlf, @besked, @oprettetDen)";

        // Tilføj parametrer (input fra brugeren / TextBox fra .aspx siden) til din SQL streng
        cmd.Parameters.Add("@fornavn", SqlDbType.NVarChar).Value = fornavn.Text;
        cmd.Parameters.Add("@efternavn", SqlDbType.NVarChar).Value = efternavn.Text;
        cmd.Parameters.Add("@adresse", SqlDbType.NVarChar).Value = adresse.Text;
        cmd.Parameters.Add("@postnr", SqlDbType.Int).Value = Convert.ToInt32(postnr.Text);
        cmd.Parameters.Add("@bynavn", SqlDbType.NVarChar).Value = bynavn.Text;
        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email.Text;
        cmd.Parameters.Add("@tlf", SqlDbType.Int).Value = Convert.ToInt32(tlf.Text);
        cmd.Parameters.Add("@besked", SqlDbType.NText).Value = besked.Text;
        cmd.Parameters.Add("@oprettetDen", SqlDbType.DateTime).Value = DateTime.Now;

        // Åben for forbindelsen til databasen
        conn.Open();

        // Udfør SQL komandoen
        cmd.ExecuteNonQuery();

        // Luk for forbindelsen til databasen
        conn.Close();

        // Besked til brugeren om at beskeden er modtaget
        Label_besked.Text = "Tak! Vi har modtaget din besked og den vil blive behandlet indenfor 24 timer";

        fornavn.Text = "";
        efternavn.Text = "";
        adresse.Text = "";
        postnr.Text = "";
        bynavn.Text = "";
        email.Text = "";
        tlf.Text = "";
        besked.Text = "";
    }
}