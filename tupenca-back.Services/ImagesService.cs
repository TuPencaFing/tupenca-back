using System;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace tupenca_back.Services
{
    public class ImagesService
    {

        private readonly IConfiguration _configuration;
        private const string IMAGES_CONTAINER = "images";
        private const string IMAGES_CONNECTION = "ImagesConnection";

        public ImagesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string uploadImage(string ImageName, Stream content)
        {
            BlobContainerClient blobContainerClient = GetBlobContainerClient(IMAGES_CONTAINER);
            BlobClient blobClient = blobContainerClient.GetBlobClient(ImageName);

            blobClient.Upload(content, overwrite: true);

            return blobClient.Uri.AbsoluteUri;
        }


        private BlobServiceClient GetBlobServiceClient()
        {
            string ImagesConnectionString = _configuration.GetConnectionString(IMAGES_CONNECTION);
            return new BlobServiceClient(ImagesConnectionString);
        }

        private BlobContainerClient GetBlobContainerClient(string containerName)
        {
            BlobServiceClient blobServiceClient = GetBlobServiceClient();
            return blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}

