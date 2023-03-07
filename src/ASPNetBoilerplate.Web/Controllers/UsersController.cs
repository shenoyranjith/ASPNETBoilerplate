using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Domain.Enumerations;
using ASPNetBoilerplate.Web.Models.ClientModels.User;
using ASPNetBoilerplate.Web.Models.ResponseModels;
using ASPNetBoilerplate.Web.Models.ResponseModels.User;
using ASPNetBoilerplate.Web.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetBoilerplate.Web.Controllers
{
    /// <summary>
    /// Controller for user
    /// </summary>
    /// <seealso cref="ASPNetBoilerplate.Web.Controllers.BaseController" />
    [Route("v{version:apiVersion}/[controller]/")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            _userService.Validate(
                    createUserRequest.Username,
                    createUserRequest.FirstName,
                    createUserRequest.EmailAddress,
                    createUserRequest.Password
             );
            var user = _userService.Add(createUserRequest, UserRole.User);
            var userResponse = MapUserToResponse(user);
            return new JsonResult(new ResponseModel() { Data = userResponse });
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="userRequest">The user request.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers([FromQuery] UserRequest userRequest)
        {
            var users = _userService.GetAll(userRequest);
            var count = _userService.Count(userRequest.Filter);
            var userResponses = users.ToList().Select(MapUserToResponse);
            return new JsonResult(new ResponseModel()
            {
                Data = userResponses,
                TotalCount = count
            });
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.Get(id);
            var userResponse = MapUserToResponse(user);
            return new JsonResult(new ResponseModel() { Data = userResponse });
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateUserRequest">The update user request.</param>
        /// <returns></returns>
        [HttpPatch("/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            var user = _userService.Update(id, updateUserRequest);
            var userResponse = MapUserToResponse(user);
            return new JsonResult(new ResponseModel() { Data = userResponse });
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpDelete("/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.Delete(id);
            var userResponse = MapUserToResponse(user);
            return new JsonResult(new ResponseModel() { Data = userResponse });
        }

        /// <summary>
        /// Creates the admin.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPost("/admin")]
        public IActionResult CreateAdmin([FromBody] CreateUserRequest createUserRequest)
        {
            _userService.Validate(
                    createUserRequest.Username,
                    createUserRequest.FirstName,
                    createUserRequest.EmailAddress,
                    createUserRequest.Password
             );
            var user = _userService.Add(createUserRequest, UserRole.Admin);
            var userResponse = MapUserToResponse(user);
            return new JsonResult(new ResponseModel() { Data = userResponse });
        }

        /// <summary>
        /// Maps the user to response.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The user response model</returns>
        private UserResponse MapUserToResponse(User user)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserResponse>()));
            var userResponse = new UserResponse();
            mapper.Map(user, userResponse);
            return userResponse;
        }
    }
}
