using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using BusinessEntities;
using Core.Services.Users;
using Microsoft.Ajax.Utilities;
using WebApi.Models;
using WebApi.Models.Errors;
using WebApi.Models.Users;
using WebApi.Utility;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Guid userId, [FromBody] UserModel model)
        {
            ApiResponse<UserData> response = new ApiResponse<UserData>();
            try
            {
                var user = _getUserService.GetUser(userId);
                if (user != null)
                {
                    response.Success = false;
                    response.ApplicationErrors = new List<ApplicationError>();
                    var error = CommonUtility.GetApplicationError(FaultCodes.LP004, "User with this id already exists.");
                    response.ApplicationErrors.Add(error);
                    return Found(response);
                }

                var newUser = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                response.Success = true;
                response.Data = new UserData(newUser);
            }
            catch (Common.Exceptions.ValidationException validationEx)
            {
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP003, validationEx.Message);
                response.ApplicationErrors.Add(error);
            }
            //catch (Exception ex)
            //{
            //    // Log the exception.
            //    string str = ex.Message;

            //    response.Success = false;
            //    response.SystemErrors = new List<SystemError>();
            //    var error = CommonUtility.GetSystemError();
            //    response.SystemErrors.Add(error);
            //}

            return Found(response);
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            ApiResponse<UserData> response = new ApiResponse<UserData>();
            try
            {
                var user = _getUserService.GetUser(userId);
                if (user == null)
                {
                    response.Success = false;
                    response.ApplicationErrors = new List<ApplicationError>();
                    var error = CommonUtility.GetApplicationError(FaultCodes.LP002, "User not found.");
                    response.ApplicationErrors.Add(error);
                    return Found(response);
                }

                _updateUserService.Update(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                response.Success = true;
                response.Data = new UserData(user);
            }
            catch (Common.Exceptions.ValidationException validationEx)
            {
                response.Success = false;
                response.ApplicationErrors = new List<ApplicationError>();
                var error = CommonUtility.GetApplicationError(FaultCodes.LP003, validationEx.Message);
                response.ApplicationErrors.Add(error);
            }
            
            return Found(response);
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            _deleteUserService.Delete(user);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            return Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            var users = _getUserService.GetUsers(type, name, email)
                                       .Skip(skip).Take(take)
                                       .Select(q => new UserData(q))
                                       .ToList();
            return Found(users);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllUsers()
        {
            _deleteUserService.DeleteAll();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetUsersByTag(string tag)
        {
            ApiResponse<IList<UserData>> response = new ApiResponse<IList<UserData>>();
            var users = _getUserService.GetUsers(null, null, null, tag)
                                           .Select(q => new UserData(q))
                                           .ToList();
            response.Success = true;
            response.Data = users;            
            return Found(response);
        }
    }
}