using API_Controllers_UniTests.Common;
using AutoFixture.Kernel;

using HumanRegistrationSystem.Controllers;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API_Controllers_UniTests
{
    public class AdminControllerTests
    {
        private readonly Mock<IUserAccountService> _accountServiceMock;
        private readonly AdminController _sut;
        private readonly ClaimId _claimId;

        public AdminControllerTests()
        {
            _accountServiceMock = new Mock<IUserAccountService>();
            _sut = new AdminController(_accountServiceMock.Object);
            _claimId = new ClaimId(_sut);
        }

        [Fact]
        public async Task DeleteUserById_WhenInputIsZero_ReturnsBedRequest()
        {
            //_accountServiceMock.Setup(x => x.DeleteUser(' ')).ReturnsAsync(false);
            var result = await _sut.DeleteUserById(string.Empty);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.Equal(400, badRequestResult.StatusCode);
        }



        [Fact]
        public async Task Get_WhenIdIsEmpty_ReturnsBadRequest()
        {

            var result = await _sut.GetUserById(string.Empty);
            var resultAsBadRequest = result.Result as BadRequestObjectResult;

            Assert.Equal(400, resultAsBadRequest.StatusCode);
        }


















        //[Fact]
        //public async Task RemoveUserAccount_WhenUserIsAdminAndIdIsAdminId_ReturnsBadRequest()
        //{
        //    _claimId.AdminMockRole();
        //    _accountServiceMock.Setup(x => x.DeleteUser(2))
        //        .ReturnsAsync(new Result<User>());

        //    var result = await _sut.DeleteUserById(2);
        //    var badRequestResult = result as BadRequestObjectResult;

        //    badRequestResult.StatusCode.Should().Be(400);
        //    _accountServiceMock.Verify(x => x.DeleteUser(2), Times.Never);
        //}

        //[Fact]
        //public async Task RemoveUserAccount_WhenUserIsAdminAndIdIsNotAdminId_ReturnsOk()
        //{
        //    _claimId.AdminMockRole();
        //    _accountServiceMock
        //        .Setup(x => x.DeleteUser(1))
        //        .ReturnsAsync(new Result<User> { IsSuccess = true });

        //    var result = await _sut.DeleteUserById(1);
        //    var resultAsOkResult = result as OkObjectResult;

        //    resultAsOkResult.StatusCode.Should().Be(200);
        //    _accountServiceMock.Verify(x => x.DeleteUser(1), Times.Once);
        //}

        //[Fact]
        //public async Task RemoveUserAccount_WhenResultIsNotSuccess_ReturnsNotFound()
        //{
        //    _claimId.AdminMockRole();
        //    _accountServiceMock
        //        .Setup(x => x.DeleteUser(1))
        //        .ReturnsAsync(new Result<User> { IsSuccess = false });

        //    var result = await _sut.DeleteUserById(1);
        //    var resultAsOkResult = result as NotFoundObjectResult;

        //    resultAsOkResult.StatusCode.Should().Be(404);
        //    _accountServiceMock.Verify(x => x.DeleteUser(1), Times.Once);
        //}
    }
}