//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Invictus.Core
//{
//    public static class HttpExtensions
//    {
//        public static IHttpClientBuilder AllowSelfSignedCertificate(this IHttpClientBuilder builder)
//        {
//            if (builder == null)
//            {
//                throw new ArgumentNullException(nameof(builder));
//            }

//            return builder.ConfigureHttpMessageHandlerBuilder(b =>
//            {
//                b.PrimaryHandler =
//                    new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
//            });
//        }
//    }
//}
