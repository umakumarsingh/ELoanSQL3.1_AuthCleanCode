using E_Loan.BusinessLayer.Interfaces;
using E_Loan.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Creating the field of ILoanCustomerServices and injecting in CustomerController constructor
        /// </summary>
        private readonly ILoanCustomerServices _customerServices;
        public CustomerController(ILoanCustomerServices loanCustomerServices)
        {
            _customerServices = loanCustomerServices;
        }
        /// <summary>
        /// Get loan status for customer while or before updating loan application.
        /// Get the loan application status by loanId
        /// Check if loan applcant email id is match with loan applied email id, then show loan record.
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("loan-status/{loanId}")]
        public async Task<ActionResult<LoanMaster>> AppliedLoanStatus(int loanId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Applay new loan or mortage supply all value for LoanMaster, and make sure you are login.
        /// get the login user email id and store in loan application entities email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("apply-mortage")]
        public async Task<IActionResult> ApplayMortage([FromBody] LoanMaster model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update loan/mortage if not recived by loan clerk then perforn actual update,
        /// Pass registred user email id while update loan
        /// Check loan status is recived return a error message
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-mortage/{loanId}")]
        public async Task<IActionResult> UpdateMortage([FromBody] LoanMaster model, int loanId)
        {
            if (!ModelState.IsValid && loanId <= 0)
            {
                return BadRequest(ModelState);
            }
            //do code here
            throw new NotImplementedException();
        }
    }
}
