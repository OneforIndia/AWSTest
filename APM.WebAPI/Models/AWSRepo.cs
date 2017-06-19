using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace SMSWebAPI.Models
{
    public class AWSRepository
    {
        readonly string mybucketName = System.Configuration.ConfigurationSettings.AppSettings["awsBucket"].ToString();

        /// <summary>
        /// Retrieves the list of products.
        /// </summary>
        /// <returns></returns>
        internal List<AWSS3> RetrieveS3()
        {
            List<AWSS3> s3list = new List<AWSS3>();
            try
            {
                #region S3
                AmazonS3Client s3Client = new AmazonS3Client(System.Configuration.ConfigurationSettings.AppSettings["awsaccesskey"].ToString(),
    System.Configuration.ConfigurationSettings.AppSettings["awssecretkey"].ToString(),
    Amazon.RegionEndpoint.USEast1);

                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = mybucketName;
                do
                {
                    ListObjectsResponse response = s3Client.ListObjects(request);

                    // Process response.
                    // ...
                    var ssetype = "";
                    response.S3Objects.ForEach(x =>
                    {
                        ssetype = string.Empty;
                        GetObjectRequest bktitemrequest = new GetObjectRequest
                        {
                            BucketName = mybucketName,
                            Key = x.Key,
                        };
                        using (GetObjectResponse bktitemresponse = s3Client.GetObject(bktitemrequest))
                        {
                            AWSS3 awss3 = new AWSS3();
                            awss3.AWSServiceName = "S3";
                            awss3.BucketName = bktitemresponse.BucketName;

                            if (bktitemresponse.ServerSideEncryptionMethod != null)
                            {
                                awss3.ServerSideEncryptionMethod = ((ServerSideEncryptionMethod)(bktitemresponse.ServerSideEncryptionMethod)).Value;
                                awss3.Encrypted = true;
                            }
                            else
                            {
                                awss3.ServerSideEncryptionMethod = "NONE";
                                awss3.Encrypted = false;
                            }
                            awss3.KeyNM = bktitemresponse.Key;
                            if (awss3 != null)
                                s3list.Add(awss3);
                        }

                    });
                    // If response is truncated, set the marker to get the next 
                    // set of keys.
                    if (response.IsTruncated)
                    {
                        request.Marker = response.NextMarker;
                    }
                    else
                    {
                        request = null;
                    }
                } while (request != null);
                #endregion
                #region ec2
                var ec2Client = new AmazonEC2Client(System.Configuration.ConfigurationSettings.AppSettings["awsaccesskey"].ToString(),
System.Configuration.ConfigurationSettings.AppSettings["awssecretkey"].ToString(),
Amazon.RegionEndpoint.USEast1);

                var ec2response = ec2Client.DescribeVolumes();
                ec2response.Volumes.ForEach(ebs => {
                    AWSS3 awss3 = new AWSS3();
                    awss3.AWSServiceName = "EBS";
                    awss3.SnapshotId = ebs.SnapshotId;
                    awss3.VolumeId = ebs.VolumeId;
                    awss3.Encrypted = ebs.Encrypted;

                    if (awss3 != null)
                        s3list.Add(awss3);

                });

                #endregion
            }
            catch (Exception ex)
            {
            }

           return s3list;
        }

    }
}