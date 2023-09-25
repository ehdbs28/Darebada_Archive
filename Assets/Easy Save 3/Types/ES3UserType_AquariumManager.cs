using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("facilityParent", "fishBowls", "decoVisuals", "state", "floor", "_walls", "_fishBowlObject", "_snackShopObject", "_roadTileObject", "_cleaningParticle", "_cleaningParticleSystemObject", "roadSurfaces", "facilityLayer", "gridLayer", "facilityObj", "_build")]
	public class ES3UserType_AquariumManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AquariumManager() : base(typeof(AquariumManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (AquariumManager)obj;
			
			writer.WritePropertyByRef("facilityParent", instance.facilityParent);
			writer.WriteProperty("fishBowls", instance.fishBowls, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<UnityEngine.GameObject>)));
			writer.WriteProperty("decoVisuals", instance.decoVisuals, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<DecoVisualSO>)));
			writer.WriteProperty("state", instance.state, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(AquariumManager.STATE)));
			writer.WritePrivateFieldByRef("floor", instance);
			writer.WritePrivateFieldByRef("_walls", instance);
			writer.WritePrivateFieldByRef("_fishBowlObject", instance);
			writer.WritePrivateFieldByRef("_snackShopObject", instance);
			writer.WritePrivateFieldByRef("_roadTileObject", instance);
			writer.WritePrivateFieldByRef("_cleaningParticle", instance);
			writer.WritePrivateFieldByRef("_cleaningParticleSystemObject", instance);
			writer.WriteProperty("roadSurfaces", instance.roadSurfaces, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<UnityEngine.AI.NavMeshSurface>)));
			writer.WriteProperty("facilityLayer", instance.facilityLayer, ES3Type_LayerMask.Instance);
			writer.WriteProperty("gridLayer", instance.gridLayer, ES3Type_LayerMask.Instance);
			writer.WritePropertyByRef("facilityObj", instance.facilityObj);
			writer.WritePrivateFieldByRef("_build", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (AquariumManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "facilityParent":
						instance.facilityParent = reader.Read<UnityEngine.Transform>(ES3Type_Transform.Instance);
						break;
					case "fishBowls":
						instance.fishBowls = reader.Read<System.Collections.Generic.List<UnityEngine.GameObject>>();
						break;
					case "decoVisuals":
						instance.decoVisuals = reader.Read<System.Collections.Generic.List<DecoVisualSO>>();
						break;
					case "state":
						instance.state = reader.Read<AquariumManager.STATE>();
						break;
					case "floor":
					instance = (AquariumManager)reader.SetPrivateField("floor", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_walls":
					instance = (AquariumManager)reader.SetPrivateField("_walls", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_fishBowlObject":
					instance = (AquariumManager)reader.SetPrivateField("_fishBowlObject", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_snackShopObject":
					instance = (AquariumManager)reader.SetPrivateField("_snackShopObject", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_roadTileObject":
					instance = (AquariumManager)reader.SetPrivateField("_roadTileObject", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_cleaningParticle":
					instance = (AquariumManager)reader.SetPrivateField("_cleaningParticle", reader.Read<UnityEngine.ParticleSystem>(), instance);
					break;
					case "_cleaningParticleSystemObject":
					instance = (AquariumManager)reader.SetPrivateField("_cleaningParticleSystemObject", reader.Read<UnityEngine.ParticleSystem>(), instance);
					break;
					case "roadSurfaces":
						instance.roadSurfaces = reader.Read<System.Collections.Generic.List<UnityEngine.AI.NavMeshSurface>>();
						break;
					case "facilityLayer":
						instance.facilityLayer = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "gridLayer":
						instance.gridLayer = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "facilityObj":
						instance.facilityObj = reader.Read<Facility>();
						break;
					case "_build":
					instance = (AquariumManager)reader.SetPrivateField("_build", reader.Read<BuildFacility>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AquariumManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AquariumManagerArray() : base(typeof(AquariumManager[]), ES3UserType_AquariumManager.Instance)
		{
			Instance = this;
		}
	}
}