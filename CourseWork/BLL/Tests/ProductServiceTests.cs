using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService service;

        [SetUp]
        public void Load()
        {
            service = new ProductService();
        }

        [Test]
        public void Get_TryToGetWhenIndexOutOfRange_ReturnNull()
        {
            Assert.Null(service.Get(-1));
        }

        [Test]
        public void Get_TryToGet()
        {
            Product product = new Product { Name = "name", Brand = "Brand", Category = new CategoryService().Categories[0] };
            service.Products.Add(product);

            Assert.AreEqual(service.Get(0), product);
        }

        [Test]
        public void Add_TryToAddWithNullName_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Add("", "brand", 15, 15, new Category(), new Provider()));

            Assert.AreEqual(a.Message, ("Name cant be null"));
        }

        [Test]
        public void Add_TryToAddWithNullBrand_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Add("Name", "", 15, 15, new Category(), new Provider()));

            Assert.AreEqual(a.Message, ("Brand cant be null"));
        }

        [Test]
        public void Add_TryToAddWitInvalidNumber_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Add("Name", "Brand", -100, 15, new Category(), new Provider()));

            Assert.AreEqual(a.Message, ("Number of product cant be lass then 0"));
        }

        [Test]
        public void Add_TryToAddWitInvalidCost_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Add("Name", "Brand", 100, -15, new Category(), new Provider()));

            Assert.AreEqual(a.Message, ("Cost of product cant be lass then 1"));
        }

        [Test]
        public void Add_TryToAddWithNullProvide_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Add("Name", "Brand", 15, 15, new Category(), null));

            Assert.AreEqual(a.Message, ("Product must have provider"));
        }


        [Test]
        public void Add_TryToAdd()
        {
            int count = service.Products.Count;

            service.Add("Name", "Brand", 15, 15, new Category(), new Provider());
            count++;

            Assert.AreEqual(service.Products.Count, count);
        }

        [Test]
        public void Edit_TryToEditWhenIndOutOfRange_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Edit(-1, "", "", 100, new List<Provider>(), new List<Category>()));

            Assert.AreEqual(a.Message, "Index out of range");
        }

        [Test]
        public void Edit_TryToEditWitInvalidCost_ShouldThrowException()
        {
            service.Products.Add(new Product());

            var a = Assert.Throws<Exception>(() => service.Edit(0, "", "", -100, new List<Provider>(), new List<Category>()));

            Assert.AreEqual(a.Message, "Cost of product cant be lass then 1");
        }

        [Test]
        public void Edit_TryToEdit()
        {
            Category category = new Category();
            Provider provider = new Provider { };
            var element = new Product { Category = category, Provider = provider };
            category.Products.Add(element);
            provider.Products.Add(element);
            List<Category> categories = new List<Category> { category };
            List<Provider> providers = new List<Provider> { provider };
            service.Products.Add(element);

            service.Edit(0, "Name", "Brend", 100, providers, categories);
        }

        [Test]
        public void Remove_TryToRemoveWhenIndOutOfRange_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => service.Remove(-1, new List<Category>(), new List<Provider>()));

            Assert.AreEqual(a.Message, "Index out of range");
        }

        [Test]
        public void Remove_TryToRemove()
        {
            Category category = new Category();
            Provider provider = new Provider { };
            var element = new Product { Category = category, Provider = provider };
            category.Products.Add(element);
            provider.Products.Add(element);
            List<Category> categories = new List<Category> { category };
            List<Provider> providers = new List<Provider> { provider };
            service.Products.Add(element);

            service.Remove(0, categories, providers);
        }
    }
}
