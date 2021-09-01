using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NLog;
using StoreApp.DataAccess.Interfaces;
using StoreApp.Models;
using StoreApp.NetFramework.Helpers;
using StoreApp.NetFramework.Services;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using HttpContext = System.Web.HttpContext;

namespace StoreApp.NetFramework.SubDir
{
    public partial class AddStore : System.Web.UI.Page
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IImageService _imageService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public AddStore(IStoreRepository storeRepository, IImageService imageService)
        {
            _storeRepository = storeRepository;
            _imageService = imageService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == HTTPMethods.POST.ToString())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(StoreName.Value))
                    {
                        ErrorLbl.Text = "Store must have a name";
                        ErrorLbl.ForeColor = System.Drawing.Color.DarkRed;
                        return;
                    }
                    string imageUrl = string.Empty;
                    if (FileUpload.HasFile)
                    {
                        imageUrl = ImageUpload();
                    }

                    Store store = new Store
                    {
                        Name = StoreName.Value,
                        TotalNumberOfProducts = Convert.ToInt32(ProductCount.Value),
                        Image = imageUrl,
                        UserName = HttpContext.Current.User.Identity.Name
                    };

                    _storeRepository.Add(store).ConfigureAwait(false).GetAwaiter().GetResult();
                    Response.Redirect("~/SubDir/UserStores");
                }
                catch (FormatException ex)
                {
                    _logger.Error(ex.Message);
                    ErrorLbl.Text = ex.Message;
                    ErrorLbl.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    ErrorLbl.Text = "Unable to add store";
                    ErrorLbl.ForeColor = System.Drawing.Color.DarkRed;
                    return;
                }
            }
        }

        private string ImageUpload()
        {
            var validExtensions = ConfigurationManager.AppSettings["PhotoExtensions"].Split(',');
            var fileExtensions = Path.GetExtension(FileUpload.FileName).ToLower();

            if (validExtensions.Contains(fileExtensions))
            {
                var ms = new MemoryStream();
                try
                {
                    FileUpload.FileContent.CopyTo(ms);
                    IFormFile file = new FormFile(ms, 0, ms.Length, FileUpload.FileName, FileUpload.FileName);
                    return  _imageService.UploadImage(file);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    ms.Dispose();
                    throw;
                }
                finally
                {
                    ms.Dispose();
                }
            }

            throw new FormatException("File must be .jpeg, .jpg or .png");
        }
    }
}