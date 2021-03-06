using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using client_zproto;

class Tool {
	public static void ToNative(ref Vector3 dst, float x, float z) {
		dst.x = x;
		dst.z = z;
		dst.y = 0.0f;
	}

	public static DB.IdCount ToNative(idcount i) {
		DB.IdCount a;
		a.id = i.id;
		a.count = i.count;
		return a;
	}

	public static void ToNative(ref Dictionary<int, DB.IdCount> o, idcount[] src) {
		for (int i = 0; i < src.Length; i++)
			o[src[i].id] = Tool.ToNative(src[i]);
	}

	public static byte[] tobytes(string dat) {
		return UTF8Encoding.UTF8.GetBytes(dat);
	}

	public static string tostring(byte[] dat) {
		return UTF8Encoding.UTF8.GetString(dat);
	}

	public static byte[] sha1(string passwd) {
		ASCIIEncoding enc = new ASCIIEncoding();
		byte[] hash = enc.GetBytes(passwd);
		SHA1 sha = new SHA1CryptoServiceProvider();
		return sha.ComputeHash(hash);
	}

	public static byte[] hmac(byte[] passwd, string text) {
		ASCIIEncoding enc = new ASCIIEncoding();
		byte[] hash = enc.GetBytes(text);
		HMACSHA1 hmac = new HMACSHA1(passwd);
		return hmac.ComputeHash(hash);
	}

	public static GameObject FindChild(Transform parent, string childName) {
		var f = parent.Find(childName);
		if (f != null)
			return f.gameObject;
		foreach (Transform child in parent) {
			var obj = FindChild(child, childName);
			if (obj != null)
				return obj;
		}
		return null;
	}
	public static Quaternion ClampRotationAroundXAxis(Quaternion q) {
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;
		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
		angleX = Mathf.Clamp(angleX, -15.0f, 15.0f);
		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);
		return q;
        }

	public static void Filter(ref Vector3 a, Vector3 b, float delta) {
		if (Mathf.Abs(a.x - b.x) > delta)
			a.x = b.x;
		if (Mathf.Abs(a.y - b.y) > delta)
			a.y = b.y;
		if (Mathf.Abs(a.z - b.z) > delta)
			a.z = b.z;
		return ;
	}

	private static string GetiPhoneDocumentsPath() {
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}

	public static string GetPath(string fileName){
	#if UNITY_EDITOR
		return Application.dataPath +"/Resources/"+fileName;
	#elif UNITY_ANDROID
		return Application.persistentDataPath+fileName;
	#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath()+"/"+fileName;
	#else
		return Application.dataPath +"/"+ fileName;
	#endif
	}

	public static GameObject InstancePrefab(string name, Vector3 pos, Quaternion rot) {
		return Module.Misc.tool.InstancePrefab(name, pos, rot);
	}

	public static GameObject InstancePrefab(string name) {
		return InstancePrefab(name, Vector3.zero, Quaternion.identity);
	}

	public static void PlayParticle(string name, Vector3 pos, float sec) {
		Module.Misc.tool.PlayParticle(name, pos, sec);
	}
}


