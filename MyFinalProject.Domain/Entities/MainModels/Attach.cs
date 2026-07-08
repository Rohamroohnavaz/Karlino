using MyFinalProject.Domain.Entities.Abstraction;
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
        public Attach()
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

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public Company Company { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
        public Guid? RequestResumeId { get; set; }
        public RequestResume RequestResume { get; set; }
        public User? User { get; set; }
        public Guid? UserId { get; set; }

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
