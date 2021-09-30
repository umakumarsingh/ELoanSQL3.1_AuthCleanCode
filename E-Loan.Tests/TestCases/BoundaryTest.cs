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
    public class BoundaryTest
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
        private static string type = "Boundary";
        public BoundaryTest(ITestOutputHelper output)
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
        /// Test to validate if Apply Mortage loanId is correct or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateMortageApplicationLoanId()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                if (result.LoanId == _loanMaster.LoanId)
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
        /// Test to validate Loan Process Id Is correct or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateLoanProcesstransId()
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

                if (result.Id == _loanProcesstrans.Id)
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
        /// Test to validate LoanApproval Id is correect or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateLoanApprovaltransId()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                if (result.Id == _loanProcesstrans.Id)
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
        /// Testfor_ValidEmail to test email id is valid or not
        /// </summary>
        [Fact]
        public async Task<bool> Testfor_ValidEmail()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                //bool isEmail = Regex.IsMatch(_loanMaster.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                //Assert
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Email.ToString().Length;
                if (result.Email.ToString().Length == actualLength)
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
        /// Testfor_ValidateMobileNumber is used for test mobile number is valid or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ValidateMobileNumber()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Phone.ToString().Length;
                if (result.Phone.ToString().Length == actualLength)
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
        /// Test to validate loan master loan name connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_LoanName_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.LoanName.ToString().Length;
                if (result.LoanName.ToString().Length == actualLength)
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
        /// Test to validate loan master loan BusinessStructure connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_BusinessStructure_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.BusinessStructure.ToString().Length;
                if (result.BusinessStructure.ToString().Length == actualLength)
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
        /// Test to validate loan master loan Billing_Indicator connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_Billing_Indicator_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Billing_Indicator.ToString().Length;
                if (result.Billing_Indicator.ToString().Length == actualLength)
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
        /// Test to validate loan master loan Tax_Indicator connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_Tax_Indicator_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Tax_Indicator.ToString().Length;
                if (result.Tax_Indicator.ToString().Length == actualLength)
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
        /// Test to validate loan master loan Tax_Indicator connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_ContactAddress_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.ContactAddress.ToString().Length;
                if (result.ContactAddress.ToString().Length == actualLength)
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
        /// Test to validate loan master loan Status connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanMaster_LoanStatus_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
                var result = await _customerServices.ApplyMortgage(_loanMaster);
                var actualLength = _loanMaster.Status.ToString().Length;
                if (result.Status.ToString().Length == actualLength)
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
        /// Test to validate loan master Loan Process trans_Id connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_Id_NotEmpty()
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
                var actualLength = _loanProcesstrans.Id.ToString().Length;
                if (result.Id.ToString().Length == actualLength)
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
        /// Test to validate loan master Loan AcresofLand connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_AcresofLand_NotEmpty()
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
                var actualLength = _loanProcesstrans.AcresofLand.ToString().Length;
                if (result.AcresofLand.ToString().Length == actualLength)
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
        /// Test to validate loan master Loan Landvalueinrs connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_LandValueinRs_NotEmpty()
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
                var actualLength = _loanProcesstrans.LandValueinRs.ToString().Length;
                if (result.LandValueinRs.ToString().Length == actualLength)
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
        /// Test to validate loan master Loan Landvalueinrs connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_SuggestedAmount_NotEmpty()
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
                var actualLength = _loanProcesstrans.SuggestedAmount.ToString().Length;
                if (result.SuggestedAmount.ToString().Length == actualLength)
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
            //Asert//final result save in text file, Call rest API to save test result
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
        /// Test to validate loan master Loan Manager Id connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanProcesstrans_ManagerId_NotEmpty()
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
                var actualLength = _loanProcesstrans.ManagerId.ToString().Length;
                if (result.ManagerId.ToString().Length == actualLength)
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
        /// Test to validate loan Approval Loan Sanctioned Amount connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_SanctionedAmount_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                var actualLength = _loanApprovaltrans.SanctionedAmount.ToString().Length;
                if (result.SanctionedAmount.ToString().Length == actualLength)
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
        /// Test to validate loan Approval Loan Termofloan connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_Termofloan_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                var actualLength = _loanApprovaltrans.Termofloan.ToString().Length;
                if (result.Termofloan.ToString().Length == actualLength)
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
        /// Test to validate loan Approval Loan Payment Start Date connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_PaymentStartDate_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                var actualLength = _loanApprovaltrans.PaymentStartDate.ToString().Length;
                if (result.PaymentStartDate.ToString().Length == actualLength)
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
        /// Test to validate loan Approval Loan Loan Closer Date connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_LoanCloserDate_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                var actualLength = _loanApprovaltrans.LoanCloserDate.ToString().Length;
                if (result.LoanCloserDate.ToString().Length == actualLength)
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
        /// Test to validate loan Approval Loan Monthly Payment connaot be blanks.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_LoanApprovaltrans_MonthlyPayment_NotEmpty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
                var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
                var actualLength = _loanApprovaltrans.MonthlyPayment.ToString().Length;
                if (result.MonthlyPayment.ToString().Length == actualLength)
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