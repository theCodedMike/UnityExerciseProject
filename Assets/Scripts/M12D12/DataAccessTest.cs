using System.Xml;
using UnityEngine;

namespace M12D12
{
    public class DataAccessTest : MonoBehaviour
    {
        public TextAsset textAsset;

        // Start is called before the first frame update
        void Start()
        {
            // 1. data.txt
            //TestTxt();

            // 2.json
            //TestJson();

            // 3.XML
            //XmlTest();

            // 4.CSV
            CsvTest();
        }

        void TestTxt()
        {
            print(textAsset.text);

            string content = Resources.Load<TextAsset>("Data/data").text;
            print(content);
        }

        void TestJson()
        {
            Person person = new Person("ZhangSan", 20, false);
            string jsonP = JsonUtility.ToJson(person);
            print(jsonP);

            Person p2 = JsonUtility.FromJson<Person>(jsonP);
            print(p2);
        }


        void XmlTest()
        {
            //WriteXml();
            ReadXml();
        }

        void CsvTest()
        {
            TextAsset csvFile = Resources.Load<TextAsset>("Data/csv_data");
        }

        void WriteXml()
        {
            XmlDocument xml = new XmlDocument();

            XmlElement student = xml.CreateElement("Student");
            xml.AppendChild(student);

            XmlElement name = xml.CreateElement("Name");
            name.InnerText = "张三丰";
            student.AppendChild(name);

            XmlElement sex = xml.CreateElement("Sex");
            sex.InnerText = "男";
            student.AppendChild(sex);

            XmlElement age = xml.CreateElement("Age");
            age.InnerText = "20";
            student.AppendChild(age);
            
            string path = Application.persistentDataPath + "/xml_data.xml";
            print(path);
            xml.Save(path);
        }

        void ReadXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Application.persistentDataPath + "/xml_data.xml");
            //xml.LoadXml(); // Don't use this API

            XmlNode student = xml.SelectSingleNode("Student");
            XmlNode name = student.SelectSingleNode("Name");
            print(name.InnerText);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
