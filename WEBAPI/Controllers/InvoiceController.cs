using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using T002.Core.Interfaces;
using T002.Core.Models;

namespace WEBAPI.Controllers
{
    public class InvoiceController : ControllerBase
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

            var invoice = await _invoiceHeaderRepository.GetByIdAsync(InvoiceId);

            return Ok(invoice);
        }
    }
}
