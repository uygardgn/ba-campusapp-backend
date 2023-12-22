using System.Globalization;
using System.Security.AccessControl;
using BACampusApp.Entities.Enums;

namespace BACampusApp.Core.Utilities.Helpers
{
    public class FileManagementHelper
    {
        /// <summary>
        /// Bu metot kullanıcıdan alınan dosyayı, verilen dosya kategorisi yardımıyla ilgili klasöre kayıt eder.
        /// 
        /// - Homework : Ödev oluştururken kullanılacak olan enum parametre.
        /// - StudentHomework : Öğrencinin yükleyeceği ödev için kullanılacak parametre.
        /// - SupplementaryResources : Yardımcı kaynak eklemek için kullanılacak olan parametre.
        /// 
        /// Kategori tipleri BACampusApp.Core.Enums.FileCategory içinden gelir.
        /// </summary>
        /// <param name="file">Kullanıcıdan alınan dosya</param>
        /// <param name="fileCategory">Yapılan işleme bağlı olarak seçilecek olan dosya kategorisi tipi.</param>
        /// <returns>Kullanıcıdan alınan dosya için oluşturulan Id ve dosya uzantısını döner.</returns>
        public static async Task<FileInformations> UploadFileAsync(IFormFile file, FileCategory fileCategory, int? ResourceType)
        {
            // Yüklenecek dosyanın bilgilerini içerecek ve geri döndürülecek olan nesne
            FileInformations fileInformations = new();
            try
            {
                // Kayıt klasörü atama
                string targetPath = "";

                if (fileCategory == FileCategory.Homework)
                {
                    targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\Homework";
                }
                else if (fileCategory == FileCategory.StudentHomework)
                {
                    targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\StudentHomework";
                }
                else if (fileCategory == FileCategory.SupplementaryResource)
                {
                    //ResourceType 2  = Video
                    if (ResourceType == 2)
                    {
                        targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources\\Videos";
                    }
                    else
                    {
                        targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources";
                    }
                }

                // Dosya uzantısını alma
                string extension = Path.GetExtension(file.FileName);

                string itemId = Guid.NewGuid().ToString();

                // Oluşacak dosya adını elde etme
                fileInformations.FileName = itemId + extension;

                // Dosya resim türlerinden biri ise byte dizisine çevirilmesi
                if (file != null && file.ContentType != null && file.ContentType.Contains("image"))
                {
                    fileInformations.FileType = FileType.Image;

                    byte[] imageBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }

                    fileInformations.FileByteArrayFormat = imageBytes;
                }
                else if (file.ContentType.Contains("http") || file.ContentType.Contains("html"))
                {
                    // Eğer dosya internet adresi ise, dosyanın bağlantısını saklayın
                    fileInformations.FileType = FileType.Other;
                    fileInformations.FileName = file.FileName;

                }
                else
                {
                    fileInformations.FileType = FileType.Other;

                    // Hedef dosya yolunu oluşturma
                    string targetFilePath = Path.Combine(targetPath, itemId + extension);

                    // Dosyayı hedef klasöre kopyalama
                    using (var stream = new FileStream(targetFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                // Başarılı oldu mesajı iletme
                fileInformations.Message = "Success";

                return fileInformations;
            }
            catch (Exception ex)
            {
                // Başarısız oldu mesajı iletme
                fileInformations.Message = $"Failure - {ex.Message}";

                return fileInformations;
            }
        }

        public static async Task<FileInformations> PermanentlyDeleteSoftDeletedFile(string filePath, FileType? fileType)
        {
            FileInformations fi = new();
            string targetPath = "";

            try
            {
                if (fileType == FileType.Other)
                {
                    targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources\\DeletedSupplementaryResources";

                    var entityFilePath = Path.Combine(targetPath, filePath);

                    File.Delete(entityFilePath);
                }

                fi.Message = "Success";
                fi.FileType = null;
                return fi;
            }
            catch (Exception ex)
            {
                fi.Message = $"Failure - {ex.Message}";

                return fi;
            }
        }

        public static async Task<FileInformations> DeleteFileAsync(string filePath, FileCategory fileCategory, FileType? fileType)
        {
            FileInformations fi = new();
            try
            {
                string targetPath = "";
                if (fileType == FileType.Other)
                {
                    if (fileCategory == FileCategory.Homework)
                    {
                        targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\Homework";
                    }
                    else if (fileCategory == FileCategory.StudentHomework)
                    {
                        targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\StudentHomework";
                    }
                    else if (fileCategory == FileCategory.SupplementaryResource)
                    {
                        if (filePath.Contains(".mp4"))
                        {
                            targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources\\Videos";
                        }
                        else
                        {
                            targetPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources";
                        }
                    }

                    var entityFilePath = Path.Combine(targetPath, filePath);

                    File.Delete(entityFilePath);

                }


                fi.Message = "Success";
                fi.FileType = null;
                return fi;
            }
            catch (Exception ex)
            {
                fi.Message = $"Failure - {ex.Message}";

                return fi;
            }

        }


        public static void MoveToDeletedFiles(string filePath, FileCategory fileCategory, FileType? fileType)
        {

            FileInformations fi = new();
            try
            {
                string fromPath = "";
                string toPath = "";
                if (fileType == FileType.Other)
                {
                    if (fileCategory == FileCategory.Homework)
                    {
                        fromPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\Homework";
                        toPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\Homework\\DeletedHomework";
                    }
                    else if (fileCategory == FileCategory.StudentHomework)
                    {
                        fromPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\StudentHomework";
                        toPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\StudentHomework\\DeletedStudentHomework";
                    }
                    else if (fileCategory == FileCategory.SupplementaryResource)
                    {
                        fromPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources";
                        toPath = Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources\\DeletedSupplementaryResources";
                    }

                    var from = Path.Combine(fromPath, filePath);
                    var to = Path.Combine(toPath, filePath);
                    if (filePath.Contains("https") || filePath.Contains("http") || filePath.Contains(".com") || filePath.Contains("jpeg") || filePath.Contains("jpg") || filePath.Contains("png") || filePath.Contains(".mp4"))
                    {

                    }
                    else
                    {

                        File.Move(from, to);
                    }

                }
            }
            catch (Exception ex)
            {


                throw new Exception($"Message : {ex.Message}");
            }

        }
        public static async Task<FileInformations> UpdateFileAsync(string filePath, IFormFile file, FileCategory fileCategory, FileType? fileType, bool isHardDelete, int? resourceType)
        {
            FileInformations fileInformations = new FileInformations();
            fileInformations.Message = "Success";

            //// Video update edildiğinde eski videonun tutulmaması için yazıldı. 
            //if (resourceType == 2) //ResourceType 2 = Video
            //{
            //    isHardDelete = true;
            //}

            if (isHardDelete)
            {
                await DeleteFileAsync(filePath, fileCategory, fileType);
            }
            else
            {
                MoveToDeletedFiles(filePath, fileCategory, fileType);
            }
            if (file != null)
            {

                var newFileName = await UploadFileAsync(file, fileCategory, resourceType);
                return newFileName;
            }
            return fileInformations;
        }

        public static string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static bool Recover(string fileUrl)
        {
            string filePath = string.Empty;
            string targetPath = string.Empty;
            filePath = Path.Combine(Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources\\DeletedSupplementaryResources", fileUrl);
          

            if (Path.Exists(filePath))
            {
               
                    targetPath = Path.Combine(Environment.CurrentDirectory + "\\wwwroot\\documents\\SupplementaryResources", fileUrl);

                File.Move(filePath, targetPath);
                return true;
            }
            else
                return false;
        }
    }
}
