namespace T002.Core.Models
{
    public class InvoiceLineItem
    {
       public short LineNo { get; set; }
       public int InvoiceNo { get; set; }
       public string UOM { get; set; }
       public decimal Quantity { get; set; }
       public decimal UnitPrice { get; set; }
       public decimal LineAmount { get; set; }
       public decimal GSTRate { get; set; }
       public decimal GSTAmount { get; set; }
    }
}
