using UnityEngine;
using System.Collections;
using UnityEditor;

public class StartBundle : ScriptableObject {

	[MenuItem("StartBundle/UNITY5Bundle")]
	static void NewBundle()
	{
		//string targetPath = Application.dataPath + "/StreamingAssets" ;
		//if (BuildPipeline.BuildAssetBundles ( targetPath)) {
			//Debug.Log ("资源打包成功");
		//} 
		//BuildPipeline.BuildAssetBundles(Application.dataPath+"/AssetBundles");
		BuildPipeline.BuildAssetBundles(Application.dataPath+"/StreamingAsset");
	}

	[MenuItem("StartBundle/PackageName")]
	static void PackageName()
	{
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object),SelectionMode.DeepAssets);
		foreach(Object obj in SelectedAsset)
		{
			string path = AssetDatabase.GetAssetPath (obj);
			AssetImporter importer = AssetImporter.GetAtPath (path);
			importer.assetBundleName = obj.name + ".ab";
		}
	}


	[MenuItem("StartBundle/Create AssetBundles Main")]
	static void createAssetBundleMain()
	{
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object),SelectionMode.DeepAssets);
		foreach(Object obj in SelectedAsset)
		{
			Debug.Log (obj.name);
			string sourcePath = AssetDatabase.GetAssetPath (obj);
			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies)) {
				Debug.Log (obj.name + "资源打包成功");
			} 
			else 
			{
				Debug.Log (obj.name+"资源打包失败");
			}
		}
		AssetDatabase.Refresh ();
	}

	[MenuItem("StartBundle/Create AssetBundles All")]
	static void createAssetBundleAll()
	{
		Caching.CleanCache ();
		string Path = Application.dataPath + "StreamingAssets/All.assetbundle";
		Object[] selectedAsset = Selection.GetFiltered (typeof(Object),SelectionMode.DeepAssets);
		string targetPath = Application.dataPath + "/StreamingAssets/All.assetbundle";
		if (BuildPipeline.BuildAssetBundle (null,selectedAsset,targetPath,BuildAssetBundleOptions.CollectDependencies)) 
		{
			Debug.Log ("资源打包成功");
		}
		else {
			Debug.Log ("资源打包失败");
		}
		AssetDatabase.Refresh ();
	}

	[MenuItem("StartBundle/PushAssetDepend")]
	static void PushAssetDepend()
	{
		BuildPipeline.PushAssetDependencies ();
	}

	[MenuItem("StartBundle/PopAssetDepend")]
	static void PopAssetDepend()
	{
		BuildPipeline.PopAssetDependencies ();
	}
}
