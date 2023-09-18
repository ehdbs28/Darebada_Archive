using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("boidUnitPrefab", "boidCount", "spawnRange", "speedRange", "cohesionWeight", "alignmentWeight", "separationWeight", "boundsWeight", "obstacleWeight", "egoWeight", "currUnit", "unitLayer", "ShowBounds", "cameraFollowUnit", "randomColor", "blackAndWhite", "protectiveColor", "enemyPercentage", "GizmoColors", "_fishData", "Fishes", "FishData", "enabled", "name")]
	public class ES3UserType_AquariumBoids : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AquariumBoids() : base(typeof(AquariumBoids)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (AquariumBoids)obj;
			
			writer.WritePropertyByRef("boidUnitPrefab", instance.boidUnitPrefab);
			writer.WriteProperty("boidCount", instance.boidCount, ES3Type_int.Instance);
			writer.WriteProperty("spawnRange", instance.spawnRange, ES3Type_float.Instance);
			writer.WriteProperty("speedRange", instance.speedRange, ES3Type_Vector2.Instance);
			writer.WriteProperty("cohesionWeight", instance.cohesionWeight, ES3Type_float.Instance);
			writer.WriteProperty("alignmentWeight", instance.alignmentWeight, ES3Type_float.Instance);
			writer.WriteProperty("separationWeight", instance.separationWeight, ES3Type_float.Instance);
			writer.WriteProperty("boundsWeight", instance.boundsWeight, ES3Type_float.Instance);
			writer.WriteProperty("obstacleWeight", instance.obstacleWeight, ES3Type_float.Instance);
			writer.WriteProperty("egoWeight", instance.egoWeight, ES3Type_float.Instance);
			writer.WritePropertyByRef("currUnit", instance.currUnit);
			writer.WritePrivateField("unitLayer", instance);
			writer.WriteProperty("ShowBounds", instance.ShowBounds, ES3Type_bool.Instance);
			writer.WriteProperty("cameraFollowUnit", instance.cameraFollowUnit, ES3Type_bool.Instance);
			writer.WriteProperty("randomColor", instance.randomColor, ES3Type_bool.Instance);
			writer.WriteProperty("blackAndWhite", instance.blackAndWhite, ES3Type_bool.Instance);
			writer.WriteProperty("protectiveColor", instance.protectiveColor, ES3Type_bool.Instance);
			writer.WriteProperty("enemyPercentage", instance.enemyPercentage, ES3Type_float.Instance);
			writer.WriteProperty("GizmoColors", instance.GizmoColors, ES3Type_ColorArray.Instance);
			writer.WritePrivateField("_fishData", instance);
			writer.WriteProperty("Fishes", instance.Fishes, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<AuariumBoidUnit>)));
			writer.WriteProperty("FishData", instance.FishData, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(FishDataUnit)));
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (AquariumBoids)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "boidUnitPrefab":
						instance.boidUnitPrefab = reader.Read<AuariumBoidUnit>();
						break;
					case "boidCount":
						instance.boidCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "spawnRange":
						instance.spawnRange = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "speedRange":
						instance.speedRange = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "cohesionWeight":
						instance.cohesionWeight = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "alignmentWeight":
						instance.alignmentWeight = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "separationWeight":
						instance.separationWeight = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "boundsWeight":
						instance.boundsWeight = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "obstacleWeight":
						instance.obstacleWeight = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "egoWeight":
						instance.egoWeight = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "currUnit":
						instance.currUnit = reader.Read<BoidUnit>();
						break;
					case "unitLayer":
					instance = (AquariumBoids)reader.SetPrivateField("unitLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "ShowBounds":
						instance.ShowBounds = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "cameraFollowUnit":
						instance.cameraFollowUnit = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "randomColor":
						instance.randomColor = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "blackAndWhite":
						instance.blackAndWhite = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "protectiveColor":
						instance.protectiveColor = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "enemyPercentage":
						instance.enemyPercentage = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "GizmoColors":
						instance.GizmoColors = reader.Read<UnityEngine.Color[]>(ES3Type_ColorArray.Instance);
						break;
					case "_fishData":
					instance = (AquariumBoids)reader.SetPrivateField("_fishData", reader.Read<FishDataUnit>(), instance);
					break;
					case "Fishes":
						instance.Fishes = reader.Read<System.Collections.Generic.List<AuariumBoidUnit>>();
						break;
					case "FishData":
						instance.FishData = reader.Read<FishDataUnit>();
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


	public class ES3UserType_AquariumBoidsArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AquariumBoidsArray() : base(typeof(AquariumBoids[]), ES3UserType_AquariumBoids.Instance)
		{
			Instance = this;
		}
	}
}