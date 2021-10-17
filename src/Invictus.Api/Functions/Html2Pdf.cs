////using DinkToPdf;
////using DinkToPdf.Contracts;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Invictus.Api.Functions
//{
//    public class Html2Pdf
//    {
//        //IPdfConverter pdfConverter = new SynchronizedConverter(new PdfTools());

//        private IConverter _converter;
//        public Html2Pdf(IConverter converter)
//        {
//            _converter = converter;
//        }
//        // The name of the function
//        [FunctionName("Html2Pdf")]
//        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "POST")] Html2PdfRequest Request, ILogger Log)
//        {
//            // PDFByteArray is a byte array of pdf generated from the HtmlContent 
//            var PDFByteArray = BuildPdf(Request.HtmlContent, "8.5in", "11in", new MarginSettings(0, 0, 0, 0));

//            // The connection string of the Storage Account to which our PDF file will be uploaded
//            var StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=<YOUR ACCOUNT NAME>;AccountKey=<YOUR ACCOUNT KEY>;EndpointSuffix=core.windows.net";

//            // Generate an instance of CloudStorageAccount by parsing the connection string
//            var StorageAccount = CloudStorageAccount.Parse(StorageConnectionString);

//            // Create an instance of CloudBlobClient to connect to our storage account
//            CloudBlobClient BlobClient = StorageAccount.CreateCloudBlobClient();

//            // Get the instance of CloudBlobContainer which points to a container name "pdf"
//            // Replace your own container name
//            CloudBlobContainer BlobContainer = BlobClient.GetContainerReference("pdf");

//            // Get the instance of the CloudBlockBlob to which the PDFByteArray will be uploaded
//            CloudBlockBlob Blob = BlobContainer.GetBlockBlobReference(Request.PDFFileName);

//            // Upload the pdf blob
//            await Blob.UploadFromByteArrayAsync(PDFByteArray, 0, PDFByteArray.Length);
//        }

//        private byte[] BuildPdf(string HtmlContent, string Width, string Height, MarginSettings Margins, int? DPI = 180)
//        {
//            // Call the Convert method of SynchronizedConverter "pdfConverter"
//            return _converter.Convert(new HtmlToPdfDocument()
//            {
//                // Set the html content
//                Objects =
//                {
//                    new ObjectSettings
//                    {
//                        HtmlContent = HtmlContent
//                    }
//                },
//                // Set the configurations
//                GlobalSettings = new GlobalSettings
//                {
//                    // PaperKind.A4 can also be used instead PechkinPaperSize
//                    PaperSize = new PechkinPaperSize(Width, Height),
//                    DPI = DPI,
//                    Margins = Margins
//                }
//            });
//        }
//    }

//    public class Html2PdfRequest
//    {
//        // The HTML content that needs to be converted.
//        public string HtmlContent { get; set; }

//        // The name of the PDF file to be generated
//        public string PDFFileName { get; set; }
//    }
//}
