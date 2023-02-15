using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using T002.Core.Models;

namespace T002.API.Controllers
{
    public class InvoiceController : ControllerBase
    {
        /// <summary>
        /// Create New Invoice
        /// </summary>
        /// <param name="AddInvoice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateInvoice([FromBody] InvoiceHeader invoiceHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //var toDo = await _repo.AddAsync(invoice)

            return Ok();
        }
    }
}
