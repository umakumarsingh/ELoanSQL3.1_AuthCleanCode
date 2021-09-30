using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services
{
    public class LoanAdminServices : ILoanAdminServices
    {
        /// <summary>
        /// Creating instance/field of ILoanCustomerRepository and injecting into LoanCustomerSevices Constructor
        /// </summary>
        private readonly ILoanAdminRepository _adminRepository;
        public LoanAdminServices(ILoanAdminRepository loanAdminRepository)
        {
            _adminRepository = loanAdminRepository;
        }
        /// <summary>
        /// Create a new role if role is exists, throw a erroe message, role allready exixts
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Disable an existing user if not required to work, and provide userId as GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DisableUser(string userId)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Edit/Update an existing role if required using below method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditRole(EditRoleViewModel model)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Edit register user or change user password only, UserName/Name and Email are not change and must provide
        /// Change user Password applicable using below method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangeUserPassword(ChangePasswordViewModel model)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Provide different role for registered User
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditUsersInRole(UserRoleViewModel model, string roleId)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Enable an existing user user userId must be supplied GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EnableUser(string userId)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Find user by user emailId
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserMaster> FindByEmailAsync(string email)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Find an existing role by role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRoleByRoleId(string roleId)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Find an existing role by role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRoleByRoleName(string roleName)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// List all user role from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityRole>> ListAllRole()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// List all user from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserMaster>> ListAllUser()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Find user by user id, userId must as GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserMaster> FindUserByIdAsync(string userId)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Register a new user with default user role is customer
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Register(UserMaster user, string password)
        {
            //do code here
            throw new NotImplementedException();
        }
    }
}
