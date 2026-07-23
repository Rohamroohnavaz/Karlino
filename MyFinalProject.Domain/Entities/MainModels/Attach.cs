using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Attach : BaseEntity
    {
        private Attach()
        {
            
        }

        public Attach(string filePath, string fileName ,string contentType ,long fileSize)
        {
            FilePath = filePath;
            FileName = fileName;
            ContentType = contentType;
            FileSize = fileSize;
            Validation();
        }

        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public long FileSize { get; private set; }
        public Company Company { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Guid? AdvertisementId { get; private set; }
        public Advertisement Advertisement { get; private set; }
        public User? User { get; private set; }
        public Guid? UserId { get; private set; }

        public void ChangeFileName(string newFileName)
        {
            if (string.IsNullOrWhiteSpace(newFileName))
                throw new Exception("FileName is required !");

            FileName = newFileName;
        }

        public void ChangeFilePath(string newFilePath)
        {
            if (string.IsNullOrWhiteSpace(newFilePath))
                throw new Exception("FilePath is required !");

            FilePath = newFilePath;
        }

        public void ChangeContentType(string newContentType)
        {
            if (string.IsNullOrWhiteSpace(newContentType))
                throw new Exception("ContentType is required !");

            ContentType = newContentType;
        }

        public void ChangeFileSize(long newFileSize)
        {
            if (newFileSize <= 0)
                throw new Exception("FileSize is invalid !!");

            FileSize = newFileSize;
        }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                throw new Exception("Invalid FilePath");

            if (string.IsNullOrWhiteSpace(FileName))
                throw new Exception("Invalid FileName !!");

            if (string.IsNullOrWhiteSpace(ContentType))
                throw new Exception("Invalid ContentType !!");
        }
    }
}
