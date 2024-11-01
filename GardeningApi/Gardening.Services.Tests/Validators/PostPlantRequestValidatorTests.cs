using FluentAssertions;
using Gardening.Services.DTOs.Plant.Post;

namespace Gardening.Services.Tests.Validators
{
    public class PostPlantRequestValidatorTests
    {
        private readonly PostPlantRequestValidator _sut;
        private PostPlantRequest _baseRequest;

        public PostPlantRequestValidatorTests()
        {
            _sut = new PostPlantRequestValidator();
            _baseRequest = new PostPlantRequest()
            {
                Name = "Mint",
                Specie = "Herb"
            }; 
        }

        [Fact]
        public void Validate_ShouldPass_ProperObject()
        {
            var result = _sut.Validate(_baseRequest);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_ShoulldFail_ObjectWithIncorectName(string name)
        {
            var testRequest = _baseRequest with { Name = name };

            var result = _sut.Validate(testRequest);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == nameof(PostPlantRequest.Name));
        }
    }
}
