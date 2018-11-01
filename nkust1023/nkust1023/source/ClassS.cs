using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using nkust1023.Models;

namespace nkust1023.source
{
    class ClassS
    {
        public Repository.ClassR _RRR;

        public ClassS()
        {
            _RRR = new Repository.ClassR();
        }
        public List<Class1> FindOpenData()
        {
            List<Class1> result = new List<Class1>();

            string baseDir = Directory.GetCurrentDirectory();


            var xml = XElement.Load(@"C:/Users/mm930/source/repos/nkust1023/nkust1023/nkust1023/data/datagovtw_dataset_20181025.xml");


            //XNamespace gml = @"http://www.opengis.net/gml/3.2";
            //XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";
            var nodes = xml.Descendants("node").ToList();

            result = nodes
                .Where(x => !x.IsEmpty).ToList()
                .Select(node =>
                {
                    Class1 item = new Class1();
                    item.id = int.Parse(getValue(node, "id"));
                    item.資料集名稱 = getValue(node, "資料集名稱");
                    item.服務分類 = getValue(node, "服務分類");
                    item.主要欄位說明 = getValue(node, "主要欄位說明");
                    return item;
                }).ToList();
            return result;
        }
        public List<Class1> FindOpenDataFromDb(string name)
        {
            return _RRR.SelectAll(name);
        }
        public void ImportToDb(List<Class1> openDatas)
        {
            //Repository.ClassR Repository = new Repository.ClassR();
            //openDatas.ForEach(item =>
            //{
            //    Repository.Insert(item);
            //});
            openDatas.ForEach(item =>
            {
                _RRR.Insert(item);
            });
        }
        private string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }
    }
}