using MedicineScheduler.DataAccessLayer.Entities;
using MedicineScheduler.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineScheduler.Tests.DataAccessLayer.Tests.Configuration;

[TestClass]
public class OrderConfigurationTest
{

  private static IEnumerable<object[]> PriceTotal_Generated_OK_TestData
    => new[] 
    { 
      new object[] { 1M, 1, 1M},
      new object[] { 1M, 2, 2M},
      new object[] { 10M, 2, 20M},
      new object[] { 1.55M, 2, 3.1M},
    };

  [TestMethod]
  [DynamicData(nameof(PriceTotal_Generated_OK_TestData))]
  public void PriceTotal_GeneratedColumn_OK(decimal pricePerPackage, int quantityPackages, decimal priceTotalExpected)
  {
    using var context = SqLite.GetEFContextInMemory();
    var order = new Order()
    {
      DosageForm = new(),
      Medicine = new() {Title = "Medicine"},
      Location = new() {Name = "Location"},
      PricePerPackage = pricePerPackage,
      QuantityPackages = quantityPackages,
    };
    context.Orders.Add(order);
    context.SaveChanges();

    Assert.AreEqual(priceTotalExpected, order.PriceTotal);
  }
}
