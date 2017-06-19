using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMSWebAPI.Models;
using System.Web.Http.Cors;

namespace SMS.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:64293", "*", "*")]
    public class MyAWSS3Controller : ApiController
    {
        public IEnumerable<AWSS3> Get()
        {
            var productRepository = new AWSRepository();

            return productRepository.RetrieveS3();
        }
    }
}
