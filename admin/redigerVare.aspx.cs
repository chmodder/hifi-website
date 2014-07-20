using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_redigerVare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
        WHERE p.productId = @productId";

            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = Request.QueryString["id"];

            //åben for forbindelsen til databasen
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //Henter produktinformation til til textbox'ene
                TextBoxProductModel.Text = reader["productModel"].ToString();
                TextBoxProductDescription.Text = reader["productDescription"].ToString();
                TextBoxProductPrice.Text = reader["productPrice"].ToString();

                //Henter billedet ud.
                productImg.ImageUrl = "../img/products/thumbs/" + (reader["productImg"]);

                //Skriver det billednavn ud, som er er tilknyttet produktet.
                labelProductImgName.Text = reader["productImg"].ToString();

                //Fortæller dropdownlisten, hvilket produkt-id, som er valgt
                DropDownListBrandName.SelectedValue = reader["brandsId"].ToString();

                //gemmer billednavnet fra databasen/readeren, som skal bruges senere
                ViewState["deleteImg"] = reader["productImg"].ToString();
            }


            // Luk for forbindelsen til databasen
            // reader.Close();
            conn.Close();

            
        }
    }



    #region simpel upload til mappe og database

    protected void gemBtn_Click(object sender, EventArgs e)
    {
        if (billedUpload.HasFile)
        {
            try
            {

                //Find gammelt filnavn og slet fil
                try
                {
                    //Bruger billednavnet fra databasen, som er overført fra readeren
                    string imageName = ViewState["deleteImg"].ToString();

                    // slet Originalfilen i image/upload/Original/ mappen
                    File.Delete(Server.MapPath("~/img/products/originals/" + imageName));

                    // Slet Thumbsfilen i /Images/Upload/Thumbs/
                    File.Delete(Server.MapPath("~/img/products/thumbs/" + imageName));
                }

                //Sender en fejlmeddelelse, hvis den gamle fil ikke kunne slettes
                catch (Exception)
                {
                    Label_besked.Text = "<div>Der skete en fejl. Det gamle billede kunne ikke slettes i mappen, så produktet er ikke opdateret</div>";
                }

                //
                //Skriver nyt fil navn i databasen
                //
                // Upload originalen til mappen /images/upload/original/
                billedUpload.SaveAs(Server.MapPath("/img/products/originals/") + billedUpload.FileName);

                // Kald Metoden MakeThumbs, som laver en Thumbnail og uploader den til Thumbs mappen
                MakeThumb(billedUpload.FileName, "/img/products/originals/", 250, "/img/products/thumbs/");

                // Insert i databasen
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = @"
            UPDATE Products 
            SET fkBrandId=@brandsName, productModel=@productModel, productDescription=@productDescription, productPrice=@productPrice, productImg=@productImg
             
            WHERE productId = @urlId";

                cmd.Parameters.Add("@urlId", SqlDbType.Int).Value = Request.QueryString["id"];
                cmd.Parameters.Add("@brandsName", SqlDbType.NVarChar).Value = DropDownListBrandName.SelectedValue;
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

                // Besked om at billedet er gemt. Man kan også bruge session message, hvis man har et  sessionlog in system. Se webintegration del 2
                Label_besked.Text = "<div>Produktet er opdateret med et nyt billede</div>";
                productImg.ImageUrl = "/img/products/originals/" + billedUpload.FileName;
                labelProductImgName.Text = billedUpload.FileName;
            }

            catch (Exception)
            {
                Label_besked.Text = "<div>Billedet blev <b>ikke</b> gemt</div>";
            }

        }

        else
        {
            //Lave SQL update, der ikke rører filnavn kolonnen i databasen.
            // Overskriver filnavn i databasen med det samme
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            //cmd.CommandText = "UPDATE p SET productImg=@imageNotchanged FROM Products p WHERE productId = @ImageId";

            cmd.CommandText = @"
            UPDATE Products 
            SET fkBrandId=@brandsName, productModel=@productModel, productDescription=@productDescription, productPrice=@productPrice, productImg=@imageNotchanged      
            WHERE productId = @urlId";

            cmd.Parameters.Add("@urlId", SqlDbType.Int).Value = Request.QueryString["id"];
            cmd.Parameters.Add("@brandsName", SqlDbType.NVarChar).Value = DropDownListBrandName.SelectedValue;
            cmd.Parameters.Add("@productModel", SqlDbType.NVarChar).Value = TextBoxProductModel.Text;
            cmd.Parameters.Add("@productDescription", SqlDbType.NText).Value = TextBoxProductDescription.Text;
            cmd.Parameters.Add("@productPrice", SqlDbType.Float).Value = TextBoxProductPrice.Text;

            cmd.Parameters.Add("@imageNotchanged", SqlDbType.NVarChar).Value = ViewState["deleteImg"].ToString();
            cmd.Parameters.Add("@ImageId", SqlDbType.Int).Value = Request.QueryString["id"].ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            // Besked om at produktet er opdateret
            Label_besked.Text = "<div>Produktet er opdateret</div>";
        }
    }

    #endregion

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


//protected string thumb()
//{
//    string imageName = ViewState["productThumb"].ToString();
//    //Server.MapPath("~/img/products/thumbs/" + imageName);

//    return "../img/products/thumbs/" + imageName;

//    //return (Server.MapPath("" + imageName));
//    //return ("../img/products/thumbs/creek_classic_cd.jpg");
//}
//protected void tilbageBtn_Click(object sender, EventArgs e)
//{
//    //Response.Redirect("kategori.aspx" + Request.QueryString["id"]);
//}

//protected void gemBtn_Click(object sender, EventArgs e)
//{

//}

