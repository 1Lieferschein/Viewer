//using System.Collections.Generic;
//using System.Xml;
//using System;
//using System.IO;
//using System.Xml.Schema;
//using System.Text.RegularExpressions;
//using System.Xml.Serialization;

//namespace _1Lieferschein.Models.DeliveryNotes
//{

//    public class DeliveryNote
//    {

//        public DespatchAdvice DespatchAdvice1 { get; set; }

//        public DespatchAdvice DespatchAdvice2 { get; set; }
//    }

//    public partial class DespatchAdvice
//    {


//        public class Group
//        {
//            public string GroupName;
//        }

//        public class Test
//        {
//            static void Main()
//            {
//                Test t = new Test();
//                // Deserialize the file containing unknown elements.
//                t.DeserializeObject("UnknownElements.xml");
//            }
//            private void Serializer_UnknownElement(object sender, XmlElementEventArgs e)
//            {
//                Console.WriteLine("Unknown Element");
//                Console.WriteLine("\t" + e.Element.Name + " " + e.Element.InnerXml);
//                Console.WriteLine("\t LineNumber: " + e.LineNumber);
//                Console.WriteLine("\t LinePosition: " + e.LinePosition);

//                Group x = (Group)e.ObjectBeingDeserialized;
//                Console.WriteLine(x.GroupName);
//                Console.WriteLine(sender.ToString());
//            }
//            private void DeserializeObject(string filename)
//            {
//                XmlSerializer ser = new XmlSerializer(typeof(Group));
//                // Add a delegate to handle unknown element events.
//                ser.UnknownElement += new XmlElementEventHandler(Serializer_UnknownElement);
//                // A FileStream is needed to read the XML document.
//                FileStream fs = new FileStream(filename, FileMode.Open);
//                Group g = (Group)ser.Deserialize(fs);
//                fs.Close();
//            }
//        }



//        //static List<string> xmlnodes = new List<string>();
//        //private static void TraverseNodes(XmlNodeList nodes)
//        //{
//        //    foreach (XmlNode node in nodes)
//        //    {
//        //        List<string> temp = new List<string>();
//        //        temp.Add("Node name: " + node.Name.ToString());
//        //        XmlAttributeCollection xmlAttributes = node.Attributes;

//        //        XmlNodeList nodeList = node.SelectNodes("//*[@X]");

//        //        foreach (XmlAttribute at in xmlAttributes)
//        //        {
//        //            temp.Add("  Atrib: " + at.Name + ": " + at.Value);
//        //        }

//        //        xmlnodes.AddRange(temp);
//        //        TraverseNodes(node.ChildNodes);

//        //        Console.WriteLine(node.ChildNodes);
//        //    }

//        //}

//    }

//}
