var putRequest = new PutObjectRequest
{
    BucketName = bucketName,
    Key = keyName,
    ContentBody = "Sample text",
    ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
};

var response = await s3Client.PutObjectAsync(putRequest);
Console.WriteLine($"Object uploaded with encryption: {response.ETag}");