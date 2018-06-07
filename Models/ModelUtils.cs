using DiabetWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DiabetWeb.Models
{
    public class ModelUtils
    {
		public enum SaveModes { All, Foods, Members, Meals }

		public const string FOODS = "foods.xml";
		public const string MEMBERS = "members.xml";
		public const string MEALS = "meals.xml";

		public const string PATH = ".\\data";

		public static string GetXMLFromObject(object obj)
		{
			StringWriter sw = new StringWriter();
			XmlTextWriter tw = null;
			try
			{
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
				tw = new XmlTextWriter(sw);
				serializer.Serialize(tw, obj);
			}
			catch (Exception ex)
			{
				//Handle Exception Code
			}
			finally
			{
				sw.Close();
				if (tw != null)
				{
					tw.Close();
				}
			}
			return sw.ToString();
		}

		public static Object GetObjectFromXML(string xml, Type objectType)
		{
			StringReader strReader = null;
			XmlSerializer serializer = null;
			XmlTextReader xmlReader = null;
			Object obj = null;
			try
			{
				strReader = new StringReader(xml);
				serializer = new XmlSerializer(objectType);
				xmlReader = new XmlTextReader(strReader);
				obj = serializer.Deserialize(xmlReader);
			}
			catch (Exception exp)
			{
				//Handle Exception Code
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
				if (strReader != null)
				{
					strReader.Close();
				}
			}
			return obj;
		}

    }
}
