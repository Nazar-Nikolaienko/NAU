using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace BLL
{
    public class ProviderService
    {
        public List<Provider> Providers { get; set; } = new List<Provider> { new Provider { Name = "Default", Surname = "Default" } };
        private Serialize<Provider> serialize;

        public ProviderService() { }

        public ProviderService(string name)
        {
            serialize = new Serialize<Provider>(name);
            try { Providers = serialize.Load().ToList(); }
            catch { serialize.Save(Providers.ToArray()); }
        }

        public void Add(string name, string surname)
        {
            if (name == null || string.IsNullOrEmpty(name.Trim()))
                throw new Exception("Name cant be null");
            if (surname == null || string.IsNullOrEmpty(surname.Trim()))
                throw new Exception("Surname cant be null");
            Providers.Add(new Provider { Name = name, Surname = surname });
        }

        public void Remove(int ind, List<Product> products)
        {
            if (ind < 0 || ind >= Providers.Count)
                throw new Exception("Index out of range");
            if (Providers[ind].Name == "Default")
                throw new Exception("You can`t delete default provider");
            foreach (var product in products)
                if (product.Provider == Providers[ind])
                    product.Provider = Providers[0];
            Providers.RemoveAt(ind);
        }

        public void Edit(int ind, string newName, string newSurname, List<Product> products)
        {
            if (ind < 0 || ind >= Providers.Count)
                throw new Exception("Index out of range");
            if (string.IsNullOrEmpty(newName.Trim()) || newName == null)
                throw new Exception("Category name can`t be empty");
            if (string.IsNullOrEmpty(newSurname.Trim()) || newSurname == null)
                throw new Exception("Category name can`t be empty");
            if (Providers[ind].Name == "Default")
                throw new Exception("You can`t delete default provider");

            foreach (var product in products)
                if (product.Provider == Providers[ind])
                {
                    product.Provider.Name = newName;
                    product.Provider.Surname = newSurname;
                }
            Providers[ind].Surname = newSurname;
            Providers[ind].Name = newName;
        }

        public List<Provider> SortByName()
        {
            var temp = Providers;
            for (int i = 0; i < Providers.Count - 1; i++)
            {
                int min = i;
                for (int j = i; j < Providers.Count - 1; j++)
                    if (temp[j].Name.CompareTo(temp[min].Name) < 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;

            }
            return temp;
        }

        public List<Provider> SortBySurname()
        {
            var temp = Providers;
            for (int i = 0; i < Providers.Count - 1; i++)
            {
                int min = i;
                for (int j = i; j < Providers.Count - 1; j++)
                    if (temp[j].Surname.CompareTo(temp[min].Surname) < 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;

            }
            return temp;
        }

        public Provider Get(int ind) => ind < 0 || ind >= Providers.Count ? null : Providers[ind];

        public void Save() => serialize.Save(Providers.ToArray());

        public string Find(string name)
        {
            string str = "";
            foreach (var a in Providers.FindAll(x => x.Name == name))
                str += a;
            if (str == "")
                str += "Nothing found";
            return str;

        }
    }
}
