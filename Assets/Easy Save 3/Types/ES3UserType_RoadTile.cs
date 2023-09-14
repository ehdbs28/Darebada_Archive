using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("isCollision", "_layerMask", "_collider", "_wallLayer")]
	public class ES3UserType_RoadTile : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_RoadTile() : base(typeof(RoadTile)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (RoadTile)obj;
			
			writer.WriteProperty("isCollision", instance.isCollision, ES3Type_bool.Instance);
			writer.WritePrivateField("_layerMask", instance);
			writer.WritePrivateFieldByRef("_collider", instance);
			writer.WritePrivateField("_wallLayer", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (RoadTile)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "isCollision":
						instance.isCollision = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "_layerMask":
					instance = (RoadTile)reader.SetPrivateField("_layerMask", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "_collider":
					instance = (RoadTile)reader.SetPrivateField("_collider", reader.Read<UnityEngine.Collider>(), instance);
					break;
					case "_wallLayer":
					instance = (RoadTile)reader.SetPrivateField("_wallLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_RoadTileArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_RoadTileArray() : base(typeof(RoadTile[]), ES3UserType_RoadTile.Instance)
		{
			Instance = this;
		}
	}
}