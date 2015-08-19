namespace Xml2Obj
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;


    // XML deserializatoin with 'xsd' tool.
    // c.f. https://stackoverflow.com/questions/3187444/convert-xml-string-to-object
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(msg));
            msg result = (msg)serializer.Deserialize(new XmlTextReader("msg.xml"));


            Console.WriteLine("<MSG# id: {0}, action: {1}>", result.id, result.action);
        }
    }
}

// vim: set et ts=4 sw=4 cin:
