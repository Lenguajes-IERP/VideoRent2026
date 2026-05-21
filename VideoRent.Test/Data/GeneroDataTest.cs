
using VideoRent.Data;
using VideoRent.Domain;

namespace VideoRent.Test.Data
{
    [TestFixture]
    public class GeneroDataTest
    {
        private string _testConnectionString;

        [SetUp]
        public void Setup()
        {
            _testConnectionString = "Data Source=AMENA\\SQLEXPRESS;User ID=rentinguser;Password=rentinguser;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        }

     
       [Test]
        public async Task GetGeneros_ReturnsSortedList_WhenDataExists()
        {
            // Arrange
            var generoData = new GeneroData(_testConnectionString);
            // Act
             IEnumerable<Genero> generos =  await generoData.GetGeneros();
            // Assert
            Assert.That(generos.Count() >= 1);
        }
    }
}

