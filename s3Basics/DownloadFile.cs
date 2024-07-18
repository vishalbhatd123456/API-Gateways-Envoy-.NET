//download a file using an endpoint in s3

public DownloadFile
{
    //note that this is a GET
    [HttpGet("download/{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var bucketName = "your-bucket-name";
        try
        {
             var request = new GetObjectRequest
                {
                BucketName = bucketName,
                Key = fileName
                };
            using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
            using (Stream responseStream = response.ResponseStream)
            {
            var contentType = response.Headers["Content-Type"];
            return File(responseStream, contentType, fileName);
            }
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
    
}