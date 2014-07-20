using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_opretVare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Finder labelcontrolleren LabelOverskrift på den side den er og henter variablen fra url og putter den ind i MinOverskrift.Text
        Label MinOverskrift = (Label)Master.FindControl("LabelOverskrift");
        MinOverskrift.Text = Request.QueryString["overskrift"].ToString();

    }
    protected void gemBtn_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //opretter produktet med billede
        if (billedUpload.HasFile)
        {


            try
            {
                // Upload originalen til mappen /images/upload/original/
                billedUpload.SaveAs(Server.MapPath("/img/products/originals/") + billedUpload.FileName);

                // Kald Metoden MakeThumbs, som laver en Thumbnail og uploader den til Thumbs mappen
                MakeThumb(billedUpload.FileName, "/img/products/originals/", 250, "/img/products/thumbs/");

                // Insert i databasen
                

                cmd.CommandText = @"
            Insert INTO Products 
           (fkCategoryId, fkBrandId, productModel, productDescription, productPrice, productImg)  VALUES (@fkCategoryId, @brandsName, @productModel, @productDescription, @productPrice, @productImg)";

                cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];
                cmd.Parameters.Add("@brandsName", SqlDbType.Int).Value = DropDownListBrandName.SelectedValue;
                cmd.Parameters.Add("@productModel", SqlDbType.NVarChar).Value = TextBoxProductModel.Text;
                cmd.Parameters.Add("@productDescription", SqlDbType.NText).Value = TextBoxProductDescription.Text;
                cmd.Parameters.Add("@productPrice", SqlDbType.Float).Value = TextBoxProductPrice.Text;
                cmd.Parameters.Add("@productImg", SqlDbType.NVarChar).Value = billedUpload.FileName;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


                //Page_Load(null, null);

                //opdaterer siden med de nye informationer
                // Response.Redirect(Request.RawUrl);

                //Gør tekstboksene read-only
                TextBoxProductModel.Attributes.Add("readonly", "readonly");
                TextBoxProductDescription.Attributes.Add("readonly", "readonly");
                TextBoxProductPrice.Attributes.Add("readonly", "readonly");

                //fjerner gemknappen og filvælgerknappen
                gemBtn.Visible = false;
                billedUpload.Visible = false;

                // Besked om at billedet er gemt. Man kan også bruge session message, hvis man har et  sessionlog in system. Se webintegration del 2
                Label_besked.Text = "<div>Produktet er nu oprettet i databasen</div>";
                productImg.ImageUrl = "/img/products/originals/" + billedUpload.FileName;
                labelProductImgName.Text = billedUpload.FileName;
            }
            
                //Laver en fejlmeddelse, hvis produktet ikke kunne oprettes med et billede.
            catch (Exception)
            {
                Label_besked.Text = "<div>Produktet blev <b>ikke</b> gemt, pga fejl i billedupload</div>";
            }

        }
        
        //Opretter produktet uden billede
        else
        {
            // Insert i databasen
            cmd.CommandText = @"
            Insert INTO Products 
           (fkCategoryId, fkBrandId, productModel, productDescription, productPrice)  VALUES (@fkCategoryId, @brandsName, @productModel, @productDescription, @productPrice)";

            cmd.Parameters.Add("@fkCategoryId", SqlDbType.Int).Value = Request.QueryString["id"];
            cmd.Parameters.Add("@brandsName", SqlDbType.Int).Value = DropDownListBrandName.SelectedValue;
            cmd.Parameters.Add("@productModel", SqlDbType.NVarChar).Value = TextBoxProductModel.Text;
            cmd.Parameters.Add("@productDescription", SqlDbType.NText).Value = TextBoxProductDescription.Text;
            cmd.Parameters.Add("@productPrice", SqlDbType.Float).Value = TextBoxProductPrice.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //Gør tekstboksene read-only
            TextBoxProductModel.Attributes.Add("readonly", "readonly");
            TextBoxProductDescription.Attributes.Add("readonly", "readonly");
            TextBoxProductPrice.Attributes.Add("readonly", "readonly");

            //fjerner gemknappen og filvælgerknappen
            gemBtn.Visible = false;
            billedUpload.Visible = false;

            // Besked om at produktet er oprettet uden billede. Man kan også bruge session message, hvis man har et  sessionlog in system. Se webintegration del 2
            Label_besked.Text = "<div>Produktet er nu oprettet i databasen. <br /> Når du finder et brugbart billede, kan det uploades ved at redigere produktet</div>";
        }
    }

    #region Metoden MakeThumb
    /// <summary>
    /// Opret et Thumb, baseret på en fil og en mappe
    /// </summary>
    /// <param name="Filename">Hvad hedder filen</param>
    /// <param name="UploadFolder">Hvor er den uploadet til</param>
    private void MakeThumb(string Filename, string UploadFolder, int bredde, string thumbuploadfolder)
    {
        // find det uploadede image
        System.Drawing.Image OriginalImg = System.Drawing.Image.FromFile(Server.MapPath("~/") + UploadFolder + Filename);

        // find højde og bredde på image
        int originalWidth = OriginalImg.Width;
        int originalHeight = OriginalImg.Height;

        // bestem den nye bredde på det thumbnail som skal laves
        int newWidth = bredde;

        // beregn den nye højde på thumbnailbilledet
        double ratio = newWidth / (double)originalWidth;
        int newHeight = Convert.ToInt32(ratio * originalHeight);


        Bitmap Thumb = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
        Thumb.SetResolution(OriginalImg.HorizontalResolution, OriginalImg.VerticalResolution);

        // Hvis billedet indeholder nogen form for transperans 
        //(mere eller mindre gennemsigtig, eller en gennemsigtig baggrund) bliver det gjort her
        Thumb.MakeTransparent();


        // opret det nye billede
        Graphics ThumbMaker = Graphics.FromImage(Thumb);
        ThumbMaker.InterpolationMode = InterpolationMode.HighQualityBicubic;

        ThumbMaker.DrawImage(OriginalImg,
            new Rectangle(0, 0, newWidth, newHeight),
            new Rectangle(0, 0, originalWidth, originalHeight),
            GraphicsUnit.Pixel);

        // encoding
        ImageCodecInfo encoder;
        string fileExt = System.IO.Path.GetExtension(Filename);
        switch (fileExt)
        {
            case ".png":
                encoder = GetEncoderInfo("image/png");
                break;

            case ".gif":
                encoder = GetEncoderInfo("image/gif");
                break;

            default:
                // default til JPG 
                encoder = GetEncoderInfo("image/jpeg");
                break;
        }

        EncoderParameters encoderParameters = new EncoderParameters(1);
        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

        // gem thumbnail i mappen /img/products/thumbs/
        Thumb.Save(Server.MapPath("~") + thumbuploadfolder + Filename, encoder, encoderParameters);

        // Fjern originalbilledet, thumbnail mm, fra computerhukommelsen
        OriginalImg.Dispose();
        ThumbMaker.Dispose();
        Thumb.Dispose();

    }

    #region encoding metode

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
        for (int i = 0; i < encoders.Length; i++)
        {
            if (encoders[i].MimeType == mimeType)
            {
                return encoders[i];
            }
        }
        return null;
    }

}
    #endregion
    #endregion





