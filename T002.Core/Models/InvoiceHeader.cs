using System;

namespace T002.Core.Models
{
	public class InvoiceHeader: Db
    {
		public int InvoiceNo { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceSubTotal { get; set; }
        public decimal GstTotal { get; set; }
        public decimal InvoiceTotal { get; set; }
        public string SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public InvoiceLineItem[] LineItems { get; set; }
    }
}
