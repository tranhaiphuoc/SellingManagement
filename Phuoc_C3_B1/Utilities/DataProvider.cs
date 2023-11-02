using System.Xml;


namespace Phuoc_C3_B1
{
    class DataProvider
    {
        private static readonly XmlDocument _doc = new XmlDocument();
        public static XmlNode NodeRoot;


        public static string PathData { get; set; }


        public static void Open()
        {
            _doc.Load(PathData);
            NodeRoot = _doc.DocumentElement;
        }

        public static void Close()
        {
            _doc.Save(PathData);
        }

        public static XmlNode GetNode(string xpath)
        {
            return NodeRoot.SelectSingleNode(xpath);
        }

        public static XmlNode GetNode(string xpath, int index)
        {
            XmlNode temp = _doc.SelectSingleNode(xpath);

            for (int i = 0; i < index; i++)
            {
                temp = temp.NextSibling;
            }
            return temp;
        }

        public static XmlNodeList GetNodeList(string xpath)
        {
            return NodeRoot.SelectNodes(xpath);
        }

        public static XmlNode CreateNode(string tagName)
        {
            return _doc.CreateElement(tagName);
        }

        public static XmlAttribute CreateAttr(string name)
        {
            return _doc.CreateAttribute(name);
        }

        public static void AppendNode(XmlNode node, XmlNode newNode)
        {
            node.AppendChild(newNode);
        }

        public static void PrependNode(XmlNode node, XmlNode newNode)
        {
            node.PrependChild(newNode);
        }

        public static void InsertNode(XmlNode childNode, XmlNode refNode)
        {
            NodeRoot.InsertAfter(childNode, refNode);
        }

        public static void RemoveNode(XmlNode refNode)
        {
            XmlNode parent = refNode.ParentNode;
            parent.RemoveChild(refNode);
        }
    }
}
