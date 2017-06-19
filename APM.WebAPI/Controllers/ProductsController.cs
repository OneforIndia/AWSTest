using SMSWebAPI.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SMSWebAPI.Controllers
{
	[EnableCorsAttribute("http://localhost:64293", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
		public IEnumerable<Product> Get()
		{
			var productRepository = new ProductRepository();

			return productRepository.Retrieve();
		}

		// GET: api/Products/5
		public string Get(int id)
		{
			return "value";
		}
    }
}
