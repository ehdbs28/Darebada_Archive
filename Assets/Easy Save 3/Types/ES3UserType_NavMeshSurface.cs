using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("m_AgentTypeID", "m_CollectObjects", "m_Size", "m_Center", "m_LayerMask", "m_UseGeometry", "m_DefaultArea", "m_IgnoreNavMeshAgent", "m_IgnoreNavMeshObstacle", "m_OverrideTileSize", "m_TileSize", "m_OverrideVoxelSize", "m_VoxelSize", "m_BuildHeightMesh", "m_NavMeshData", "m_NavMeshDataInstance", "m_LastPosition", "m_LastRotation", "agentTypeID", "collectObjects", "size", "center", "layerMask", "useGeometry", "defaultArea", "ignoreNavMeshAgent", "ignoreNavMeshObstacle", "overrideTileSize", "tileSize", "overrideVoxelSize", "voxelSize", "buildHeightMesh", "navMeshData", "enabled", "name")]
	public class ES3UserType_NavMeshSurface : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_NavMeshSurface() : base(typeof(UnityEngine.AI.NavMeshSurface)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.AI.NavMeshSurface)obj;
			
			writer.WritePrivateField("m_AgentTypeID", instance);
			writer.WritePrivateField("m_CollectObjects", instance);
			writer.WritePrivateField("m_Size", instance);
			writer.WritePrivateField("m_Center", instance);
			writer.WritePrivateField("m_LayerMask", instance);
			writer.WritePrivateField("m_UseGeometry", instance);
			writer.WritePrivateField("m_DefaultArea", instance);
			writer.WritePrivateField("m_IgnoreNavMeshAgent", instance);
			writer.WritePrivateField("m_IgnoreNavMeshObstacle", instance);
			writer.WritePrivateField("m_OverrideTileSize", instance);
			writer.WritePrivateField("m_TileSize", instance);
			writer.WritePrivateField("m_OverrideVoxelSize", instance);
			writer.WritePrivateField("m_VoxelSize", instance);
			writer.WritePrivateField("m_BuildHeightMesh", instance);
			writer.WritePrivateFieldByRef("m_NavMeshData", instance);
			writer.WritePrivateField("m_NavMeshDataInstance", instance);
			writer.WritePrivateField("m_LastPosition", instance);
			writer.WritePrivateField("m_LastRotation", instance);
			writer.WriteProperty("agentTypeID", instance.agentTypeID, ES3Type_int.Instance);
			writer.WriteProperty("collectObjects", instance.collectObjects, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.AI.CollectObjects)));
			writer.WriteProperty("size", instance.size, ES3Type_Vector3.Instance);
			writer.WriteProperty("center", instance.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("layerMask", instance.layerMask, ES3Type_LayerMask.Instance);
			writer.WriteProperty("useGeometry", instance.useGeometry, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.AI.NavMeshCollectGeometry)));
			writer.WriteProperty("defaultArea", instance.defaultArea, ES3Type_int.Instance);
			writer.WriteProperty("ignoreNavMeshAgent", instance.ignoreNavMeshAgent, ES3Type_bool.Instance);
			writer.WriteProperty("ignoreNavMeshObstacle", instance.ignoreNavMeshObstacle, ES3Type_bool.Instance);
			writer.WriteProperty("overrideTileSize", instance.overrideTileSize, ES3Type_bool.Instance);
			writer.WriteProperty("tileSize", instance.tileSize, ES3Type_int.Instance);
			writer.WriteProperty("overrideVoxelSize", instance.overrideVoxelSize, ES3Type_bool.Instance);
			writer.WriteProperty("voxelSize", instance.voxelSize, ES3Type_float.Instance);
			writer.WriteProperty("buildHeightMesh", instance.buildHeightMesh, ES3Type_bool.Instance);
			writer.WritePropertyByRef("navMeshData", instance.navMeshData);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (UnityEngine.AI.NavMeshSurface)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "m_AgentTypeID":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_AgentTypeID", reader.Read<System.Int32>(), instance);
					break;
					case "m_CollectObjects":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_CollectObjects", reader.Read<UnityEngine.AI.CollectObjects>(), instance);
					break;
					case "m_Size":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_Size", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "m_Center":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_Center", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "m_LayerMask":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_LayerMask", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "m_UseGeometry":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_UseGeometry", reader.Read<UnityEngine.AI.NavMeshCollectGeometry>(), instance);
					break;
					case "m_DefaultArea":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_DefaultArea", reader.Read<System.Int32>(), instance);
					break;
					case "m_IgnoreNavMeshAgent":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_IgnoreNavMeshAgent", reader.Read<System.Boolean>(), instance);
					break;
					case "m_IgnoreNavMeshObstacle":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_IgnoreNavMeshObstacle", reader.Read<System.Boolean>(), instance);
					break;
					case "m_OverrideTileSize":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_OverrideTileSize", reader.Read<System.Boolean>(), instance);
					break;
					case "m_TileSize":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_TileSize", reader.Read<System.Int32>(), instance);
					break;
					case "m_OverrideVoxelSize":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_OverrideVoxelSize", reader.Read<System.Boolean>(), instance);
					break;
					case "m_VoxelSize":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_VoxelSize", reader.Read<System.Single>(), instance);
					break;
					case "m_BuildHeightMesh":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_BuildHeightMesh", reader.Read<System.Boolean>(), instance);
					break;
					case "m_NavMeshData":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_NavMeshData", reader.Read<UnityEngine.AI.NavMeshData>(), instance);
					break;
					case "m_NavMeshDataInstance":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_NavMeshDataInstance", reader.Read<UnityEngine.AI.NavMeshDataInstance>(), instance);
					break;
					case "m_LastPosition":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_LastPosition", reader.Read<UnityEngine.Vector3>(), instance);
					break;
					case "m_LastRotation":
					instance = (UnityEngine.AI.NavMeshSurface)reader.SetPrivateField("m_LastRotation", reader.Read<UnityEngine.Quaternion>(), instance);
					break;
					case "agentTypeID":
						instance.agentTypeID = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "collectObjects":
						instance.collectObjects = reader.Read<UnityEngine.AI.CollectObjects>();
						break;
					case "size":
						instance.size = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "center":
						instance.center = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "layerMask":
						instance.layerMask = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "useGeometry":
						instance.useGeometry = reader.Read<UnityEngine.AI.NavMeshCollectGeometry>();
						break;
					case "defaultArea":
						instance.defaultArea = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "ignoreNavMeshAgent":
						instance.ignoreNavMeshAgent = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "ignoreNavMeshObstacle":
						instance.ignoreNavMeshObstacle = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "overrideTileSize":
						instance.overrideTileSize = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "tileSize":
						instance.tileSize = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "overrideVoxelSize":
						instance.overrideVoxelSize = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "voxelSize":
						instance.voxelSize = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "buildHeightMesh":
						instance.buildHeightMesh = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "navMeshData":
						instance.navMeshData = reader.Read<UnityEngine.AI.NavMeshData>();
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


	public class ES3UserType_NavMeshSurfaceArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_NavMeshSurfaceArray() : base(typeof(UnityEngine.AI.NavMeshSurface[]), ES3UserType_NavMeshSurface.Instance)
		{
			Instance = this;
		}
	}
}