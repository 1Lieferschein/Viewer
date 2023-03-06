using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;
using System.Xml.Schema;

namespace _1Lieferschein.Models.DeliveryNotes
{

    public class DeliveryNote
    {

        public DespatchAdvice DespatchAdvice1 { get; set; }

        public DespatchAdvice DespatchAdvice2 { get; set; }
    }

    public partial class DespatchAdvice
    {






        //static List<string> xmlnodes = new List<string>();
        //private static void TraverseNodes(XmlNodeList nodes)
        //{
        //    foreach (XmlNode node in nodes)
        //    {
        //        List<string> temp = new List<string>();
        //        temp.Add("Node name: " + node.Name.ToString());
        //        XmlAttributeCollection xmlAttributes = node.Attributes;

        //        XmlNodeList nodeList = node.SelectNodes("//*[@X]");

        //        foreach (XmlAttribute at in xmlAttributes)
        //        {
        //            temp.Add("  Atrib: " + at.Name + ": " + at.Value);
        //        }

        //        xmlnodes.AddRange(temp);
        //        TraverseNodes(node.ChildNodes);

        //        Console.WriteLine(node.ChildNodes);
        //    }

        //}
        
    }

}
