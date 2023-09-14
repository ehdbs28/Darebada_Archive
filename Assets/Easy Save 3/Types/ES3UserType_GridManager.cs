using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("gridObject", "grids", "width", "height", "_distance")]
	public class ES3UserType_GridManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_GridManager() : base(typeof(GridManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (GridManager)obj;
			
			writer.WritePropertyByRef("gridObject", instance.gridObject);
			writer.WriteProperty("grids", instance.grids, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<GridCell>)));
			writer.WriteProperty("width", instance.width, ES3Type_float.Instance);
			writer.WriteProperty("height", instance.height, ES3Type_float.Instance);
			writer.WritePrivateField("_distance", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (GridManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "gridObject":
						instance.gridObject = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "grids":
						instance.grids = reader.Read<System.Collections.Generic.List<GridCell>>();
						break;
					case "width":
						instance.width = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "height":
						instance.height = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "_distance":
					instance = (GridManager)reader.SetPrivateField("_distance", reader.Read<System.Single>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_GridManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_GridManagerArray() : base(typeof(GridManager[]), ES3UserType_GridManager.Instance)
		{
			Instance = this;
		}
	}
}