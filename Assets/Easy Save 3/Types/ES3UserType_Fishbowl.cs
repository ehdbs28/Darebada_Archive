using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("boids", "boidObjects", "decoTrs", "fishs", "boidObject", "decoObject", "decoController", "level", "isCollision", "_layerMask", "_collider", "_wallLayer")]
	public class ES3UserType_Fishbowl : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Fishbowl() : base(typeof(Fishbowl)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Fishbowl)obj;
			
			writer.WriteProperty("boids", instance.boids, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.Dictionary<System.String, AquariumBoids>)));
			writer.WriteProperty("boidObjects", instance.boidObjects, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<UnityEngine.GameObject>)));
			writer.WriteProperty("decoTrs", instance.decoTrs, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<UnityEngine.Transform>)));
			writer.WriteProperty("fishs", instance.fishs, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<AuariumBoidUnit>)));
			writer.WritePropertyByRef("boidObject", instance.boidObject);
			writer.WritePropertyByRef("decoObject", instance.decoObject);
			writer.WritePropertyByRef("decoController", instance.decoController);
			writer.WriteProperty("level", instance.level, ES3Type_int.Instance);
			writer.WriteProperty("isCollision", instance.isCollision, ES3Type_bool.Instance);
			writer.WritePrivateField("_layerMask", instance);
			writer.WritePrivateFieldByRef("_collider", instance);
			writer.WritePrivateField("_wallLayer", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Fishbowl)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "boids":
						instance.boids = reader.Read<System.Collections.Generic.Dictionary<System.String, AquariumBoids>>();
						break;
					case "boidObjects":
						instance.boidObjects = reader.Read<System.Collections.Generic.List<UnityEngine.GameObject>>();
						break;
					case "decoTrs":
						instance.decoTrs = reader.Read<System.Collections.Generic.List<UnityEngine.Transform>>();
						break;
					case "fishs":
						instance.fishs = reader.Read<System.Collections.Generic.List<AuariumBoidUnit>>();
						break;
					case "boidObject":
						instance.boidObject = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "decoObject":
						instance.decoObject = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "decoController":
						instance.decoController = reader.Read<DecoController>();
						break;
					case "level":
						instance.level = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "isCollision":
						instance.isCollision = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "_layerMask":
					instance = (Fishbowl)reader.SetPrivateField("_layerMask", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "_collider":
					instance = (Fishbowl)reader.SetPrivateField("_collider", reader.Read<UnityEngine.Collider>(), instance);
					break;
					case "_wallLayer":
					instance = (Fishbowl)reader.SetPrivateField("_wallLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_FishbowlArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_FishbowlArray() : base(typeof(Fishbowl[]), ES3UserType_Fishbowl.Instance)
		{
			Instance = this;
		}
	}
}