using BookManager.Business.Services;
using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Model.Books;
using BookManager.Domain.Resources;
using BookManager.UnitTest.Mocks;
using BookManager.UnitTest.Utils.Application;
using FluentAssertions;
using IdentityModel;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq.Expressions;

namespace BookManager.UnitTest.Application
{
    public class BookServiceTest : ServiceBaseTest<BookService, Book>
    {
        private readonly Mock<IBookRepository> _bookRepository = new();


        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_bookRepository.Object);
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess_WhenBookIsValid()
        {
            // Arrange
            var bookCreate = BookMock.BuildBookCreate();

            _bookRepository.Setup(r => r.CreateAsync(It.IsAny<Book>())).ReturnsAsync(new Book());

            // Act
            var result = await _service.CreateAsync(bookCreate);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();

            _bookRepository.Verify(r => r.CreateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenCreateFailure()
        {
            // Arrange
            var bookCreate = BookMock.BuildBookCreate();

            _bookRepository.Setup(r => r.CreateAsync(It.IsAny<Book>())).ReturnsAsync(new Book() { Id = Guid.Empty });

            var expectedError = new Error(Issues.e1006, Messages.CreateBookFailure);

            // Act
            var result = await _service.CreateAsync(bookCreate);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And
                .Contain(e => e.Issue == expectedError.Issue && e.Message == expectedError.Message);

            _bookRepository.Verify(r => r.CreateAsync(It.IsAny<Book>()), Times.Once);
        }

        public static IEnumerable<object[]> InvalidBookCreateData =>
            [
                [new BookCreate { Title = "", Autor = "Author", ISBN = "123", ReleaseYear = 2024, Value = 10.0m }, "Title"],
                [new BookCreate { Title = "Test", Autor = "", ISBN = "123", ReleaseYear = 2024, Value = 10.0m }, "Autor"],
                [new BookCreate { Title = "Test", Autor = "Author", ISBN = "", ReleaseYear = 2024, Value = 10.0m }, "ISBN"],
                [new BookCreate { Title = "Test", Autor = "Author", ISBN = "123", ReleaseYear = 0, Value = 10.0m }, "Release Year"],
                [new BookCreate { Title = "Test", Autor = "Author", ISBN = "123", ReleaseYear = 2024, Value = 0.0m }, "Value"],
            ];

        [Theory]
        [MemberData(nameof(InvalidBookCreateData))]
        public async Task CreateAsync_ShouldReturnError_ValidationIsInvalid(BookCreate bookCreate, string expectedErrorProperty)
        {
            // Act
            var result = await _service.CreateAsync(bookCreate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And
                .Contain(e => e.Message.Contains(expectedErrorProperty, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenListEmpty()
        {
            // Arrange
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenNotEmpty()
        {
            // Arrange
            var books = BookMock.GetMockList();

            _bookRepository.Setup(x => x.Query(It.IsAny<Expression<Func<Book, bool>>>()))
                .Returns(books.AsQueryable());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeEmpty();
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccess_WhenBookExists()
        {
            // Arrange
            var book = BookMock.GetMock();
            _bookRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(book);

            // Act
            var result = await _service.GetByIdAsync(book.Id);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().Be(book.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnError_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _bookRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync((Book?)null);

            // Act
            var result = await _service.GetByIdAsync(bookId);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Errors.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenBookIsValid()
        {
            // Arrange
            var bookUpdate = BookMock.BuildBookUpdate();
            var existingBook = BookMock.GetMock();
            _bookRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(existingBook);
            _bookRepository.Setup(r => r.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(bookUpdate);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            _bookRepository.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()), Times.Once);
            _bookRepository.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenBookDoesNotExist()
        {
            // Arrange
            var bookUpdate = BookMock.BuildBookUpdate();
            _bookRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync((Book?)null);

            // Act
            var result = await _service.UpdateAsync(bookUpdate);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            _bookRepository.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenUpdateFails()
        {
            // Arrange
            var bookUpdate = BookMock.BuildBookUpdate();
            var existingBook = BookMock.GetMock();
            _bookRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .ReturnsAsync(existingBook);
            _bookRepository.Setup(r => r.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(false);

            // Act
            var result = await _service.UpdateAsync(bookUpdate);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            _bookRepository.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Once);
        }
        public static IEnumerable<object[]> InvalidBookUpdateData =>
            [
                [new BookUpdate { Id = Guid.NewGuid(), Title = "", Autor = "Author", ISBN = "123", ReleaseYear = 2024, Value = 10.0m }, "Title"],
                [new BookUpdate { Id = Guid.NewGuid(), Title = "Test", Autor = "", ISBN = "123", ReleaseYear = 2024, Value = 10.0m }, "Autor"],
                [new BookUpdate { Id = Guid.NewGuid(), Title = "Test", Autor = "Author", ISBN = "", ReleaseYear = 2024, Value = 10.0m }, "ISBN"],
                [new BookUpdate { Id = Guid.NewGuid(), Title = "Test", Autor = "Author", ISBN = "123", ReleaseYear = 0, Value = 10.0m }, "Release Year"],
                [new BookUpdate { Id = Guid.NewGuid(), Title = "Test", Autor = "Author", ISBN = "123", ReleaseYear = 2024, Value = 0.0m }, "Value"],
            ];
        [Theory]
        [MemberData(nameof(InvalidBookUpdateData))]
        public async Task UpdateAsync_ShouldReturnError_ValidationIsInvalid(BookUpdate bookUpdate, string expectedErrorProperty)
        {
            // Act
            var result = await _service.UpdateAsync(bookUpdate);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And
                .Contain(e => e.Message.Contains(expectedErrorProperty, StringComparison.OrdinalIgnoreCase));
        }

    }
}
