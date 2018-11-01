using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using nkust1023.Models;

namespace nkust1023
{
    class Program
    {
        static void Main(string[] args)
        {
            source.ClassS importService = new source.ClassS();
            var nodes = importService.FindOpenDataFromDb("新北市");
            importService.ImportToDb(nodes);
            showOpenData(nodes);
            Console.ReadKey();
        }
        public void Configure()
        {
            string baseDir = Directory.GetCurrentDirectory();

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(baseDir, "App_Data"));
        }



        private static void showOpenData(List<Class1> nodes)
        {

            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            nodes.GroupBy(node => node.資料集名稱).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"資料集名稱:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                });
        }
    }
}