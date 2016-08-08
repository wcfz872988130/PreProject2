using UnityEngine;
using System.Collections;

public class RunScript : MonoBehaviour {
	private AssetBundleManifest manifest;
	public static readonly string PathURL=
#if UNITY_ANDROID
		"jar:file://"+Application.dataPath+"!/assets/";
#elif UNITY_IPHONE
		Application.dataPath+"/StreamingAssets";
#elif UNITY_STANDALONE_WIN||UNITY_EDITOR
		"file://"+Application.dataPath+"/StreamingAsset/";
#else
	string.Empty;
#endif

	void Start()
	{
		StartCoroutine (LoadManifest (PathURL));
	}

	void OnGUI()
	{
		if(GUILayout.Button("Texture Assetbundle"))
		{
			StartCoroutine (LoadSigGameObject(PathURL+"m2.assetbundle"));
		}
		if(GUILayout.Button("Main Assetbundle"))
		{
			StartCoroutine (LoadMainGameObject(PathURL+"cube.ab"));
			//StartCoroutine (LoadMainGameObject(PathURL+"Plane.assetbundle"));
		}
		if(GUILayout.Button("ALL Assetbundle"))
		{
			StartCoroutine (LoadALLGameObject(PathURL+"All.assetbundle"));
		}
		if(GUILayout.Button("ALLCache"))
		{
			StartCoroutine (LoadMainCacheGameObject(PathURL+"All.assetbundle"));
		}
		if(GUILayout.Button("GetPlane"))
		{
			StartCoroutine (LoadNewGameObject("plane.ab"));
		}
		if(GUILayout.Button("GetCube"))
		{
			StartCoroutine (LoadCubeObject("cube.ab"));
		}
	}

	private IEnumerator LoadManifest(string Path)
	{
		string mUrl = Path + "StreamingAsset";
		//WWW bundle = new WWW (mUrl);
		//yield return bundle;
		WWW mwww=WWW.LoadFromCacheOrDownload(mUrl,0);
		yield return mwww;
		if (!string.IsNullOrEmpty (mwww.error)) 
		{
			Debug.Log (mwww.error + "1111");
		} 
		else 
		{
			manifest = (AssetBundleManifest)mwww.assetBundle.LoadAsset ("AssetBundleManifest");
			if (manifest == null) {
				Debug.Log ("Source is null.");
			} else {
				Debug.Log ("Success!...............");
				StopCoroutine ("LoadManifest");
			}
		}
		mwww.assetBundle.Unload (false);
	}

	private IEnumerator LoadMainGameObject(string path)
	{
		WWW bundle = new WWW (path);
		yield return bundle;

		yield return Instantiate (bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload (false);
	}

	private IEnumerator LoadSigGameObject(string path)
	{
		WWW bundle = new WWW (path);
		yield return bundle;
		//bundle.assetBundle.Unload (false);
	}

	private IEnumerator LoadALLGameObject(string path)
	{
		WWW bundle = new WWW (path);
		yield return bundle;

		Object obj0 = bundle.assetBundle.LoadAsset ("Cube");
		Object obj1 = bundle.assetBundle.LoadAsset ("Plane");

		yield return Instantiate (obj0);
		yield return Instantiate (obj1);
		bundle.assetBundle.Unload (false);
	}

	private IEnumerator LoadMainCacheGameObject(string path)
	{
		WWW bundle = WWW.LoadFromCacheOrDownload (path,2);
		yield return bundle;
		Object obj0 = bundle.assetBundle.LoadAsset ("Cube");
		Object obj1 = bundle.assetBundle.LoadAsset ("Plane");

		yield return Instantiate (obj0);
		yield return Instantiate (obj1);
		bundle.assetBundle.Unload (false);
	}
	private IEnumerator LoadNewGameObject(string name)
	{
			string[] dps = manifest.GetAllDependencies (name);
			AssetBundle[] abs=new AssetBundle[dps.Length];
			Debug.Log (dps.Length);
			for(int i=0;i<dps.Length;i++)
			{
				string dUrl = PathURL + dps [i];
				Debug.Log (dUrl);
				WWW dwww = WWW.LoadFromCacheOrDownload (dUrl,manifest.GetAssetBundleHash(dps[i]),0);
				yield return dwww;
				abs [i] = dwww.assetBundle;
			}
			WWW www = WWW.LoadFromCacheOrDownload (PathURL+name,manifest.GetAssetBundleHash(name),0);
			yield return www;
			if (!string.IsNullOrEmpty (www.error)) 
			{
				Debug.Log (www.error);
			} 
			else 
			{
				AssetBundle ab = www.assetBundle;
				GameObject gobj = ab.LoadAsset ("plane")as GameObject;
				Debug.Log (gobj.name);
				if(gobj!=null)
				{
					Instantiate (gobj);
					ab.Unload (false);
				}
			}
			foreach (AssetBundle ab in abs)
			{
				ab.Unload (false);
			}
	}

	private IEnumerator LoadCubeObject(string name)
	{
		string[] dps = manifest.GetAllDependencies (name);
		AssetBundle[] abs=new AssetBundle[dps.Length];
		Debug.Log (dps.Length);
		for(int i=0;i<dps.Length;i++)
		{
			string dUrl = PathURL + dps[i];
			Debug.Log (dUrl);
			WWW dwww = WWW.LoadFromCacheOrDownload (dUrl,manifest.GetAssetBundleHash(dps[i]),0);
			yield return dwww;
			abs [i] = dwww.assetBundle;
		}
		WWW www = WWW.LoadFromCacheOrDownload (PathURL+name,manifest.GetAssetBundleHash(name),0);
		Debug.Log (PathURL+name);
		yield return www;
		if (!string.IsNullOrEmpty (www.error)) {
			Debug.Log (www.error);
		} else {
			GameObject gobj = www.assetBundle.LoadAsset (name.Remove(name.Length-3,3))as GameObject;
			Debug.Log (name);
			if(gobj!=null)
			{
				Instantiate (gobj);
				www.assetBundle.Unload (false);
			}
		}
		foreach(AssetBundle ab in abs)
		{
			ab.Unload (false);
		}
	}
}
