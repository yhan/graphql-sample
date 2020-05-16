namespace GraphQLPlayground.Controllers
{
    using GraphQL;

    public class GraphQlQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public GraphQL.Inputs Variables { get; set; }
    }
}