using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers.BankingManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly BankService _bankingService;

        public BankingController()
        {
            _bankingService = new BankService();
        }

        [HttpPost("create")]
        public IActionResult createAmont([FromBody] string accountHolder)
        {
            if (string.IsNullOrEmpty(accountHolder))
            {
                return BadRequest(new
                {
                    Message = "Account Holder Name is Required"
                });

            }
            var account = _bankingService.CreateAccount(accountHolder);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, new
            {
                Result = account,
                Message = "Account Created Successfully",
                TimeStamp = DateTime.UtcNow
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(Guid id)
        {
            var account = _bankingService.GetAccount(id);
            if (account == null)
            {
                return NotFound(new { Message = "Account Not Found" });
            }

            return Ok(new
            {
                Result = account,
                Message = "Account Fetched Successfully",
                TimeStamp = DateTime.UtcNow
            });
        }


        [HttpPost("{id}/deposit")]
        public IActionResult Deposit(Guid id, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest(new { Message = "Amount must be greater than 0" });

            }

            var result = _bankingService.Deposit(id, amount);
            if (!result)
            {
                return NotFound(new
                {
                    Message = "Account Not found or Invalid Amount"
                });

            }
            return Ok(new
            {
                Message = "Deposit Successfull",
                TimeStamp = DateTime.UtcNow
            });
        }

        [HttpPost("{id}/withdraw")]

        public IActionResult Withdrawn(Guid id, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest(new
                {
                    Message = "Amount must be greater than zero."
                });
            }
            var result = _bankingService.Withdrawn(id, amount);
            if (!result)
            {
                return NotFound(new { Message = "Account not found or insufficient Funds" });
            }
            return Ok(new
            {
                Message = "Withdrawn Successfull",
                Result = result,
                TimeStamp = DateTime.UtcNow
            });
        }


        [HttpPost("{fromId}/transfer/{toId}")]

        public IActionResult Transafer(Guid fromId, Guid toId, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest(new { Message = "Amount must be greater then zero" });
            }

            var result = _bankingService.Transfer(fromId, toId, amount);
            if (!result)
            {
                return NotFound(new
                {
                    Message = "Account Not found or Insufficient Balance"
                });

            }
            return Ok(new
            {
                Message = "Transafer Successful",
                Result = result,
                TimeStamp = DateTime.UtcNow
            });
        }


        [HttpGet("{id}/transaction")]

        public IActionResult GetTransaction(Guid id)
        {
            var transaction = _bankingService.GetTransactions(id);
            if(transaction.Count == 0)
            {
                return NotFound(new
                {
                    Mesage = "No Transaction Found"
                });
            }
            return Ok(new
            {
                Message = "Transaction Fetched Successfully",
                Result = transaction,
                TimeStamp = DateTime.UtcNow
            });
        }

    }
}
