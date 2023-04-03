using System;
using System.IO;
using DG.Tweening;
using Spine.Unity;
using TMPro;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class BattleConditionFxViewer : MonoBehaviour
{
	// Token: 0x0600012E RID: 302 RVA: 0x00005C6C File Offset: 0x00003E6C
	private void Start()
	{
		this.isFade = false;
		this.isRaid = false;
		if (this.UnitRoot == null)
		{
			this.UnitRoot = GameObject.Find("POS_UNIT").transform;
		}
		if (this.UnitRoot != null)
		{
			this.hixBoxes = this.UnitRoot.GetComponentsInChildren<DrawHitBox>(true);
		}
		if (this.MainCam == null)
		{
			this.MainCam = Camera.main;
			this.MainCam.backgroundColor = Color.black;
		}
		if (this.UIRoot == null)
		{
			this.UIRoot = GameObject.Find("Canvas");
		}
		if (this.UIRoot != null)
		{
			if (this.HitBoxText != null)
			{
				this.HitBoxTmp = UnityEngine.Object.Instantiate<GameObject>(this.HitBoxText, this.UIRoot.transform).GetComponent<TextMeshProUGUI>();
				this.HitBoxTmp.rectTransform.localPosition = Vector3.zero;
				this.HitBoxTmp.rectTransform.anchoredPosition = new Vector2(0f, 0f);
				this.SetHitBox();
			}
			if (this.MapText != null)
			{
				this.MapTmp = UnityEngine.Object.Instantiate<GameObject>(this.MapText, this.UIRoot.transform).GetComponent<TextMeshProUGUI>();
				this.MapTmp.rectTransform.localPosition = Vector3.zero;
				this.MapTmp.rectTransform.anchoredPosition = new Vector2(-25.18f, 4.3f);
			}
		}
		this.ClearCurrentMap();
		this.MakeCurrentMap();
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00005DFC File Offset: 0x00003FFC
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Renderer[] componentsInChildren = this.currentMap.GetComponentsInChildren<Renderer>();
			if (this.tween != null && DOTween.IsTweening(this.tween, false))
			{
				return;
			}
			if (!this.isFade)
			{
				this.isFade = true;
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					SpriteRenderer component = componentsInChildren[i].GetComponent<SpriteRenderer>();
					if (component != null)
					{
						this.tween = component.material.DOColor(Color.black, this.FadeOutTime).target;
					}
					else if (componentsInChildren[i].GetComponent<SkeletonAnimation>() != null)
					{
						this.tween = componentsInChildren[i].material.DOColor(Color.black, this.FadeOutTime).target;
					}
				}
			}
			else
			{
				this.isFade = false;
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					SpriteRenderer component = componentsInChildren[j].GetComponent<SpriteRenderer>();
					if (component != null)
					{
						this.tween = component.material.DOColor(Color.white, this.FadeInTime).target;
					}
					else if (componentsInChildren[j].GetComponent<SkeletonAnimation>() != null)
					{
						this.tween = componentsInChildren[j].material.DOColor(Color.white, this.FadeInTime).target;
					}
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			this.ShowHitBox = !this.ShowHitBox;
			this.SetHitBox();
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (this.isRaid)
			{
				this.isRaid = false;
				this.ClearCurrentMap();
				this.MakeCurrentMap();
				this.MainCam.orthographicSize = (float)this.NormalViewSize;
				this.UnitRoot.localPosition = this.NormalViewUnitPos;
			}
			else
			{
				this.isRaid = true;
				this.ClearCurrentMap();
				this.currentMap = UnityEngine.Object.Instantiate<GameObject>(this.RaidMap, this.MapRoot.transform);
				this.MapTmp.text = "(Raid) : " + this.RaidMap.name;
				this.MapTmp.GetComponent<DOTweenAnimation>().DORestart();
				this.MainCam.orthographicSize = (float)this.RaidViewSize;
				this.UnitRoot.localPosition = this.RaidViewUnitPos;
			}
		}
		if (this.isRaid)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.Index--;
			if (0 > this.Index)
			{
				this.Index = Mathf.Clamp(this.BML.Maps.Length, 0, this.BML.Maps.Length - 1);
			}
			this.ClearCurrentMap();
			this.MakeCurrentMap();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.Index++;
			if (Mathf.Clamp(this.BML.Maps.Length, 0, this.BML.Maps.Length - 1) < this.Index)
			{
				this.Index = 0;
			}
			this.ClearCurrentMap();
			this.MakeCurrentMap();
		}
		if (Input.GetKeyUp(KeyCode.F12))
		{
			BattleConditionFxViewer.CaptureScreen();
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000060EC File Offset: 0x000042EC
	private void SetHitBox()
	{
		if (this.ShowHitBox)
		{
			this.HitBoxTmp.text = "HIT BOX ON";
		}
		else
		{
			this.HitBoxTmp.text = "HIT BOX OFF";
		}
		this.HitBoxTmp.GetComponent<DOTweenAnimation>().DORestart();
		if (this.hixBoxes != null && this.hixBoxes.Length != 0)
		{
			for (int i = 0; i < this.hixBoxes.Length; i++)
			{
				if (this.hixBoxes[i] != null)
				{
					this.hixBoxes[i].ShowHitBox(this.ShowHitBox);
				}
			}
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000617C File Offset: 0x0000437C
	private void ClearCurrentMap()
	{
		int childCount = this.MapRoot.transform.childCount;
		GameObject[] array = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = this.MapRoot.transform.GetChild(i).gameObject;
		}
		if (array.Length != 0)
		{
			GameObject[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				UnityEngine.Object.DestroyImmediate(array2[j]);
			}
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x000061E8 File Offset: 0x000043E8
	private void MakeCurrentMap()
	{
		if (this.MapTmp != null)
		{
			for (int i = 0; i < this.BML.Maps.Length; i++)
			{
				if (this.Index == i)
				{
					this.currentMap = UnityEngine.Object.Instantiate<GameObject>(this.BML.Maps[i], this.MapRoot.transform);
					this.MapTmp.text = "(" + i.ToString() + ") : " + this.BML.Maps[i].name;
					break;
				}
			}
			this.MapTmp.GetComponent<DOTweenAnimation>().DORestart();
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00006290 File Offset: 0x00004490
	public static bool CaptureScreen()
	{
		if (!Directory.Exists("ScreenShot/"))
		{
			Directory.CreateDirectory("ScreenShot/");
		}
		string text = BattleConditionFxViewer.MakeCaptureFileName();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		ScreenCapture.CaptureScreenshot(text);
		Debug.Log("Screencapture : " + text);
		return true;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000062DC File Offset: 0x000044DC
	private static string MakeCaptureFileName()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo("ScreenShot/");
		if (directoryInfo == null || !directoryInfo.Exists)
		{
			return null;
		}
		string str = directoryInfo.FullName + "CounterSide-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
		string text = str + ".png";
		if (!File.Exists(text))
		{
			return text;
		}
		int num = 60;
		for (int i = 1; i < num; i++)
		{
			text = str + string.Format(" ({0})", i) + ".png";
			if (!File.Exists(text))
			{
				return text;
			}
		}
		return null;
	}

	// Token: 0x040000BB RID: 187
	public int Index;

	// Token: 0x040000BC RID: 188
	public BattleMapList BML;

	// Token: 0x040000BD RID: 189
	public GameObject MapText;

	// Token: 0x040000BE RID: 190
	public GameObject HitBoxText;

	// Token: 0x040000BF RID: 191
	public bool ShowHitBox;

	// Token: 0x040000C0 RID: 192
	[Space]
	public Transform MapRoot;

	// Token: 0x040000C1 RID: 193
	public GameObject UIRoot;

	// Token: 0x040000C2 RID: 194
	public Transform UnitRoot;

	// Token: 0x040000C3 RID: 195
	[Space]
	public Camera MainCam;

	// Token: 0x040000C4 RID: 196
	public GameObject RaidMap;

	// Token: 0x040000C5 RID: 197
	[Space]
	public float FadeInTime = 2f;

	// Token: 0x040000C6 RID: 198
	public float FadeOutTime = 2f;

	// Token: 0x040000C7 RID: 199
	[Space]
	public int NormalViewSize = 500;

	// Token: 0x040000C8 RID: 200
	public int RaidViewSize = 750;

	// Token: 0x040000C9 RID: 201
	[Space]
	public Vector3 NormalViewUnitPos;

	// Token: 0x040000CA RID: 202
	public Vector3 RaidViewUnitPos;

	// Token: 0x040000CB RID: 203
	private GameObject currentMap;

	// Token: 0x040000CC RID: 204
	private TextMeshProUGUI HitBoxTmp;

	// Token: 0x040000CD RID: 205
	private TextMeshProUGUI MapTmp;

	// Token: 0x040000CE RID: 206
	private DrawHitBox[] hixBoxes;

	// Token: 0x040000CF RID: 207
	private bool isRaid;

	// Token: 0x040000D0 RID: 208
	private bool isFade;

	// Token: 0x040000D1 RID: 209
	private object tween;

	// Token: 0x040000D2 RID: 210
	private const string SCREENSHOT_DIR = "ScreenShot/";

	// Token: 0x040000D3 RID: 211
	private const string FILEPATH_PREFIX = "CounterSide-";

	// Token: 0x040000D4 RID: 212
	private const string FILENAME_DATE_FORMAT = "yyyy-MM-dd HH-mm-ss";

	// Token: 0x040000D5 RID: 213
	private const string FILE_EXTENSION = ".png";
}
