using Microsoft.Extensions.Configuration;

namespace Connection
{
    public class ClsConnection
    {
        private readonly IConfiguration _configuration;
        public ClsConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("SqlCon");
        }
    }
}
