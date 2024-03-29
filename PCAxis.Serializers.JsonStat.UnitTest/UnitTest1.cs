using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PCAxis.Paxiom;
using System.Globalization;
using System.IO;
using System.Text;

namespace PCAxis.Serializers.JsonStat.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
       

		[TestMethod]
		public void TestMethod1()
		{

			CultureInfo ci = new CultureInfo("fi-FI");
			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

			PCAxis.Paxiom.IPXModelBuilder builder = new PXFileBuilder();
			
			builder.SetPath("PR0101B3.px");
			builder.BuildForSelection();

			PXMeta meta = builder.Model.Meta;
			builder.BuildForPresentation(Selection.SelectAll(meta));
			
			PXModel myModel = builder.Model;



			var dsf = myModel.IsComplete;
			string actual = "";

			using (MemoryStream memStream = new MemoryStream(1000))
			{
				JsonStatSerializer jss = new JsonStatSerializer();
				jss.Serialize(myModel, memStream);

				actual = Encoding.UTF8.GetString(memStream.GetBuffer(), 0, (int)memStream.Length);
			}


			var expected = "{\"dataset\":{\"dimension\":{\"Product group\":{\"label\":\"Product group\",\"category\":{\"index\":{\"01\":0,\"01.1\":1,\"01.1.1\":2,\"01.1.2\":3,\"01.1.3\":4,\"01.1.4\":5,\"01.1.5\":6,\"01.1.6\":7},\"label\":{\"01\":\"01 Food and non-alcoholic beverages\",\"01.1\":\"01.1 Food\",\"01.1.1\":\"01.1.1 Bread and cereals\",\"01.1.2\":\"01.1.2 Meat\",\"01.1.3\":\"01.1.3 Fish\",\"01.1.4\":\"01.1.4 Milk, cheese and eggs\",\"01.1.5\":\"01.1.5 Oils and fats\",\"01.1.6\":\"01.1.6 Fruit\"}}},\"period\":{\"label\":\"period\",\"category\":{\"index\":{\"0\":0,\"1\":1,\"2\":2,\"3\":3,\"4\":4},\"label\":{\"0\":\"2006M01\",\"1\":\"2006M02\",\"2\":\"2006M03\",\"3\":\"2006M04\",\"4\":\"2006M05\"}}},\"id\":[\"Product group\",\"period\"],\"size\":[8,5],\"role\":{\"time\":[\"period\"]}},\"label\":\"Consumer Price Index (CPI) (by COICOP), 1980=100 PxcMetaTitleBy Product group PxcMetaTitleAnd period\",\"source\":\"Statistics Sweden\",\"updated\":\"2006-07-05T12:21:00Z\",\"value\":[237,238.6,238.7,238.3,242.1,246.1,247.9,248,247.6,251.9,245.3,243.6,245.3,244.7,245,229.3,232.2,232.2,232,232.9,316.3,314.9,321.9,315.3,332.8,266.9,267.4,267,266.3,267.8,208.8,211.2,211.8,211.5,211.7,232.2,227,231.8,232.8,238.2],\"extension\":{\"px\":{\"infofile\":\"PR0101\",\"decimals\":1}}}}";

			//Act

			//Assert
			//Check their equality.
			Assert.AreEqual(expected, actual);
			
		}


	}
}
