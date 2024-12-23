﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class OrderHeader
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    [DataType(DataType.PostalCode)]
    public string? ZipCode { get; set; }

    public DateTime OrderDate { get; set; }
    public string? Status { get; set; }

    public string? Comments { get; set; }

    public int CommodityCount { get; set; }

    [Required]
    public int PaymentId { get; set; }

    [ForeignKey("PaymentId")]
    public Payment? Payment { get; set; }

    [Required]
    public int DeliveryId { get; set; }

    [ForeignKey("DeliveryId")]
    public Delivery? Delivery { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [ForeignKey("SupplierId")]
    public Supplier? Supplier { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime TimeToOrder { get; set; } = DateTime.Now;

    [Required]
    [DataType(DataType.EmailAddress)]
    public DateTime TimeToRealisation { get; set; } = DateTime.Now.AddDays(14);

    public string? InfoToPdf { get; set; }
  }
}
