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
            _tileSystemService = Substitute.For<ITileSystemService>();
        }
        [Fact]
        public void GetQuadkey_ReturnsEquality() {

            const string expectedQuadKey = "122221112203";
            const double latitude = 10d;
            const double longtitude = 10d;
            const int levelDetail = 12;           

            _tileSystemService.GetQuadKey(latitude, longtitude, levelDetail);
            _tileSystemService.Received().GetQuadKey(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<int>());
            _tileSystemService.Received().GetQuadKey(Arg.Is<double>(x=> x == 10d), Arg.Is<double>(x => x == 10d), Arg.Is<int>(x => x == 12));

            _tileSystemService.GetQuadKey(latitude, longtitude, levelDetail).Returns(expectedQuadKey);
            Assert.Equal(expectedQuadKey, _tileSystemService.GetQuadKey(latitude, longtitude, levelDetail));

        }
    }
}
