using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class CategoryService
    {
        public List<Category> Categories { get; set; } = new List<Category> { new Category { Name = "Other" } };
        private Serialize<Category> serialize;

        public CategoryService(string fileName)
        {
            serialize = new Serialize<Category>(fileName);
            try
            {
                Categories = serialize.Load().ToList();
            }
            catch
            {
                serialize.Save(Categories.ToArray());
            }
        }

        public Category Get(int ind) => ind < 0 || ind >= Categories.Count ? null : Categories[ind];

        public CategoryService() { }

        public void AddCategory(string name)
        {
            if (string.IsNullOrEmpty(name.Trim()) || name == null)
                throw new Exception("Category name can`t be empty");
            Categories.Add(new Category { Name = name });
        }

        public void DeleteCategor(int ind, List<Product> products)
        {
            if (ind < 0 || ind >= Categories.Count)
                throw new Exception("Index out of range");
            if (Categories[ind].Name == "Other")
                throw new Exception("You can`t delete default category");
            foreach (var product in products)
                if (product.Category == Categories[ind])
                {
                    product.Category = Categories[0];
                    Categories[0].Products.Add(product);
                }
            Categories.RemoveAt(ind);
        }

        public void Edit(int ind, List<Product> products, string newName)
        {
            if (ind < 0 || ind >= Categories.Count)
                throw new Exception("Index out of range");
            if (string.IsNullOrEmpty(newName.Trim()) || newName == null)
                throw new Exception("Category name can`t be empty");
            if (Categories[ind].Name == "Other")
                throw new Exception("You can`t edit default category");
            foreach (var product in products)
                if (product.Category == Categories[ind])
                    product.Category.Name = newName;
            Categories[ind].Name = newName;
        }

        public string ShowConcrete(int ind) => ind < 0 || ind >= Categories.Count ? throw new Exception("Index out of range") : $"Category: {Categories[ind].Name}\tNumber of products:{Categories[ind].Products.Count}";

        public void Save() => serialize.Save(Categories.ToArray());
    }
}
