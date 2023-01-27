using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartAccountTest
{
   
    public abstract class BaseTest
    {
        protected IConfiguration _configuration;
        public BaseTest()
        {
            _configuration = InitConfiguration();
        }

        public IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }


    }
}
