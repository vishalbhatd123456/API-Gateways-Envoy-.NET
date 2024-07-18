

[HttpPost("upload")]
public async Task<IActionResult> UploadFile(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        return BadRequest("File not selected");
    }

    var bucketName = "your-bucket-name";
    var keyName = file.FileName;

    try
    {
        using (var stream = file.OpenReadStream())
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = keyName,
                InputStream = stream,
                ContentType = file.ContentType
            };

            var response = await s3Client.PutObjectAsync(request);
            return Ok($"File uploaded successfully: {response.ETag}");
        }
    }
    catch (AmazonS3Exception e)
    {
        return StatusCode(500, $"Error encountered on server. Message:'{e.Message}'");
    }
    catch (Exception e)
    {
        return StatusCode(500, $"Unknown encountered on server. Message:'{e.Message}'");
    }
}