using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace PhoneBook
{
    class Tools
    {
        static List<Data> datas = new List<Data>();

        /// <summary>
        /// Saves the Data into the JSON Text File.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="address"></param>
        public void Save(string name, int number, string address)
        {
            datas.Add(new Data(name, number, address));
            Trace.WriteLine(datas[0].name);
            datas = datas.OrderBy(x => x.name).ToList();
            string json = JsonConvert.SerializeObject(datas.ToArray(), Formatting.Indented);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "data", json);
        }

        //Removes Data from the JSON Text file from the specified index.
        public void Remove(int index)
        {
            datas.RemoveAt(index);
            string json = JsonConvert.SerializeObject(datas.ToArray(), Formatting.Indented);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "data", json);
        }

        //Loads Data from the JSON Text file to the datas list.
        public List<Data> Load()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "data";
            if (!File.Exists(fileName))
                File.Create(fileName).Dispose();

            string jsonString = File.ReadAllText(fileName);

            if (!string.IsNullOrEmpty(jsonString))
                datas = JsonConvert.DeserializeObject<List<Data>>(jsonString);
            return datas;
        }

        //Search for all Contacts that matches the specified name.
        public List<Data> Search(string name)
        {
            List<Data> filteredList = new List<Data>();
            for (int i = 0; i < datas.Count; i++)
            {
                if (string.Equals(datas[i].name, name, StringComparison.OrdinalIgnoreCase))
                {
                    filteredList.Add(datas[i]);
                }
            }
            return filteredList;
        }

        public bool Exist(string name)
        {
            return datas.Exists(x => x.name == name);
        }

        public List<Data> GetDatas()
        {
            return datas;
        }
    }
    class Data
    {
        public string name;
        public int contactNumber;
        public string address;

        public Data(string name, int contactNumber, string address)
        {
            this.name = name;
            this.contactNumber = contactNumber;
            this.address = address;
        }
    }
}