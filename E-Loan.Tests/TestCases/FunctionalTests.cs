using E_Loan.BusinessLayer;
using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Controllers;
using E_Loan.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace E_Loan.Tests.TestCases
{
    public class FunctionalTests
    {
        /// <summary>
        /// Creating Referance Variable and Mocking repository class
        /// </summary>
        private readonly ITestOutputHelper _output;
        private readonly ILoanAdminServices _adminServices;
        private readonly ILoanCustomerServices _customerServices;
        private readonly ILoanClerkServices _clerkServices;
        private readonly ILoanManagerServices _managerServices;
        public readonly Mock<ILoanAdminRepository> adminservice = new Mock<ILoanAdminRepository>();
        public readonly Mock<ILoanCustomerRepository> customerservice = new Mock<ILoanCustomerRepository>();
        public readonly Mock<ILoanClerkRepository> clerkservice = new Mock<ILoanClerkRepository>();
        public readonly Mock<ILoanManagerRepository> managerservice = new Mock<ILoanManagerRepository>();

        private readonly LoanMaster _loanMaster;
        private readonly UserMaster _userMaster;
        private readonly LoanProcesstrans _loanProcesstrans;
        private readonly LoanApprovaltrans _loanApprovaltrans;
        private readonly CreateRoleViewModel _createRoleViewModel;
        private readonly UserRoleViewModel _userRoleViewModel;
        private readonly ChangePasswordViewModel _changePasswordViewModel;
        private readonly EditRoleViewModel _editRoleViewModel;
        private static string type = "Functional";
        public FunctionalTests(ITestOutputHelper output)
        {
            /// <summary>
            /// Injecting service object into Test class constructor
            /// </summary>
            _customerServices = new LoanCustomerServices(customerservice.Object);
            _clerkServices = new LoanClerkServices(clerkservice.Object);
            _managerServices = new LoanManagerServices(managerservice.Object);
            _adminServices = new LoanAdminServices(adminservice.Object);
            _output = output;
            _loanMaster = new LoanMaster
            {
                LoanId = 1,
                LoanName = "Home Loan",
                Date = System.DateTime.Now,
                BusinessStructure = BusinessStatus.Individual,
                Billing_Indicator = BillingStatus.Not_Salaried_Person,
                Tax_Indicator = TaxStatus.Not_tax_Payer,
                ContactAddress = "Gaya-Bihar",
                Phone = "9632584754",
                Email = "eloan@iiht.com",
                AppliedBy = "Kumar",
                CreatedOn = DateTime.Now,
                ManagerRemark = "Ok",
                Status = LoanStatus.NotReceived
            };
            _userMaster = new UserMaster
            {
                Id = "1aaabedf-2002-4166-801a-ca83aac3247e",
                UserName = "Kundan",
                Email = "umakumarsingh@techademy.com",
                PasswordHash = "Password@1234",
                Contact = "9631438123",
                Address = "Gaya",
                IdproofTypes = IdProofType.Aadhar,
                IdProofNumber = "AYIPK6551B",
                Enabled = false
            };
            _loanProcesstrans = new LoanProcesstrans
            {
                Id = 1,
                AcresofLand = 1,
                LandValueinRs = 4700000,
                AppraisedBy = "Uma",
                ValuationDate = DateTime.Now,
                AddressofProperty = "Mall - Karnataka",
                SuggestedAmount = 4000000,
                ManagerId = 3,
                LoanId = 1
            };
            _loanApprovaltrans = new LoanApprovaltrans
            {
                Id = 1,
                SanctionedAmount = 4000000,
                Termofloan = 72,
                PaymentStartDate = DateTime.Now,
                LoanCloserDate = DateTime.Now,
                MonthlyPayment = 3330000
            };
            _createRoleViewModel = new CreateRoleViewModel
            {
                RoleName = "Admin"
            };
            _userRoleViewModel = new UserRoleViewModel
            {
                UserId = "1b232594-4f44-4777-9008-480746341378",
                Email = "umakumarsingh@gmail.com"
            };
            _changePasswordViewModel = new ChangePasswordViewModel
            {
                Name = "Uma",
                Email = "umakumarsingh@iiht.com",
                Password = "Password@123",
                ConfirmPassword = "Password@123"
            };
            _editRoleViewModel = new EditRoleViewModel
            {
                Id = "7f737659-aa03-4633-ad16-4c1ac83cfe98",
                RoleName = "Admin",
            };

        }
        /// <summary>
        /// This Test is use for test the applied loan application status by LoanId 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AppliedLoanStatusByLoanId()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.AppliedLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
                var result = await _customerServices.AppliedLoanStatus(_loanMaster.LoanId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }

        /// <summary>
        /// Add/Apply a loan/Mortage using this function testing below method or test case.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ApplayMortage()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using this method try to  test mortage is updated or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_UpdateMortgage()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            var _updateLoan = new LoanMaster()
            {
                LoanId = 1,
                LoanName = "Personal-Loan",
                Date = System.DateTime.Now,
                BusinessStructure = BusinessStatus.Individual,
                Billing_Indicator = BillingStatus.Not_Salaried_Person,
                Tax_Indicator = TaxStatus.Not_tax_Payer,
                ContactAddress = "Gaya-Bihar",
                Phone = "9632584754",
                Email = "eloan@iiht.com",
                AppliedBy = "Kumar",
                CreatedOn = DateTime.Now,
                ManagerRemark = "Ok",
                Status = LoanStatus.NotReceived
            };
            //Act
            try
            {
                customerservice.Setup(repo => repo.UpdateMortgage(_updateLoan)).ReturnsAsync(_updateLoan);
                var result = await _customerServices.UpdateMortgage(_updateLoan);
                if (result == _updateLoan)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");

            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using this method or test get all loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetAllLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.AllLoanApplication());
                var result = await _clerkServices.AllLoanApplication();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using this test get all recived loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ReceivedLoanApplication());
                var result = await _clerkServices.ReceivedLoanApplication();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// using the below test try to get recived loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetNotRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.NotReceivedLoanApplication());
                var result = await _clerkServices.NotReceivedLoanApplication();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using the below method try to test loan application is processed or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ProcessLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// using the below method try to test applied loan ststued is recived or not.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AppliedLoanRecivedLoan_ornot()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                clerkservice.Setup(repos => repos.ReceivedLoan(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
                var result = await _clerkServices.ReceivedLoan(_loanMaster.LoanId);
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Try to test for manager to get all accepted loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetManagerAllLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.AllLoanApplication());
                var result = await _managerServices.AllLoanApplication();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// using this test try to check and accept the loan application by manager with remark
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AcceptLoanApplication_Manager()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.AcceptLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark)).ReturnsAsync(_loanMaster); ;
                var result = await _managerServices.AcceptLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// using this test try to check and reject the loan application by manager with remark
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_RejectLoanApplication_Manager()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.RejectLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark)).ReturnsAsync(_loanMaster); ;
                var result = await _managerServices.RejectLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using the below method try to test Sancationed loan is returining correct object or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_SanctionedLoanApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Check loan ststus for manager before starting loan Sancation process
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_CheckLoanStatus_ForManager()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                managerservice.Setup(repos => repos.CheckLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
                var result = await _managerServices.CheckLoanStatus(_loanMaster.LoanId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to register new user for e loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Register_NewUser()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.Register(_userMaster, _userMaster.PasswordHash));
                var result = await _adminServices.Register(_userMaster, _userMaster.PasswordHash);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using the below method try to test CreateRole fro application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_CreateRole_ForApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.CreateRole(_createRoleViewModel));
                var result = await _adminServices.CreateRole(_createRoleViewModel);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Using the below test method Edit users in role and provide role.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_EditUsersInRole_ForApplication()
        {
            //Arrange
            var res = false;
            string roleId = "7f737659-aa03-4633-ad16-4c1ac83cfe98";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.EditUsersInRole(_userRoleViewModel, roleId));
                var result = await _adminServices.EditUsersInRole(_userRoleViewModel, roleId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Change an existing use passwor function test using below method.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ChangeUserPassword_ForApplication()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.ChangeUserPassword(_changePasswordViewModel));
                var result = await _adminServices.ChangeUserPassword(_changePasswordViewModel);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Edit Role Name for an existing user role name.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_EditRole_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.EditRole(_editRoleViewModel));
                var result = await _adminServices.EditRole(_editRoleViewModel);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to find role by role name
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_FindRoleByRoleName_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string RoleName = "Admin";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.FindRoleByRoleName(RoleName));
                var result = await _adminServices.FindRoleByRoleName(RoleName);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to find role by role id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_FindRoleByRoleId_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string roleId = "7f737659-aa03-4633-ad16-4c1ac83cfe98";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.FindRoleByRoleId(roleId));
                var result = await _adminServices.FindRoleByRoleId(roleId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to list all role in controller
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ListAllRole_ForApplicationRole()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.ListAllRole());
                var result = await _adminServices.ListAllRole();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to get all user from database for admin
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ListAllUser_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.ListAllUser());
                var result = await _adminServices.ListAllUser();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to disable user to block , by admin
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_DisableUser_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string userId = "1aaabedf-2002-4166-801a-ca83aac3247e";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.DisableUser(userId));
                var result = await _adminServices.DisableUser(userId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }

            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to enable user to use e loan app
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_EnableUser_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string userId = "1aaabedf-2002-4166-801a-ca83aac3247e";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.EnableUser(userId));
                var result = await _adminServices.EnableUser(userId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test Find use by email id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_FindByEmailAsync_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string email = "umakumarsingh@iiht.com";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.FindByEmailAsync(email));
                var result = await _adminServices.FindByEmailAsync(email);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        /// <summary>
        /// Test to find user by userId
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_FindUserByIdAsync_ForApplicationUser()
        {
            //Arrange
            var res = false;
            string userId = "1aaabedf-2002-4166-801a-ca83aac3247e";
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.FindUserByIdAsync(userId));
                var result = await _adminServices.FindUserByIdAsync(userId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
    }
}