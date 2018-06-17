using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartHome.Dto;
using SmartHome.Model;
using SmartHome.Persistence;
using SmartHome.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace SmartHome.ControllerTest
{
    public class MasterUnitControllerTest
    {
        private Mock<IRepository<MasterUnit>> _masterUnitRepoMock;
        private IRepositoryService _repositoryService;
        private Mock<UserManager<User>> _mockUserManager;
        private MasterUnitManagerController _controller;

        public MasterUnitControllerTest()
        {
            _masterUnitRepoMock = new Mock<IRepository<MasterUnit>>();
            _repositoryService = new RepositoryServiceMock(null, null, null, _masterUnitRepoMock.Object);
            _mockUserManager = MockUserManager<User>();
            _controller = new MasterUnitManagerController(_repositoryService, _mockUserManager.Object);
            _controller.ControllerContext = new ControllerContext();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.Name, "TestUser")
            }));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        #region Create

        [Fact]
        public async Task Create_Creates_Master_Unit_With_Correct_Parameters()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();
            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = Guid.NewGuid(),
                IsOn = false,
                OwnerId = user.Id
            };

            _mockUserManager
                .Setup(set => set.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.AddAsync(It.IsAny<MasterUnit>()))
                .ReturnsAsync(true);

            //  ACT
            var result = await _controller.Create(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByIdAsync(id.ToString()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.AddAsync(It.IsAny<MasterUnit>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            CreatedResult castedResult = result as CreatedResult;
            Assert.IsType<MasterUnitDto>(castedResult.Value);
            Assert.Equal((MasterUnitDto)castedResult.Value, dto);
            Assert.Contains($"api/v1/masterunit/{dto.Id}", castedResult.Location);
        }

        [Fact]
        public async Task Create_Forbids_If_USer_Cannot_Be_found()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = Guid.NewGuid(),
                IsOn = false,
                OwnerId = id
            };

            _mockUserManager
                .Setup(set => set.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            _masterUnitRepoMock
                .Setup(set => set.AddAsync(It.IsAny<MasterUnit>()))
                .ReturnsAsync(true);

            //  ACT
            var result = await _controller.Create(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByIdAsync(id.ToString()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.AddAsync(It.IsAny<MasterUnit>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task Create_Fails_When_Data_Saving_Fails()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();
            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = Guid.NewGuid(),
                IsOn = false,
                OwnerId = user.Id
            };

            _mockUserManager
                .Setup(set => set.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.AddAsync(It.IsAny<MasterUnit>()))
                .ReturnsAsync(false);

            //  ACT
            var result = await _controller.Create(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByIdAsync(id.ToString()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.AddAsync(It.IsAny<MasterUnit>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion

        #region Read

        [Fact]
        public async Task Read_Succeeds_When_element_Is_present()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var masterUnit = new MasterUnit
            {
                CustomName = "customName",
                ConcurrencyLock = Guid.NewGuid(),
                IsOn = false,
                UserId = id,
                User = user
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnit);

            //  ACT
            var result = await _controller.Get(id);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            JsonResult castedResult = result as JsonResult;
            Assert.IsType<MasterUnitDto>(castedResult.Value);
            //Assert.Equal((int)HttpStatusCode.OK, castedResult.StatusCode);
        }

        [Fact]
        public async Task Read_Forbids_When_Current_User_Not_Present()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var masterUnit = new MasterUnit
            {
                CustomName = "customName",
                ConcurrencyLock = Guid.NewGuid(),
                IsOn = false,
                UserId = id,
                User = user
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnit);

            //  ACT
            var result = await _controller.Get(id);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task Read_Fails_When_Element_Is_Not_present()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(default(MasterUnit));

            //  ACT
            var result = await _controller.Get(id);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ReadAll_Succeeds_When_element_Is_present()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var masterUnitList = new List<MasterUnit>
            {
                new MasterUnit
                {
                    CustomName = "customName",
                    ConcurrencyLock = Guid.NewGuid(),
                    IsOn = false,
                    UserId = id,
                    User = user
                }
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnitList);

            //  ACT
            var result = await _controller.GetAll();

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            JsonResult castedResult = result as JsonResult;
            Assert.IsAssignableFrom<IEnumerable<MasterUnitDto>>(castedResult.Value);
            //Assert.Equal((int)HttpStatusCode.OK, castedResult.StatusCode);
        }

        [Fact]
        public async Task ReadAll_Forbids_When_Current_User_Not_Present()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var masterUnitList = new List<MasterUnit>
            {
                new MasterUnit
                {
                    CustomName = "customName",
                    ConcurrencyLock = Guid.NewGuid(),
                    IsOn = false,
                    UserId = id,
                    User = user
                }
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            _masterUnitRepoMock
                .Setup(set => set.FindListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnitList);

            //  ACT
            var result = await _controller.GetAll();

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task ReadAll_Fails_When_Element_Is_Not_present()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(default(List<MasterUnit>));

            //  ACT
            var result = await _controller.GetAll();

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_Succeeds_Whith_Correct_Data()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser",
            };

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = id,
                IsOn = false,
                OwnerId = user.Id
            };

            var masterUnit = new MasterUnit
            {
                CustomName = "customName",
                ConcurrencyLock = id,
                IsOn = false,
                UserId = user.Id,
                User = user
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnit);

            //  ACT
            var result = await _controller.Update(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_Fails_On_Concurrency()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = id,
                UserName = "TestUser",
            };

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = Guid.NewGuid(),
                IsOn = false,
                OwnerId = user.Id
            };

            var masterUnit = new MasterUnit
            {
                CustomName = "customName",
                ConcurrencyLock = Guid.NewGuid(),
                IsOn = false,
                UserId = user.Id,
                User = user
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnit);

            //  ACT
            var result = await _controller.Update(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            BadRequestObjectResult castedResult = (BadRequestObjectResult)result;
            Assert.IsAssignableFrom<IEnumerable<MasterUnitDto>>(castedResult.Value);
            Assert.Equal(2, ((List<MasterUnitDto>)castedResult.Value).Count);
        }

        [Fact]
        public async Task Update_Forbids_Without_Correct_User()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser",
            };

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = id,
                IsOn = false,
                OwnerId = user.Id
            };

            var masterUnit = new MasterUnit
            {
                CustomName = "customName",
                ConcurrencyLock = id,
                IsOn = false,
                UserId = user.Id,
                User = user
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(masterUnit);

            //  ACT
            var result = await _controller.Update(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task Update_Fails_With_Incorrect_Data_Reference()
        {
            //ARRANGE
            Guid id = Guid.NewGuid();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser",
            };

            var dto = new MasterUnitDto
            {
                CustomName = "customName",
                eTag = id,
                IsOn = false,
                OwnerId = user.Id
            };

            var masterUnit = new MasterUnit
            {
                CustomName = "customName",
                ConcurrencyLock = id,
                IsOn = false,
                UserId = user.Id,
                User = user
            };

            _mockUserManager
                .Setup(set => set.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _masterUnitRepoMock
                .Setup(set => set.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()))
                .ReturnsAsync(default(MasterUnit));

            //  ACT
            var result = await _controller.Update(dto);

            //ASSERT

            _mockUserManager.Verify(ver => ver.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _masterUnitRepoMock.Verify(ver => ver.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<MasterUnit, bool>>>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Delete

        #endregion
    }
}
