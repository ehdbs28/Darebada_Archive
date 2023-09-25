using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("employee", "level", "isCollision", "_layerMask", "_collider", "_wallLayer")]
	public class ES3UserType_Shop : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Shop() : base(typeof(Shop)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Shop)obj;
			
			writer.WriteProperty("employee", instance.employee, ES3Type_int.Instance);
			writer.WriteProperty("level", instance.level, ES3Type_int.Instance);
			writer.WriteProperty("isCollision", instance.isCollision, ES3Type_bool.Instance);
			writer.WritePrivateField("_layerMask", instance);
			writer.WritePrivateFieldByRef("_collider", instance);
			writer.WritePrivateField("_wallLayer", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Shop)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "employee":
						instance.employee = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "level":
						instance.level = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "isCollision":
						instance.isCollision = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "_layerMask":
					instance = (Shop)reader.SetPrivateField("_layerMask", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "_collider":
					instance = (Shop)reader.SetPrivateField("_collider", reader.Read<UnityEngine.Collider>(), instance);
					break;
					case "_wallLayer":
					instance = (Shop)reader.SetPrivateField("_wallLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ShopArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ShopArray() : base(typeof(Shop[]), ES3UserType_Shop.Instance)
		{
			Instance = this;
		}
	}
}