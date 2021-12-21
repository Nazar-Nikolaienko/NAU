using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace BLL
{
    public class ProductService
    {
        public List<Product> Products { get; set; } = new List<Product>();
        private Serialize<Product> serialize;

        public ProductService()
        {
        }

        public ProductService(string fileName)
        {
            serialize = new Serialize<Product>(fileName);
            try { Products = serialize.Load().ToList(); }
            catch { serialize.Save(Products.ToArray()); }
        }

        public void Add(string name, string brand, int number, int cost, Category category, Provider provider)
        {
            if (provider == null)
                throw new Exception("Product must have provider");
            if (name == null || string.IsNullOrEmpty(name.Trim()))
                throw new Exception("Name cant be null");
            if (brand == null || string.IsNullOrEmpty(brand.Trim()))
                throw new Exception("Brand cant be null");
            if (number < 0)
                throw new Exception("Number of product cant be lass then 0");
            if (cost < 1)
                throw new Exception("Cost of product cant be lass then 1");
            if (category == null)
                category = new CategoryService("Category").Categories[0];
            Product product = new Product { Name = name, Category = category, Number = number, Provider = provider, Cost = cost, Brand = brand };
            category.Products.Add(product);
            Products.Add(product);
        }

        public Product Get(int ind) => ind < 0 || ind >= Products.Count ? null : Products[ind];

        public void Remove(int ind, List<Category> categories, List<Provider> providers)
        {
            if (ind < 0 || ind >= Products.Count)
                throw new Exception("Index out of range");
            foreach (var category in categories)
                if (category == Products[ind].Category)
                    category.Products.Remove(Products[ind]);
            foreach (var provider in providers)
                if (provider == Products[ind].Provider)
                    provider.Products.Remove(Products[ind]);
            Products.RemoveAt(ind);
        }

        public void Edit(int ind, string newName, string newBrand, int cost, List<Provider> providers, List<Category> categories)
        {
            if (ind < 0 || ind >= Products.Count)
                throw new Exception("Index out of range");
            if (cost < 1)
                throw new Exception("Cost of product cant be lass then 1");

            foreach (var provider in providers)
                foreach (var product in provider.Products)
                    if (product == Products[ind])
                    {
                        product.Name = newName == "" ? product.Name : newName;
                        product.Brand = newBrand == "" ? product.Brand : newBrand;
                        product.Cost = cost;
                    }

            foreach (var category in categories)
                foreach (var product in category.Products)
                    if (product == Products[ind])
                    {
                        product.Name = newName == "" ? product.Name : newName;
                        product.Brand = newBrand == "" ? product.Brand : newBrand;
                        product.Cost = cost;
                    }
            Products[ind].Name = newName == "" ? Products[ind].Name : newName;
            Products[ind].Brand = newBrand == "" ? Products[ind].Brand : newBrand;
            Products[ind].Cost = cost;
        }

        public void Add(int ind, int number)
        {
            if (ind < 0 || ind >= Products.Count)
                throw new Exception("Index out of range");
            Products[ind].Number += number;
        }

        public void Remove(int ind, int number)
        {
            if (ind < 0 || ind >= Products.Count)
                throw new Exception("Index out of range");
            if (Products[ind].Number - number < 0)
                throw new Exception("not enough product");
            Products[ind].Number -= number;
        }

        public string ShowConcrete(int ind)
        {
            if (ind < 0 || ind >= Products.Count)
                throw new Exception("Index out of range");
            return $"Profuct: {Products[ind].Name}\tCategory: {Products[ind].Category.Name} \tBrand: {Products[ind].Brand}\tRemained in stock{Products[ind].Number}\tCost for one: {Products[ind].Cost}\tProvider: {Products[ind].Provider.Name} {Products[ind].Provider.Surname}";
        }

        public string Find(string name)
        {
            string str = "";
            foreach (var a in Products.FindAll(x => x.Name == name))
                str += a;
            if (str == "")
                str += "Nothing found";
            return str;
        }

        public List<Product> SortByCost()
        {
            var temp = Products;
            for (int i = 0; i < Products.Count - 1; i++)
            {
                int min = i;
                for (int j = i; j < Products.Count - 1; j++)
                    if (temp[j].Cost < temp[min].Cost)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;

            }
            return temp;
        }

        public List<Product> SortByName()
        {
            var temp = Products;
            for (int i = 0; i < Products.Count - 1; i++)
            {
                int min = i;
                for (int j = i; j < Products.Count - 1; j++)
                    if (temp[j].Name.CompareTo(temp[min].Name) < 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;

            }
            return temp;
        }

        public List<Product> SortByBrand()
        {
            var temp = Products;
            for (int i = 0; i < Products.Count - 1; i++)
            {
                int min = i;
                for (int j = i; j < Products.Count - 1; j++)
                    if (temp[j].Brand.CompareTo(temp[min].Brand) < 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;

            }
            return temp;
        }

        public void Save() =>
            serialize.Save(Products.ToArray());

    }
}
