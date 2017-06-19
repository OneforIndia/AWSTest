using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSWebAPI.Models
{
    public class AWSEC2
    {
        public string RegionNM { get; set; }
        public string SnapshotId { get; set; }
        public string VolumeId { get; set; }
        public bool Encrypted { get; set; }
        public string KmsKeyId { get; set; }
        public string VolumeType { get; set; }
    }

    public class AWSS3
    {
        public string AWSServiceName { get; set; }
        public string RegionNM { get; set; }
        public string ServerSideEncryptionMethod { get; set; }
        public string BucketName { get; set; }
        public string KeyNM { get; set; }
        public string KmsKeyId { get; set; }
        public string VersionId { get; set; }

        public string ACLList { get; set; }

        public string SnapshotId { get; set; }
        public string VolumeId { get; set; }

        public bool Encrypted { get; set; }

    }

    public class AWSRDS
    {
        public string RegionNM { get; set; }
        public string ServerSideEncryptionMethod { get; set; }
        public string BucketName { get; set; }
        public bool KeyNM { get; set; }
        public string KmsKeyId { get; set; }
        public string VersionId { get; set; }

        public string ACLList { get; set; }
    }

}
