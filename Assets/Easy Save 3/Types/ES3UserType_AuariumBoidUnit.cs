using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("myBoids", "neighbours", "targetVec", "egoVector", "speed", "additionalSpeed", "isEnemy", "myMeshRenderer", "myTrailRenderer", "myColor", "obstacleDistance", "FOVAngle", "maxNeighbourCount", "neighbourDistance", "boidUnitLayer", "obstacleLayer", "findNeighbourCoroutine", "calculateEgoVectorCoroutine", "_radius", "_maxDistance", "_layerMask", "_bait", "hit", "IsBite", "IsSensed", "_baitVec", "_unitData", "_skinnedMR", "boundsOffset", "_isMove", "IsMove", "enabled", "name")]
	public class ES3UserType_AuariumBoidUnit : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AuariumBoidUnit() : base(typeof(AuariumBoidUnit)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (AuariumBoidUnit)obj;
			
			writer.WritePropertyByRef("myBoids", instance.myBoids);
			writer.WritePrivateField("neighbours", instance);
			writer.WriteProperty("targetVec", instance.targetVec, ES3Type_Vector3.Instance);
			writer.WritePrivateField("egoVector", instance);
			writer.WritePrivateField("speed", instance);
			writer.WritePrivateField("additionalSpeed", instance);
			writer.WritePrivateField("isEnemy", instance);
			writer.WritePrivateFieldByRef("myMeshRenderer", instance);
			writer.WritePrivateFieldByRef("myTrailRenderer", instance);
			writer.WritePrivateField("myColor", instance);
			writer.WritePrivateField("obstacleDistance", instance);
			writer.WritePrivateField("FOVAngle", instance);
			writer.WritePrivateField("maxNeighbourCount", instance);
			writer.WritePrivateField("neighbourDistance", instance);
			writer.WritePrivateField("boidUnitLayer", instance);
			writer.WritePrivateField("obstacleLayer", instance);
			writer.WritePrivateField("findNeighbourCoroutine", instance);
			writer.WritePrivateField("calculateEgoVectorCoroutine", instance);
			writer.WritePrivateField("_radius", instance);
			writer.WritePrivateField("_maxDistance", instance);
			writer.WritePrivateField("_layerMask", instance);
			writer.WritePrivateFieldByRef("_bait", instance);
			writer.WritePrivateField("hit", instance);
			writer.WriteProperty("IsBite", instance.IsBite, ES3Type_bool.Instance);
			writer.WriteProperty("IsSensed", instance.IsSensed, ES3Type_bool.Instance);
			writer.WritePrivateField("_baitVec", instance);
			writer.WritePrivateField("_unitData", instance);
			writer.WritePrivateFieldByRef("_skinnedMR", instance);
			writer.WritePrivateField("boundsOffset", instance);
			writer.WritePrivateField("_isMove", instance);
			writer.WriteProperty("IsMove", instance.IsMove, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (AuariumBoidUnit)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "myBoids":
						instance.myBoids = reader.Read<AquariumBoids>(ES3UserType_AquariumBoids.Instance);
						break;
					case "neighbours":
					instance = (AuariumBoidUnit)reader.SetPrivateField("neighbours", reader.Read<System.Collections.Generic.List<AuariumBoidUnit>>(), instance);
					break;
					case "targetVec":
						instance.targetVec = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "egoVector":
					instance = (AuariumBoidUnit)reader.SetPrivateField("egoVector", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "speed":
					instance = (AuariumBoidUnit)reader.SetPrivateField("speed", reader.Read<System.Single>(), instance);
					break;
					case "additionalSpeed":
					instance = (AuariumBoidUnit)reader.SetPrivateField("additionalSpeed", reader.Read<System.Single>(), instance);
					break;
					case "isEnemy":
					instance = (AuariumBoidUnit)reader.SetPrivateField("isEnemy", reader.Read<System.Boolean>(), instance);
					break;
					case "myMeshRenderer":
					instance = (AuariumBoidUnit)reader.SetPrivateField("myMeshRenderer", reader.Read<UnityEngine.MeshRenderer>(), instance);
					break;
					case "myTrailRenderer":
					instance = (AuariumBoidUnit)reader.SetPrivateField("myTrailRenderer", reader.Read<UnityEngine.TrailRenderer>(), instance);
					break;
					case "myColor":
					instance = (AuariumBoidUnit)reader.SetPrivateField("myColor", reader.Read<UnityEngine.Color>(), instance);
					break;
					case "obstacleDistance":
					instance = (AuariumBoidUnit)reader.SetPrivateField("obstacleDistance", reader.Read<System.Single>(), instance);
					break;
					case "FOVAngle":
					instance = (AuariumBoidUnit)reader.SetPrivateField("FOVAngle", reader.Read<System.Single>(), instance);
					break;
					case "maxNeighbourCount":
					instance = (AuariumBoidUnit)reader.SetPrivateField("maxNeighbourCount", reader.Read<System.Single>(), instance);
					break;
					case "neighbourDistance":
					instance = (AuariumBoidUnit)reader.SetPrivateField("neighbourDistance", reader.Read<System.Single>(), instance);
					break;
					case "boidUnitLayer":
					instance = (AuariumBoidUnit)reader.SetPrivateField("boidUnitLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "obstacleLayer":
					instance = (AuariumBoidUnit)reader.SetPrivateField("obstacleLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "findNeighbourCoroutine":
					instance = (AuariumBoidUnit)reader.SetPrivateField("findNeighbourCoroutine", reader.Read<UnityEngine.Coroutine>(), instance);
					break;
					case "calculateEgoVectorCoroutine":
					instance = (AuariumBoidUnit)reader.SetPrivateField("calculateEgoVectorCoroutine", reader.Read<UnityEngine.Coroutine>(), instance);
					break;
					case "_radius":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_radius", reader.Read<System.Single>(), instance);
					break;
					case "_maxDistance":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_maxDistance", reader.Read<System.Single>(), instance);
					break;
					case "_layerMask":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_layerMask", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "_bait":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_bait", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "hit":
					instance = (AuariumBoidUnit)reader.SetPrivateField("hit", reader.Read<UnityEngine.RaycastHit>(), instance);
					break;
					case "IsBite":
						instance.IsBite = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "IsSensed":
						instance.IsSensed = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "_baitVec":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_baitVec", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "_unitData":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_unitData", reader.Read<FishDataUnit>(), instance);
					break;
					case "_skinnedMR":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_skinnedMR", reader.Read<UnityEngine.SkinnedMeshRenderer>(), instance);
					break;
					case "boundsOffset":
					instance = (AuariumBoidUnit)reader.SetPrivateField("boundsOffset", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "_isMove":
					instance = (AuariumBoidUnit)reader.SetPrivateField("_isMove", reader.Read<System.Boolean>(), instance);
					break;
					case "IsMove":
						instance.IsMove = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AuariumBoidUnitArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AuariumBoidUnitArray() : base(typeof(AuariumBoidUnit[]), ES3UserType_AuariumBoidUnit.Instance)
		{
			Instance = this;
		}
	}
}