using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.IDAL_IRepo_;
using Izakayamvc.ViewModels.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utilities;

namespace Izakayamvc.Controllers
{
    public class ProductImagesApiController : ApiController
    {
        // GET: api/ProductImagesApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProductImagesApi/5
        public string Get(int id)
        {
            var service = new ProductService(GetRepo());
            var product = service.Get(id);
            string image = product.Image;
            return image;
        }
        // POST: api/ProductImagesApi
        public async Task<string> Post()
        {
            try
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int productId = 0;
                int.TryParse(HttpContext.Current.Request.Form["ProductId"], out productId);
                string uploadCloud = HttpContext.Current.Request.Form["uploadDrive"];
                if (productId <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(productId));
                }
                if (uploadCloud == "true")
                {
                    CheckImage(file);
                    using (Stream stream = file.InputStream)
                    {
                        var image = await ImgurHelper.UploadImageAsync(stream);
                        var service = new ProductService(GetRepo());
                        var productInDb = service.Get(productId);
                        productInDb.ImageUrl = image.Url;
                        productInDb.Image = image.Id;
                        service.Update(productInDb);
                    }
                    return "true";
                }
                else
                {
                    CheckImage(file);
                    string path = HttpContext.Current.Server.MapPath("/img");
                    string newfileName = new UploadFileHelper().UploadImgFile(file, path, false);
                    var service = new ProductService(GetRepo());
                    var productInDb = service.Get(productId);
                    productInDb.Image = newfileName;
                    service.Update(productInDb);
                    return "true";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        private void CheckImage(HttpPostedFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (file.ContentLength <= 0) throw new ArgumentNullException(nameof(file.ContentLength));
        }

        // PUT: api/ProductImagesApi/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ProductImagesApi/5
        public void Delete(int id)
        {
        }
        private IProductRepository GetRepo()
        {
            return new ProductDapperRepository();
        }

    }
}
