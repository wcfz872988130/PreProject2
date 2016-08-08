using UnityEngine;
using System.Collections;

public class NetBundleManager:MonoBehaviour{
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
	public static NetBundleManager Instance=null;
	private bool index = false;
	//private GameObject myObject;

	private NetBundleManager(){
		Instance = this;
	}

	public static NetBundleManager GetNetManager()
	{
		if(Instance==null)
		{
			Instance = new NetBundleManager();
		}
		return Instance;
	}
		
	public void GetMyModel(string name)
	{
		Debug.Log (name);
		StartCoroutine (GetModel(name));
	}

	void Awake()
	{
		StartCoroutine (LoadManifest());
	}

	private IEnumerator LoadManifest()
	{
		string mUrl = PathURL + "StreamingAsset";
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

	private  IEnumerator GetModel(string name)
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
			GameObject myObject = www.assetBundle.LoadAsset (name.Remove(name.Length-3,3)) as GameObject;
			if(myObject!=null)
			{
				Instantiate (myObject);
				Debug.Log (myObject.name);
				www.assetBundle.Unload (false);
				//StopCoroutine ("GetModel");
			}
		}
		foreach(AssetBundle ab in abs)
		{
			ab.Unload (false);
		}
	}
}
