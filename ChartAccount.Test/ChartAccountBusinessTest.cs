//using Duffnization.CRUD;
//using Duffnization.CRUD.Domains;
//using Duffnization.Spotify;
//using Duffnization.Spotify.Domains;

using ChartAccountBusiness.Interfaces;
using ChartAccountRepository;
using ChartAccountTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChartAccount.Test
{
    public class ChartAccountBusinessTest : BaseTest
    {
        private readonly MainDbContext _dbContext;
        private readonly IChartAccountBusiness _chartAccountBusiness;
        private readonly IGenericRepository<ChartAccountDomain.ChartAccount> _repository;


        public ChartAccountBusinessTest() : base()
        {
            _dbContext = new MainDbContext(_configuration.GetConnectionString("Default"));

            _repository = new GenericRepository<ChartAccountDomain.ChartAccount>(_dbContext);
            _chartAccountBusiness = new ChartAccountBusiness.ChartAccountBusiness(_repository);
        }

        [SetUp]
        public void Setup()
        {

            var x = 0;
            

            
            //_duffnizationCRUDService = new DuffnizationCRUDService(new DuffnizationCRUDConfig
            //{
            //    Username = "admin",
            //    Password = "admin",
            //    BaseApiUrl = "http://localhost:8000"
            //});
        }

        [Test]
        [TestCase("1.998.999.999")]
        public async Task GetNextCode(string parent)
        {
            var newCode = _chartAccountBusiness.GetNextCode(parent);

            Assert.IsNotNull(newCode);
        }

        [Test]
        public async Task GetById()
        {
            var response = _chartAccountBusiness.GetById(1);

            Assert.IsNotNull(response);
        }
    }
}