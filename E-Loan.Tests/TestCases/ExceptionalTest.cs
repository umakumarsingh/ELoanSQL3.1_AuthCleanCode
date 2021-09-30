using E_Loan.BusinessLayer;
using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace E_Loan.Tests.TestCases
{
    public class ExceptionalTest
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

        private LoanMaster _loanMaster;
        private UserMaster _userMaster;
        private LoanProcesstrans _loanProcesstrans;
        private LoanApprovaltrans _loanApprovaltrans;
        private CreateRoleViewModel _createRoleViewModel;
        private UserRoleViewModel _userRoleViewModel;
        private ChangePasswordViewModel _changePasswordViewModel;
        private EditRoleViewModel _editRoleViewModel;
        private static string type = "Exceptional";
        public ExceptionalTest(ITestOutputHelper output)
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
                UserName = "Kundan",
                Email = "umakumarsingh@techademy.com",
                PasswordHash = "Password@1234",
                Contact = "9631438123",
                Address = "Gaya",
                IdproofTypes = IdProofType.PAN,
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
        /// Test to validate if user pass the null object while apply mortage, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidApplyMortage()
        {
            //Arrange
            bool res = false;
            _loanMaster = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster = null);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                if (result == null)
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
        /// Test to validate if clerk pass the null object while Process Loan, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidProcessLoanTrans()
        {
            //Arrange
            bool res = false;
            _loanProcesstrans = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans = null);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result == null)
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
        /// Test to validate if clerk pass the null object while Sanctioned Loan, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidSanctionedLoanTrans()
        {
            //Arrange
            bool res = false;
            _loanApprovaltrans = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans = null);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                if (result == null)
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
        /// Test to validate if user pass the null loan id or 0 while finding mortage, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_ifInvalidLoanIdIsPassedandLoanNotFound()
        {
            //Arrange
            bool res = false;
            var loanId = 0;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.AppliedLoanStatus(loanId)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.AppliedLoanStatus(loanId);
                if (result != null || result.LoanId > 0)
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
        /// Test to validate if loan name must be greater then 5 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidLoanNamePassed_LoanNameMinFiveCharactor()
        {
            //Arrange
            bool res = false;
            _loanProcesstrans = null;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                if (result != null && result.LoanName.Length > 5)
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
        /// Test to validate if loan process id must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcessIdIsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.Id > 0)
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
        /// Test to validate if loan process acres of land must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_AcresOfLand_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.AcresofLand > 0)
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
        /// Test to validate if loan process Land ValueinRs must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_LandValueinRs_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.LandValueinRs > 0)
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
        /// Test to validate if loan process Suggested Amount must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_SuggestedAmount_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.SuggestedAmount > 0)
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
        /// Test to validate if loan process manager Id must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_VaidateLoanProcess_ManagerId_IsvalidorNot_Return()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
                var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
                if (result != null && result.ManagerId > 0)
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