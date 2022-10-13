using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Tests.Services
{
    public class TileSystemServiceTests
    {
        private readonly ITileSystemService _tileSystemService;
        public TileSystemServiceTests()
        {
            _tileSystemService = new TileSystemService();
        }
        [Fact]
        public void GetQuadkey_ReturnsEquality() {

            const string expectedQuadKey = "023112133033";
            const double latitude = 33.5524951d;
            const double longitude = -94.5822016;
            const int levelDetail = 12;           

            var actualResult = _tileSystemService.GetQuadKey(latitude, longitude, levelDetail);

            Assert.Equal(expectedQuadKey, actualResult);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(24)]
        public void GetQuadKey_ThrowsArgumentException_InvalidParameters(int levelDetail)
        {         

            Assert.Throws<ArgumentException>(() => _tileSystemService.GetQuadKey(1d, 1d, levelDetail));
          
        }

    }
}
