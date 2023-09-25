using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("terrainData", "enabled", "isTrigger", "contactOffset", "hasModifiableContacts", "sharedMaterial", "material", "name")]
	public class ES3UserType_TerrainCollider : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_TerrainCollider() : base(typeof(UnityEngine.TerrainCollider)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.TerrainCollider)obj;
			
			writer.WritePropertyByRef("terrainData", instance.terrainData);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("isTrigger", instance.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("contactOffset", instance.contactOffset, ES3Type_float.Instance);
			writer.WriteProperty("hasModifiableContacts", instance.hasModifiableContacts, ES3Type_bool.Instance);
			writer.WritePropertyByRef("sharedMaterial", instance.sharedMaterial);
			writer.WritePropertyByRef("material", instance.material);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.TerrainCollider)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "terrainData":
						instance.terrainData = reader.Read<UnityEngine.TerrainData>();
						break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isTrigger":
						instance.isTrigger = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "contactOffset":
						instance.contactOffset = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "hasModifiableContacts":
						instance.hasModifiableContacts = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "sharedMaterial":
						instance.sharedMaterial = reader.Read<UnityEngine.PhysicMaterial>(ES3Type_PhysicMaterial.Instance);
						break;
					case "material":
						instance.material = reader.Read<UnityEngine.PhysicMaterial>(ES3Type_PhysicMaterial.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_TerrainColliderArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_TerrainColliderArray() : base(typeof(UnityEngine.TerrainCollider[]), ES3UserType_TerrainCollider.Instance)
		{
			Instance = this;
		}
	}
}