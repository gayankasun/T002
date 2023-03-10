using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using T002.Core.Interfaces;
using T002.Core.Models;

namespace T002.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceHeaderRepository _invoiceHeaderRepository;
        public InvoiceController(IInvoiceHeaderRepository invoiceHeaderRepository)
        {
            _invoiceHeaderRepository = invoiceHeaderRepository;
        }
        /// <summary>
        /// Create New Invoice
        /// </summary>
        /// <param name="AddInvoice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> CreateInvoice([FromBody] InvoiceHeader invoiceHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var addInvoice = await _invoiceHeaderRepository.AddAsync(invoiceHeader);

            return Ok(addInvoice);
        }

        /// <summary>
        /// Get New Invoice
        /// </summary>
        /// <param name="AddInvoice"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> GetInvoice(string InvoiceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var invoice = await _invoiceHeaderRepository.GetByIdAsync(InvoiceId);

            return Ok(invoice);
        }
    }
}
