using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("terrainData", "treeDistance", "treeBillboardDistance", "treeCrossFadeLength", "treeMaximumFullLODCount", "detailObjectDistance", "detailObjectDensity", "heightmapPixelError", "heightmapMaximumLOD", "basemapDistance", "lightmapIndex", "realtimeLightmapIndex", "lightmapScaleOffset", "realtimeLightmapScaleOffset", "keepUnusedRenderingResources", "shadowCastingMode", "reflectionProbeUsage", "materialTemplate", "drawHeightmap", "allowAutoConnect", "groupingID", "drawInstanced", "drawTreesAndFoliage", "patchBoundsMultiplier", "treeLODBiasMultiplier", "collectDetailPatches", "editorRenderFlags", "bakeLightProbesForTrees", "deringLightProbesForTrees", "preserveTreePrototypeLayers", "renderingLayerMask", "enabled", "name")]
	public class ES3UserType_Terrain : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Terrain() : base(typeof(UnityEngine.Terrain)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.Terrain)obj;
			
			writer.WritePropertyByRef("terrainData", instance.terrainData);
			writer.WriteProperty("treeDistance", instance.treeDistance, ES3Type_float.Instance);
			writer.WriteProperty("treeBillboardDistance", instance.treeBillboardDistance, ES3Type_float.Instance);
			writer.WriteProperty("treeCrossFadeLength", instance.treeCrossFadeLength, ES3Type_float.Instance);
			writer.WriteProperty("treeMaximumFullLODCount", instance.treeMaximumFullLODCount, ES3Type_int.Instance);
			writer.WriteProperty("detailObjectDistance", instance.detailObjectDistance, ES3Type_float.Instance);
			writer.WriteProperty("detailObjectDensity", instance.detailObjectDensity, ES3Type_float.Instance);
			writer.WriteProperty("heightmapPixelError", instance.heightmapPixelError, ES3Type_float.Instance);
			writer.WriteProperty("heightmapMaximumLOD", instance.heightmapMaximumLOD, ES3Type_int.Instance);
			writer.WriteProperty("basemapDistance", instance.basemapDistance, ES3Type_float.Instance);
			writer.WriteProperty("lightmapIndex", instance.lightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("realtimeLightmapIndex", instance.realtimeLightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("lightmapScaleOffset", instance.lightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("realtimeLightmapScaleOffset", instance.realtimeLightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("keepUnusedRenderingResources", instance.keepUnusedRenderingResources, ES3Type_bool.Instance);
			writer.WriteProperty("shadowCastingMode", instance.shadowCastingMode, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.Rendering.ShadowCastingMode)));
			writer.WriteProperty("reflectionProbeUsage", instance.reflectionProbeUsage, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.Rendering.ReflectionProbeUsage)));
			writer.WritePropertyByRef("materialTemplate", instance.materialTemplate);
			writer.WriteProperty("drawHeightmap", instance.drawHeightmap, ES3Type_bool.Instance);
			writer.WriteProperty("allowAutoConnect", instance.allowAutoConnect, ES3Type_bool.Instance);
			writer.WriteProperty("groupingID", instance.groupingID, ES3Type_int.Instance);
			writer.WriteProperty("drawInstanced", instance.drawInstanced, ES3Type_bool.Instance);
			writer.WriteProperty("drawTreesAndFoliage", instance.drawTreesAndFoliage, ES3Type_bool.Instance);
			writer.WriteProperty("patchBoundsMultiplier", instance.patchBoundsMultiplier, ES3Type_Vector3.Instance);
			writer.WriteProperty("treeLODBiasMultiplier", instance.treeLODBiasMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("collectDetailPatches", instance.collectDetailPatches, ES3Type_bool.Instance);
			writer.WriteProperty("editorRenderFlags", instance.editorRenderFlags, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.TerrainRenderFlags)));
			writer.WriteProperty("bakeLightProbesForTrees", instance.bakeLightProbesForTrees, ES3Type_bool.Instance);
			writer.WriteProperty("deringLightProbesForTrees", instance.deringLightProbesForTrees, ES3Type_bool.Instance);
			writer.WriteProperty("preserveTreePrototypeLayers", instance.preserveTreePrototypeLayers, ES3Type_bool.Instance);
			writer.WriteProperty("renderingLayerMask", instance.renderingLayerMask, ES3Type_uint.Instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.Terrain)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "terrainData":
						instance.terrainData = reader.Read<UnityEngine.TerrainData>();
						break;
					case "treeDistance":
						instance.treeDistance = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "treeBillboardDistance":
						instance.treeBillboardDistance = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "treeCrossFadeLength":
						instance.treeCrossFadeLength = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "treeMaximumFullLODCount":
						instance.treeMaximumFullLODCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "detailObjectDistance":
						instance.detailObjectDistance = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "detailObjectDensity":
						instance.detailObjectDensity = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "heightmapPixelError":
						instance.heightmapPixelError = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "heightmapMaximumLOD":
						instance.heightmapMaximumLOD = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "basemapDistance":
						instance.basemapDistance = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "lightmapIndex":
						instance.lightmapIndex = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "realtimeLightmapIndex":
						instance.realtimeLightmapIndex = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "lightmapScaleOffset":
						instance.lightmapScaleOffset = reader.Read<UnityEngine.Vector4>(ES3Type_Vector4.Instance);
						break;
					case "realtimeLightmapScaleOffset":
						instance.realtimeLightmapScaleOffset = reader.Read<UnityEngine.Vector4>(ES3Type_Vector4.Instance);
						break;
					case "keepUnusedRenderingResources":
						instance.keepUnusedRenderingResources = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "shadowCastingMode":
						instance.shadowCastingMode = reader.Read<UnityEngine.Rendering.ShadowCastingMode>();
						break;
					case "reflectionProbeUsage":
						instance.reflectionProbeUsage = reader.Read<UnityEngine.Rendering.ReflectionProbeUsage>();
						break;
					case "materialTemplate":
						instance.materialTemplate = reader.Read<UnityEngine.Material>(ES3Type_Material.Instance);
						break;
					case "drawHeightmap":
						instance.drawHeightmap = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "allowAutoConnect":
						instance.allowAutoConnect = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "groupingID":
						instance.groupingID = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "drawInstanced":
						instance.drawInstanced = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "drawTreesAndFoliage":
						instance.drawTreesAndFoliage = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "patchBoundsMultiplier":
						instance.patchBoundsMultiplier = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "treeLODBiasMultiplier":
						instance.treeLODBiasMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "collectDetailPatches":
						instance.collectDetailPatches = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "editorRenderFlags":
						instance.editorRenderFlags = reader.Read<UnityEngine.TerrainRenderFlags>();
						break;
					case "bakeLightProbesForTrees":
						instance.bakeLightProbesForTrees = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "deringLightProbesForTrees":
						instance.deringLightProbesForTrees = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "preserveTreePrototypeLayers":
						instance.preserveTreePrototypeLayers = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "renderingLayerMask":
						instance.renderingLayerMask = reader.Read<System.UInt32>(ES3Type_uint.Instance);
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


	public class ES3UserType_TerrainArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_TerrainArray() : base(typeof(UnityEngine.Terrain[]), ES3UserType_Terrain.Instance)
		{
			Instance = this;
		}
	}
}