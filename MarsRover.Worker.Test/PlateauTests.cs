using MarsRover.Contracts.Services;
using MarsRover.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MarsRover.Worker.Test
{
    public class PlateauTests
    {
        private static IPlateauService _plateauService;

        public PlateauTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPlateauService, PlateauService>()
                .BuildServiceProvider();

            _plateauService = serviceProvider.GetService<IPlateauService>();
        }

        [Fact]
        public void ShouldCreatePlateau()
        {
            var coordinateString = "5 5";

            var plateau = _plateauService.CreatePlateau(coordinateString);

            Assert.NotNull(plateau);
        }

        [Fact]
        public void ShouldNotCreatePlateau()
        {
            var coordinateString = "44";

            var plateau = _plateauService.CreatePlateau(coordinateString);

            Assert.Null(plateau);
        }
    }
}
