using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLPlayground.Entities;
using GraphQLPlayground.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLPlayground.Controllers
{
    [Route(Startup.GraphQlPath)]
    public class GraphQlController : Controller
    {
        readonly BlogService blogService;
        public GraphQlController(BlogService blogService)
        {
            this.blogService = blogService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            var schema = new Schema { Query = new AuthorQuery(blogService) };
            var result = await new DocumentExecuter().ExecuteAsync(x =>
            {
                x.Schema = schema;
                x.Query = query.Query;
                x.Inputs = query.Variables;
            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors.First().Message);
            }
            return Ok(result);
        }
    }
}