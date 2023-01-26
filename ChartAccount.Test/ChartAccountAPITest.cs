//using Duffnization.CRUD;
//using Duffnization.CRUD.Domains;
//using Duffnization.Spotify;
//using Duffnization.Spotify.Domains;

namespace ChartAccount.Test
{
    public class ChartAccountAPITest
    {
        //        private IDuffnizationCRUDService _duffnizationCRUDService;

        [SetUp]
        public void Setup()
        {
            //_duffnizationCRUDService = new DuffnizationCRUDService(new DuffnizationCRUDConfig
            //{
            //    Username = "admin",
            //    Password = "admin",
            //    BaseApiUrl = "http://localhost:8000"
            //});
        }

        [Test]
        public async Task GetTokenAsync()
        {
            var x = 1;

            Assert.IsTrue(x == 1);
        }

        //        [Test]
        //        public async Task ListAll()
        //        {
        //            var response = await _duffnizationCRUDService.ListAll();

        //            Assert.IsNotNull(response);
        //            Assert.IsTrue(response.Count > 0);
        //        }
    }
}