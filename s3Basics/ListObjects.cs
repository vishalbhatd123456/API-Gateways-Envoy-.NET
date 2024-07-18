private static async Task ListObjectsAsync(string bucketName)
{
    try
    {
        var request = new ListObjectsV2Request
        {
            BucketName = bucketName
        };

         ListObjectsV2Response response;
         do
        {
            response = await s3Client.ListObjectsV2Async(request);
            foreach (S3Object entry in response.S3Objects)
            {
                Console.WriteLine($"Key: {entry.Key}, Size: {entry.Size}");
            }

            request.ContinuationToken = response.NextContinuationToken;
        } while (response.IsTruncated);
    }
    catch (AmazonS3Exception e)
    {
        Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when listing objects");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when listing objects");
    }

}
