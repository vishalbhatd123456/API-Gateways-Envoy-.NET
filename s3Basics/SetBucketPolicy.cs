private static async Task SetBucketPolicyAsync(string bucketName, string policyJson)
{
     try
     {
        var request = new PutBucketPolicyRequest
        {
            BucketName = bucketName,
            Policy = policyJson
        };
        await s3Client.PutBucketPolicyAsync(request);
        Console.WriteLine("Bucket policy set successfully.");
     }
     catch (AmazonS3Exception e)
    {
        Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when setting bucket policy");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when setting bucket policy");
    }
}