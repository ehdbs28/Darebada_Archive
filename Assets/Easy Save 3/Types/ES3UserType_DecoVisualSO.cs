using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("Name", "mesh", "mainMat", "sprite", "name")]
	public class ES3UserType_DecoVisualSO : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_DecoVisualSO() : base(typeof(DecoVisualSO)){ Instance = this; priority = 1; }


		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (DecoVisualSO)obj;
			
			writer.WriteProperty("Name", instance.Name, ES3Type_string.Instance);
			writer.WritePropertyByRef("mesh", instance.mesh);
			writer.WritePropertyByRef("mainMat", instance.mainMat);
			writer.WritePropertyByRef("sprite", instance.sprite);
			writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (DecoVisualSO)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Name":
						instance.Name = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "mesh":
						instance.mesh = reader.Read<UnityEngine.Mesh>(ES3Type_Mesh.Instance);
						break;
					case "mainMat":
						instance.mainMat = reader.Read<UnityEngine.Material>(ES3Type_Material.Instance);
						break;
					case "sprite":
						instance.sprite = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "name":
						instance.name = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DecoVisualSOArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DecoVisualSOArray() : base(typeof(DecoVisualSO[]), ES3UserType_DecoVisualSO.Instance)
		{
			Instance = this;
		}
	}
}