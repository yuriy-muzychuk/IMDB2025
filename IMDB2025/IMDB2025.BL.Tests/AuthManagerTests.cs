using IMDB2025.BL.Concrete;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;
using Moq;
using NUnit.Framework;

namespace IMDB2025.BL.Tests
{
    [TestFixture]
    public class AuthManagerTests
    {
        private Mock<IUserDal> _userDalMock;
        private Mock<IUserPrivilegeDal> _userPrivilegeDalMock;
        private AuthManager _sut;

        [SetUp]
        public void SetUp()
        {
            _userDalMock = new Mock<IUserDal>(MockBehavior.Strict);
            _userPrivilegeDalMock = new Mock<IUserPrivilegeDal>(MockBehavior.Strict);
            _sut = new AuthManager(_userDalMock.Object, _userPrivilegeDalMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _userDalMock.VerifyAll();
            _userPrivilegeDalMock.VerifyAll();
        }

        [Test]
        public void CreateUser_ShouldCreateAssignPrivilegeAndReturnFullUser()
        {
            // Arrange
            const string email = "user@test.com";
            const string login = "user1";
            const string password = "P@ssw0rd";
            const PrivilegeType privilege = PrivilegeType.User;

            var createdUser = new User
            {
                UserId = 10,
                Email = email,
                Login = login,
                Privileges = new List<Privilege>()
            };

            var finalUser = new User
            {
                UserId = 10,
                Email = email,
                Login = login,
                Privileges = new List<Privilege>
                {
                    new Privilege { PrivilegeId = 1, Name = privilege }
                }
            };

            _userDalMock
                .Setup(d => d.CreateUser(email, login, password))
                .Returns(createdUser);

            _userPrivilegeDalMock
                .Setup(p => p.AddPrivilegeToUser(createdUser.UserId, privilege));

            _userDalMock
                .Setup(d => d.GetUserById(createdUser.UserId))
                .Returns(finalUser);

            // Act
            var result = _sut.CreateUser(email, login, password, privilege);

            // Assert
            Assert.That(result, Is.SameAs(finalUser));
            Assert.That(result.UserId, Is.EqualTo(10));
            Assert.That(result.Privileges, Has.Count.EqualTo(1));

            _userPrivilegeDalMock.Verify(p => p.AddPrivilegeToUser(10, privilege), Times.Once);
        }

        [Test]
        public void CreateUser_ShouldThrow_WhenDalReturnsNull()
        {
            // Arrange
            _userDalMock
                .Setup(d => d.CreateUser("a@b.com", "u", "pwd"))
                .Returns((User)null);

            // Act / Assert
            var ex = Assert.Throws<Exception>(() => _sut.CreateUser("a@b.com", "u", "pwd", PrivilegeType.Admin));
            Assert.That(ex!.Message, Is.EqualTo("User creation failed."));
            _userPrivilegeDalMock.Verify(p => p.AddPrivilegeToUser(It.IsAny<int>(), It.IsAny<PrivilegeType>()), Times.Never);
        }

        [Test]
        public void CreateUser_ShouldThrow_WhenReturnedUserHasNonPositiveId()
        {
            // Arrange
            var invalidUser = new User
            {
                UserId = 0,
                Email = "x@y.com",
                Login = "login",
                Privileges = new List<Privilege>()
            };
            _userDalMock
                .Setup(d => d.CreateUser(invalidUser.Email, invalidUser.Login, "pwd"))
                .Returns(invalidUser);

            // Act / Assert
            var ex = Assert.Throws<Exception>(() => _sut.CreateUser(invalidUser.Email, invalidUser.Login, "pwd", PrivilegeType.User));
            Assert.That(ex!.Message, Is.EqualTo("User creation failed."));
            _userPrivilegeDalMock.Verify(p => p.AddPrivilegeToUser(It.IsAny<int>(), It.IsAny<PrivilegeType>()), Times.Never);
        }

        [Test]
        public void GetUserById_ShouldReturnUserFromDal()
        {
            // Arrange
            var user = new User { UserId = 5, Login = "u5", Email = "u5@test.com", Privileges = new List<Privilege>() };
            _userDalMock.Setup(d => d.GetUserById(5)).Returns(user);

            // Act
            var result = _sut.GetUserById(5);

            // Assert
            Assert.That(result, Is.SameAs(user));
            Assert.That(result.UserId, Is.EqualTo(5));
        }

        [Test]
        public void GetUserByLogin_ShouldReturnUserFromDal()
        {
            // Arrange
            var user = new User { UserId = 12, Login = "login12", Email = "l12@test.com", Privileges = new List<Privilege>() };
            _userDalMock.Setup(d => d.GetUserByLogin("login12")).Returns(user);

            // Act
            var result = _sut.GetUserByLogin("login12");

            // Assert
            Assert.That(result, Is.SameAs(user));
            Assert.That(result.Login, Is.EqualTo("login12"));
        }

        [Test]
        public void GetUsers_ShouldReturnListFromDal()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 1, Login = "a", Email = "a@test.com", Privileges = new List<Privilege>() },
                new User { UserId = 2, Login = "b", Email = "b@test.com", Privileges = new List<Privilege>() }
            };
            _userDalMock.Setup(d => d.GetUsers()).Returns(users);

            // Act
            var result = _sut.GetUsers();

            // Assert
            Assert.That(result, Is.SameAs(users));
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void Login_ShouldDelegateToDal()
        {
            // Arrange
            _userDalMock.Setup(d => d.Login("user", "pwd")).Returns(true);

            // Act
            var success = _sut.Login("user", "pwd");

            // Assert
            Assert.That(success, Is.True);
        }

        [Test]
        public void Login_ShouldReturnFalse_WhenDalReturnsFalse()
        {
            // Arrange
            _userDalMock.Setup(d => d.Login("user", "bad")).Returns(false);

            // Act
            var success = _sut.Login("user", "bad");

            // Assert
            Assert.That(success, Is.False);
        }
    }
}
