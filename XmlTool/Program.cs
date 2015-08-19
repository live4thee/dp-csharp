namespace Xml2Obj
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;

    // The order of the fields matters. `DataContractSerializer` requires the
    // elements in the XML to be in the same order as the data member order in
    // the data contract.
    //
    // The data members is in *alphabetical* order, if not specified explicitly.
    //
    // c.f. https://stackoverflow.com/questions/28874861/getting-null-value-while-parsing-the-xml-string-in-c-sharp
    [DataContract(Name = "msg", Namespace = "")]
    class Msg
    {
        [DataMember(Name = "id", Order = 1, EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "action", Order = 2, EmitDefaultValue = false)]
        public string Action {get; set; }

        public override string ToString()
        {
            return string.Format("<MSG# id: {0}, action: {1}>", Id, Action);
        }
    }

    class Program
    {
        private static readonly string xmlstring =
@"<msg>
   <id>1</id>
   <action>stop</action>
</msg>";

        static void Main(string[] args)
        {
            var bytes = Encoding.UTF8.GetBytes(xmlstring);

            Msg msg;
            using (var stream = new MemoryStream(bytes))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Msg));
                msg = (Msg)serializer.ReadObject(stream);
            }

            Console.WriteLine(msg);
        }
    }
}

// vim: set et ts=4 sw=4 cin:
