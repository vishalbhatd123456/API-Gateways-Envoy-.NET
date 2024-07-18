using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Util;

public class Function
{
    private static readonly IAmazonS3 s3Client = new AmazonS3Client();
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        var s3Event = evnt.Records?.FirstOrDefault()?.S3;
        if (s3Event == null)
        {
            return;
        }

        var bucketName = s3Event.Bucket.Name;
        var key = s3Event.Object.Key;

        try
        {
            var response = await s3Client.GetObjectMetadataAsync(bucketName, key);
            Console.WriteLine($"Object {key} retrieved with size {response.ContentLength} bytes.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error retrieving object {key} from bucket {bucketName}. Error: {e.Message}");
            throw;
        }
    }
}