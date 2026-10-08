using System;
using CMS.BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMS.BusinessLayerTest
{
    [TestClass]
    public class Demo2Test
    {
        [TestMethod]
        public void RepositoriesPreserveRequestedIds()
        {
            Assert.AreEqual(11, new CustomerRepository().Retrieve(11).CustomerId);
            Assert.AreEqual(22, new ProductRepository().Retrieve(22).ProductId);
            Assert.AreEqual(33, new OrderRepository().Retrieve(33).OrderId);
            Assert.AreEqual(44, new OrderItemRepository().Retrieve(44).OrderItemId);
        }

        [TestMethod]
        public void OrderCanUseCustomerHomeAddressForShipping()
        {
            var customer = new Customer(1)
            {
                LastName = "Baggins",
                EmailAddress = "bilbo@example.com",
                HomeAddress = new Address
                {
                    StreetLine1 = "Bag End",
                    City = "Hobbiton",
                    Country = "Shire"
                },
                WorkAddress = new Address { City = "Bree" }
            };
            var order = new Order(2)
            {
                Customer = customer,
                ShippingAddress = customer.HomeAddress,
                OrderDate = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero)
            };

            Assert.AreSame(customer, order.Customer);
            Assert.AreSame(customer.HomeAddress, order.ShippingAddress);
            Assert.AreNotSame(customer.WorkAddress, order.ShippingAddress);
            Assert.IsTrue(customer.Validate());
            Assert.IsTrue(order.Validate());
        }

        [TestMethod]
        public void ProductRequiresNameAndPrice()
        {
            var product = new Product { ProductName = "Book", CurrentPrice = 0m };
            Assert.IsTrue(product.Validate());

            product.CurrentPrice = null;
            Assert.IsFalse(product.Validate());

            product.CurrentPrice = 10m;
            product.ProductName = " ";
            Assert.IsFalse(product.Validate());
        }

        [TestMethod]
        public void OrderRequiresDate()
        {
            Assert.IsFalse(new Order().Validate());
            Assert.IsTrue(new Order
            {
                OrderDate = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero)
            }.Validate());
        }

        [DataTestMethod]
        [DataRow(1, 1, true, true)]
        [DataRow(0, 1, true, false)]
        [DataRow(-1, 1, true, false)]
        [DataRow(1, 0, true, false)]
        [DataRow(1, -1, true, false)]
        [DataRow(1, 1, false, false)]
        public void OrderItemRequiresQuantityProductAndPrice(
            int quantity, int productId, bool hasPrice, bool expected)
        {
            var item = new OrderItem
            {
                OrderQuantity = quantity,
                ProductId = productId,
                PurchasePrice = hasPrice ? (decimal?)0m : null
            };

            Assert.AreEqual(expected, item.Validate());
        }
    }
}
