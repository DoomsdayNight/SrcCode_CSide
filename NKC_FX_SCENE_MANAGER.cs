using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200002B RID: 43
public class NKC_FX_SCENE_MANAGER : MonoBehaviour
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600014A RID: 330 RVA: 0x00006802 File Offset: 0x00004A02
	// (set) Token: 0x0600014B RID: 331 RVA: 0x0000680A File Offset: 0x00004A0A
	public float GlobalTransparancy
	{
		get
		{
			return this.globalTransparancy;
		}
		set
		{
			this.globalTransparancy = value;
			this.SetGlobalTransparency(this.globalTransparancy);
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00006820 File Offset: 0x00004A20
	private void Start()
	{
		GameObject gameObject = GameObject.Find("Slider_VfxOpacity");
		if (gameObject != null)
		{
			this.vfxOpacitySlider = gameObject.GetComponent<Slider>();
			this.vfxOpacitySlider.onValueChanged.AddListener(delegate(float <p0>)
			{
				this.SetGlobalTransparency(this.vfxOpacitySlider.value);
			});
		}
		if (this.UIRoot == null)
		{
			this.UIRoot = GameObject.Find("Canvas");
		}
		if (this.UIRoot != null && this.SceneDropDown != null)
		{
			this.sceneDropDown = UnityEngine.Object.Instantiate<GameObject>(this.SceneDropDown, this.UIRoot.transform);
			this.sceneDropDown.transform.localPosition = new Vector3(-50f, 300f, 0f);
			this.dropdown = this.sceneDropDown.GetComponent<Dropdown>();
			this.InitializeDropdown();
			this.dropdown.onValueChanged.AddListener(delegate(int <p0>)
			{
				this.OnDropDownChanged(this.dropdown);
			});
			this.sceneDropDown.SetActive(false);
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00006928 File Offset: 0x00004B28
	private void InitializeDropdown()
	{
		if (SceneManager.sceneCountInBuildSettings > 0)
		{
			this.dropdown.ClearOptions();
			for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
			{
				this.path = SceneUtility.GetScenePathByBuildIndex(i);
				this.sceneName = Path.GetFileNameWithoutExtension(this.path);
				this.optionDatas.Add(new Dropdown.OptionData(this.sceneName));
				this.sceneList = this.sceneList + SceneUtility.GetScenePathByBuildIndex(i) + "\n";
			}
			this.dropdown.AddOptions(this.optionDatas);
			Scene activeScene = SceneManager.GetActiveScene();
			this.dropdown.value = activeScene.buildIndex;
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000069D4 File Offset: 0x00004BD4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			this.sceneDropDown.SetActive(!this.sceneDropDown.activeSelf);
		}
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			this.isHideHud = !this.isHideHud;
			this.HideHud(this.isHideHud);
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00006A26 File Offset: 0x00004C26
	private void OnDropDownChanged(Dropdown _dropDown)
	{
		this.LoadScene(_dropDown.value);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00006A34 File Offset: 0x00004C34
	private void LoadScene(int _index)
	{
		this.path = SceneUtility.GetScenePathByBuildIndex(_index);
		SceneManager.LoadScene(this.path, LoadSceneMode.Single);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00006A50 File Offset: 0x00004C50
	private void HideHud(bool _toggle)
	{
		int childCount = this.UIRoot.transform.childCount;
		int instanceID = this.sceneDropDown.gameObject.GetInstanceID();
		for (int i = 0; i < childCount; i++)
		{
			if (this.UIRoot.transform.GetChild(i).gameObject.GetInstanceID() != instanceID)
			{
				this.UIRoot.transform.GetChild(i).gameObject.SetActive(_toggle);
			}
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00006AC5 File Offset: 0x00004CC5
	public void SetGlobalTransparency(float _factor)
	{
		Shader.SetGlobalFloat("_FxGlobalTransparency", _factor);
	}

	// Token: 0x040000EA RID: 234
	[SerializeField]
	[Range(0f, 1f)]
	private float globalTransparancy = 1f;

	// Token: 0x040000EB RID: 235
	public GameObject UIRoot;

	// Token: 0x040000EC RID: 236
	public GameObject SceneDropDown;

	// Token: 0x040000ED RID: 237
	private GameObject sceneDropDown;

	// Token: 0x040000EE RID: 238
	private Dropdown dropdown;

	// Token: 0x040000EF RID: 239
	private Slider vfxOpacitySlider;

	// Token: 0x040000F0 RID: 240
	private string path = string.Empty;

	// Token: 0x040000F1 RID: 241
	private string sceneName = string.Empty;

	// Token: 0x040000F2 RID: 242
	private string sceneList = string.Empty;

	// Token: 0x040000F3 RID: 243
	private List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

	// Token: 0x040000F4 RID: 244
	private bool isHideHud;
}
