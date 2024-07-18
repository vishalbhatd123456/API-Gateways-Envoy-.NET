using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Threading.Tasks;


namespace s3Basics
{
    //we try to upload to s3
    class Program
    {
        private static readonly string bucketName = "your-bucket-name";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 s3Client;

        public static async Task Main(string[] args)
        {
            s3Client = new AmazonS3Client(bucketRegion);
             Console.WriteLine("Creating a bucket...");
             await CreateBucketAsync(bucketName);
             Console.WriteLine("Uploading an object...");
             await UploadObjectAsync(bucketName, "example-key", "Hello, S3!");

        }
        private static async Task CreateBucketAsync(string bucketName)
        {
            try   
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };
                
                var response = await s3Client.PutBucketAsync(putBucketRequest);
                Console.WriteLine($"Bucket created: {response.BucketName}");
            }
             catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when creating bucket");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when creating bucket");
            }
        }
        private static Task UploadObjectAsync(string bucketName, string keyName, string content)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = content
                };
                var response = await s3Client.PutObjectAsync(putRequest);
                Console.WriteLine($"Object uploaded: {response.ETag}");

                catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when writing an object");
            }
            }
        }
    }
}