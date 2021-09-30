using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace E_Loan.BusinessLayer.Services
{
    public class LoanClerkServices : ILoanClerkServices
    {
        /// <summary>
        /// Creating the ILoanClerkRepository field/instance and injecting in LoanClerkServices constuctor
        /// </summary>
        private readonly ILoanClerkRepository _clerkRepository;
        public LoanClerkServices(ILoanClerkRepository loanClerkRepository)
        {
            _clerkRepository = loanClerkRepository;
        }
        /// <summary>
        /// Get/Show all loan application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Show not recived loan application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> NotReceivedLoanApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Start the loan process after all status verifaction by loan clerk
        /// loan status must be in "Recived" before process
        /// </summary>
        /// <param name="loanProcesstrans"></param>
        /// <returns></returns>
        public async Task<LoanProcesstrans> ProcessLoan(LoanProcesstrans loanProcesstrans)
        {
            //do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Change the status of Not Recived to Recived loan application before start the loan process
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> ReceivedLoan(int loanId)
        {
            //Do code here
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sho/get all loan application that is in recived status
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> ReceivedLoanApplication()
        {
            //do code here
            throw new NotImplementedException();
        }
    }
}
