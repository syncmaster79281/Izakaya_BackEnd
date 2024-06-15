using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Izakayamvc.ViewModels.Infra
{
    public class UploadFileHelper
    {
        public const string NoImage = "NoImage.jpg";
        int _targetHeight;
        int _targetWidth;
        /// <summary>
        /// 將上傳檔案放到指定資料夾
        /// </summary>
        /// <param name="file">上傳的檔案</param>
        /// <param name="path">檔案存放資料夾</param>
        /// <returns>回傳實際存放的檔名</returns>
        /// <exception cref="ArgumentNullException">若沒上傳檔案,丟出例外</exception>
        /// <exception cref="ArgumentException">上傳非照片檔案,丟出例外</exception>
        /// <exception cref="Exception">指定的資料夾不存在,丟出例外</exception>
        public string UploadImgFile(HttpPostedFileBase file, string path, bool wantResize)
        {
            // 判斷有沒有上傳檔案,若沒有,在ModelState裡加入error
            //取得副檔名並判斷是不是允許得檔案類型
            //設定想要的圖片長寬
            //為了避免不同時間上傳相同檔名,造成覆蓋,所以每次都要取一個唯一的檔名
            //與副檔名合併成一個正常檔名
            //將上傳檔案存放,並取得檔名
            //將檔名寫到model.FileName
            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentNullException("file沒上傳");
            }
            string[] allowExts = { ".jpg", ".jpeg", ".png" };
            string ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowExts.Contains(ext))
            {
                throw new ArgumentException($"不允許上傳此檔案{ext}");
            }
            if (!Directory.Exists(path))
            {
                throw new Exception($"資料夾{path}不存在");
            }

            if (wantResize == true)
            {
                file = ResizeImage(file);
            }
            string fileName = Path.GetRandomFileName();

            string newfileName = fileName + ext;
            string fullName = Path.Combine(path, newfileName);
            file.SaveAs(fullName);
            return newfileName;

        }
        public string UploadImgFile(HttpPostedFile file, string path, bool wantResize)
        {
            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentNullException("file沒上傳");
            }
            string[] allowExts = { ".jpg", ".jpeg", ".png" };

            string ext = Path.GetExtension(file.FileName).ToLower();
            if (!allowExts.Contains(ext))
            {
                throw new ArgumentException($"不允許上傳此檔案{ext}");
            }
            if (!Directory.Exists(path))
            {
                throw new Exception($"資料夾{path}不存在");
            }
            //取得亂數名稱
            string fileName = Path.GetRandomFileName();
            string newfileName = fileName + ext;
            //檔案存到指定路徑
            string fullName = Path.Combine(path, newfileName);

            // 保存文件
            if (wantResize == true)
            {
                var savefile = ResizeImage(file);
                savefile.SaveAs(fullName);
            }
            else
            {
                file.SaveAs(fullName);
            }
            return newfileName;

        }
        private HttpPostedFileBase ResizeImage(HttpPostedFile file)
        {
            Image originalImage = GetImageFromPostedFile(file);

            int newWidth, newHeight;
            if (_targetHeight == 0) _targetHeight = 300;
            if (_targetWidth == 0) _targetWidth = 300;

            CalculateNewDimensions(originalImage.Width, originalImage.Height, _targetWidth, _targetHeight, out newWidth, out newHeight);

            using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
            {
                using (Graphics g = Graphics.FromImage(resizedBitmap))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }
                // 保存到文件
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedBitmap.Save(ms, ImageFormat.Jpeg);
                    imageBytes = ms.ToArray();
                }

                // 將 byte[] 創建為新的 HttpPostedFileBase
                MemoryPostedFile compressedFile = new MemoryPostedFile(imageBytes, file.FileName, "image/jpeg");

                return compressedFile;
            }
        }

        private static Image GetImageFromPostedFile(HttpPostedFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                return Image.FromStream(ms);
            }
        }
        //縮放圖片大小
        private HttpPostedFileBase ResizeImage(HttpPostedFileBase file)
        {
            Image originalImage = GetImageFromPostedFile(file);

            int newWidth, newHeight;
            if (_targetHeight == 0) _targetHeight = 300;
            if (_targetWidth == 0) _targetWidth = 300;

            CalculateNewDimensions(originalImage.Width, originalImage.Height, _targetWidth, _targetHeight, out newWidth, out newHeight);

            using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
            {
                using (Graphics g = Graphics.FromImage(resizedBitmap))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }
                // 保存到文件
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedBitmap.Save(ms, ImageFormat.Jpeg);
                    imageBytes = ms.ToArray();
                }

                // 將 byte[] 創建為新的 HttpPostedFileBase
                HttpPostedFileBase compressedFile = new MemoryPostedFile(imageBytes, file.FileName, "image/jpeg");

                return compressedFile;
            }
        }

        private static Image GetImageFromPostedFile(HttpPostedFileBase file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                return Image.FromStream(ms);
            }
        }

        private static void CalculateNewDimensions(int originalWidth, int originalHeight, int targetWidth, int targetHeight, out int newWidth, out int newHeight)
        {
            if (originalWidth > originalHeight)
            {
                newWidth = targetWidth;
                newHeight = (int)((double)originalHeight / originalWidth * targetWidth);
            }
            else if (originalWidth < originalHeight)
            {
                newWidth = (int)((double)originalWidth / originalHeight * targetHeight);
                newHeight = targetHeight;
            }
            else
            {
                newWidth = targetWidth;
                newHeight = targetHeight;
            }
        }
        public void SetImageSize(int targetWidth, int targetHeight)
        {
            _targetWidth = targetWidth;
            _targetHeight = targetHeight;
        }
    }

    //override HttpPostedFileBase 
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] _imageBytes;
        private readonly string _fileName;
        private readonly string _contentType;

        public MemoryPostedFile(byte[] imageBytes, string fileName, string contentType)
        {
            _imageBytes = imageBytes;
            _fileName = fileName;
            _contentType = contentType;
        }
        public override int ContentLength => _imageBytes.Length;

        public override string FileName => _fileName;

        public override string ContentType => _contentType;

        public override Stream InputStream => new MemoryStream(_imageBytes);
        public override void SaveAs(string filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                InputStream.CopyTo(fileStream);
            }
        }
    }
}
