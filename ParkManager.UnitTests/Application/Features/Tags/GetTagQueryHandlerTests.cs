using AutoMapper;
using MediatR;
using Moq;
using ParkManager.Application.Contracts.Persistence;
using ParkManager.Application.Features.Tags.Queries.GetTag;
using ParkManager.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ParkManager.UnitTests
{
    public class GetTagQueryHandlerTests
    {
        private readonly Mock<ITagsRepository> _mockTagsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IRequestHandler<GetTagQuery, GetTagQueryResponse> _handler;

        public GetTagQueryHandlerTests()
        {
            _mockTagsRepository = new Mock<ITagsRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetTagQueryHandler(_mockTagsRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTag_WhenTagExists()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            var tag = new Tag(tagId, 1);
            var expectedResponse = new GetTagQueryResponse { Id = tagId, Number = 1 };
            _mockTagsRepository.Setup(repo => repo.Get(tagId)).ReturnsAsync(tag);
            _mockMapper.Setup(mapper => mapper.Map<GetTagQueryResponse>(tag)).Returns(expectedResponse);
            var query = new GetTagQuery(tagId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.Number, result.Number);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenTagDoesNotExist()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            _mockTagsRepository.Setup(repo => repo.Get(tagId)).ReturnsAsync((Tag)null);
            var query = new GetTagQuery (tagId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
