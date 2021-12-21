using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private CategoryService categoryService;

        [SetUp]
        public void Load()
        {
            categoryService = new CategoryService();
        }

        [Test]
        public void Get_TryToGetElementWhenIndeOutOfRange_ReturnNull()
        {
            Assert.Null(categoryService.Get(-1));
        }

        [Test]
        public void Get_TryToGetElemen()
        {
            var category = categoryService.Get(categoryService.Categories.Count - 1);

            Assert.AreEqual(category.Name, "Other");
        }

        [Test]
        public void Add_TryToAddWithNullName_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => categoryService.AddCategory(""));

            Assert.AreEqual(a.Message, "Category name can`t be empty");
        }

        [Test]
        public void Add_TryToAdd()
        {
            var count = categoryService.Categories.Count;

            categoryService.AddCategory("category");
            count++;

            Assert.AreEqual(count, categoryService.Categories.Count);
        }

        [Test]
        public void Delete_TryToDeleteDefaultCategory_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => categoryService.DeleteCategor(0, new List<Product>()));

            Assert.AreEqual(a.Message, "You can`t delete default category");
        }

        [Test]
        public void Delete_TryToDeleteWhenIndeOutOfRange_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => categoryService.DeleteCategor(-1, new List<Product>()));

            Assert.AreEqual(a.Message, "Index out of range");
        }

        [Test]
        public void Delete_TryToDelete()
        {
            categoryService.Categories.Add(new Category());
            var count = categoryService.Categories.Count;

            categoryService.DeleteCategor(categoryService.Categories.Count - 1, new List<Product> { new Product { Category = categoryService.Categories[1] } });
            count--;

            Assert.AreEqual(count, categoryService.Categories.Count);
        }

        [Test]
        public void Edit_TryToEditWhenIndeOutOfRange_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => categoryService.Edit(-1, new List<Product>(), "str"));

            Assert.AreEqual(a.Message, "Index out of range");
        }

        [Test]
        public void Edit_TryToEditWhenNameIsEmpt_ShouldThrowException()
        {
            categoryService.Categories.Add(new Category());

            var a = Assert.Throws<Exception>(() => categoryService.Edit(1, new List<Product>(), ""));

            Assert.AreEqual(a.Message, "Category name can`t be empty");
        }

        [Test]
        public void Edit_TryToEditeDefaultCategory_ShouldThrowException()
        {
            var a = Assert.Throws<Exception>(() => categoryService.Edit(0, new List<Product>(), "str"));

            Assert.AreEqual(a.Message, "You can`t edit default category");
        }

        [Test]
        public void Edit_TryToEdite()
        {
            categoryService.Categories.Add(new Category());

            categoryService.Edit(1, new List<Product> { new Product { Category = categoryService.Categories[1] } }, "str");

            Assert.AreEqual(categoryService.Categories[1].Name, "str");
        }
    }
}
