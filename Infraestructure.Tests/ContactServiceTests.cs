using Domain.Exceptions;
using Domain.Request;
using Infraestructure.Abstractions;
using Infraestructure.Data.Abstractions;
using Infraestructure.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Infraestructure.Tests
{
    public class ContactServiceTests
    {
        private Mock<IContactRepository> _contactRepository = new Mock<IContactRepository>();
        private Mock<IBlobStorageService> _blobStorageService = new Mock<IBlobStorageService>();
        private ContactService _subject;

        public ContactServiceTests()
        {
            _subject = new ContactService(_contactRepository.Object, _blobStorageService.Object);
        }

        [Fact]
        public async Task Create_Ok_WithoutImg()
        {
            // Arrange
            var contact = new Domain.Contact
            {
                Name = "Nombre",
                Email = "nombre@algo.com",
                Birthdate = "11/02/1990"
            };

            _contactRepository
                .Setup(x => x.Add(contact))
                .ReturnsAsync(contact);

            // Act
            var result = await _subject.Create(contact);

            // Assert
            _blobStorageService.Verify(x => x.UploadFile(It.IsAny<UploadFileRequest>()), Times.Never);
            _contactRepository.Verify(x => x.Add(It.IsAny<Domain.Contact>()), Times.Once);
        }

        [Fact]
        public async Task Create_Ok_WithImg()
        {
            // Arrange
            var contact = new Domain.Contact
            {
                Name = "Nombre",
                Email = "nombre@algo.com",
                Birthdate = "11/02/1990",
                ImageAsBase64 = "asd"
            };

            _contactRepository
                .Setup(x => x.Add(contact))
                .ReturnsAsync(contact);

            // Act
            var result = await _subject.Create(contact);

            // Assert
            _blobStorageService.Verify(x => x.UploadFile(It.IsAny<UploadFileRequest>()), Times.Once);
            _contactRepository.Verify(x => x.Add(It.IsAny<Domain.Contact>()), Times.Once);
        }

        [Fact]
        public async Task Create_Fail()
        {
            // Arrange
            var contact = new Domain.Contact
            {
                Name = "Nombre",
                Email = "nombre@algo.com",
                Birthdate = "02/1990",
                ImageAsBase64 = "asd"
            };

            _contactRepository
                .Setup(x => x.Add(contact))
                .ReturnsAsync(contact);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _subject.Create(contact));
        }
    }
}