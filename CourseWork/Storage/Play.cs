using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace Storage
{
    public class Play
    {
        public Play()
        { }

        private CategoryService categoryService = new CategoryService("Category");
        private ProductService productService = new ProductService("Product");
        private ProviderService providerService = new ProviderService("Provider");

        private int Parse(string number)
        {
            try { return int.Parse(number) - 1; }
            catch { return -1; }
        }

        private int CategoryInd()
        {
            int count = 1;
            foreach (var category in categoryService.Categories)
                Console.WriteLine($"{count++}. Category: {category.Name}");
            Console.Write("Index: ");
            return Parse(Console.ReadLine());
        }

        private int ProviderInd()
        {
            int count = 1;
            foreach (var provider in providerService.Providers)
                Console.WriteLine($"{count++}. Provider: {provider.Name} {provider.Surname}");
            Console.Write("Index: ");
            return Parse(Console.ReadLine());
        }

        private int ProductInd()
        {
            int count = 1;
            foreach (var product in productService.Products)
                Console.WriteLine($"{count++}. Product: {product.Name} {product.Brand}");
            Console.Write("Index: ");
            return Parse(Console.ReadLine());
        }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Main Menu" +
                    "\n1. Work with categories\n2. Work with porducts\n3. Work with providers\n4. Find\n0. Exit");
                Console.Write("Your choise: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {
                    case "0":
                        categoryService.Save();
                        productService.Save();
                        providerService.Save();
                        run = false;
                        break;
                    case "1":
                        Category();
                        break;
                    case "2":
                        Product();
                        break;
                    case "3":
                        Provaider();
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Category()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Category Menu" +
                    "\n1. Add category\n2. Delete category\n3. Edit category\n4. Show all\n5. Show concrate\n0. Exit");
                Console.Write("Your choise: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {
                    case "0":
                        run = false;
                        break;
                    case "1":
                        try
                        {
                            Console.Write("Category name: ");
                            categoryService.AddCategory(Console.ReadLine());
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("All category:");
                            categoryService.DeleteCategor(CategoryInd(), productService.Products);
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("All category:");
                            int ind = CategoryInd();
                            Console.Write("New name: ");
                            categoryService.Edit(ind, productService.Products, Console.ReadLine());
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        foreach (var category in categoryService.Categories)
                            Console.WriteLine($"Category: {category.Name}");
                        Console.ReadKey();
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("All category:");
                            Console.WriteLine(categoryService.ShowConcrete(CategoryInd()));
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Product()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Category Menu" +
                    "\n1. Add product\n2. Delete product\n3. Edit product\n4. Add/Remove number of products\n5. Show Concrate\n6. Show sort\n0. Exit");
                Console.Write("Your choise: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {
                    case "0":
                        run = false;
                        break;
                    case "1":
                        try
                        {
                            Console.WriteLine("Providers");
                            var pInd = ProviderInd();
                            Console.WriteLine("\nCategories");
                            var cInd = CategoryInd();
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Brand: ");
                            string brend = Console.ReadLine();
                            Console.Write("Quantity in stock: ");
                            int number = Parse(Console.ReadLine()) + 1;
                            Console.Write("Price: ");
                            productService.Add(name, brend, number, Parse(Console.ReadLine()) + 1, provider: providerService.Get(pInd), category: categoryService.Get(cInd));

                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("All products:");
                            productService.Remove(ProductInd(), categoryService.Categories, providerService.Providers);

                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("All products:");
                            int ind = ProductInd();
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Brand: ");
                            string brend = Console.ReadLine();
                            Console.Write("Price: ");
                            productService.Edit(ind, name, brend, Parse(Console.ReadLine()) + 1, providerService.Providers, categoryService.Categories);
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        try
                        {
                            Console.WriteLine("All products:");
                            int ind = ProductInd();
                            Console.Write("Number: ");
                            int number = Parse(Console.ReadLine());
                            Console.WriteLine("1. Add\n2. Remove");
                            string a = Console.ReadLine();
                            if (a == "1")
                                productService.Add(ind, number);
                            else if (a == "2")
                                productService.Remove(ind, number);
                            else
                                Console.WriteLine("Wrong input");
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("All products:");
                            productService.ShowConcrete(ProductInd());
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "6":
                        Show();
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Show()
        {
            Console.Clear();
            Console.WriteLine("1. Sort by price\n2. Sort by Name\n3. Sort by Brand\n4. Show");
            string choise = Console.ReadLine();
            Console.Clear();
            switch (choise)
            {
                case "1":
                    foreach (var a in productService.SortByCost())
                        Console.WriteLine(a);
                    break;
                case "2":
                    foreach (var a in productService.SortByName())
                        Console.WriteLine(a);
                    break;
                case "3":
                    foreach (var a in productService.SortByBrand())
                        Console.WriteLine(a);
                    break;
                case "4":
                    foreach (var a in productService.Products)
                        Console.WriteLine(a);
                    break;
                default:
                    Console.WriteLine("Wrong index\nPress any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void Provaider()
        {


            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Provaider Menu" +
                    "\n1. Add provaider\n2. Delete provider\n3. Edit provaider\n4. Show\n5. Show concrate\n0. Exit");
                Console.Write("Your choise: ");
                string choise = Console.ReadLine();
                Console.Clear();
                switch (choise)
                {
                    case "0":
                        run = false;
                        break;
                    case "1":
                        try
                        {
                            Console.Write("Provider name: ");
                            string name = Console.ReadLine();
                            Console.Write("Provider surname: ");
                            providerService.Add(name, Console.ReadLine());
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("All providers:");
                            providerService.Remove(ProviderInd(), productService.Products);
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("All providers:");
                            int ind = CategoryInd();
                            Console.Write("Provider name: ");
                            string name = Console.ReadLine();
                            Console.Write("Provider surname: ");
                            providerService.Edit(ind, name, Console.ReadLine(), productService.Products);
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        ProviderShow();
                        Console.ReadKey();
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("All category:");
                            Console.WriteLine(categoryService.ShowConcrete(CategoryInd()));
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }

        }

        private void ProviderShow()
        {
            Console.Clear();
            Console.WriteLine("1. Sort by Name\n2. Sort by Surname\n3. Show");
            string choise = Console.ReadLine();
            Console.Clear();

            switch (choise)
            {
                case "1":
                    foreach (var a in providerService.SortByName())
                        Console.WriteLine(a);
                    break;
                case "2":
                    foreach (var a in providerService.SortBySurname())
                        Console.WriteLine(a);
                    break;
                case "3":
                    foreach (var a in providerService.Providers)
                        Console.WriteLine(a);
                    break;
                default:
                    Console.WriteLine("Wrong index\nPress any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void Find()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Find menu\n1.Find products by name\n2.Find providers by name\n0. Exit");
                Console.Write("Choise: ");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        Console.Write("Write name: ");
                        Console.WriteLine(productService.Find(Console.ReadLine()));
                        break;
                    case "2":
                        Console.Write("Write name: ");
                        Console.WriteLine(providerService.Find(Console.ReadLine()));
                        break;
                    case "0":
                        run = false;
                        break;
                }
            }
        }
    }
}
