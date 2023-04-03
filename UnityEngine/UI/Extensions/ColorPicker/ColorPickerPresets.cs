using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.ColorPicker
{
	// Token: 0x02000355 RID: 853
	public class ColorPickerPresets : MonoBehaviour
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0004C354 File Offset: 0x0004A554
		public virtual string JsonFilePath
		{
			get
			{
				return Application.persistentDataPath + "/" + this.playerPrefsKey + ".json";
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004C370 File Offset: 0x0004A570
		protected virtual void Reset()
		{
			this.playerPrefsKey = "colorpicker_" + base.GetInstanceID().ToString();
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004C39C File Offset: 0x0004A59C
		protected virtual void Awake()
		{
			this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
			this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
			this.picker.CurrentColor = Color.white;
			this.presetPrefab.SetActive(false);
			this.presets.AddRange(this.predefinedPresets);
			this.LoadPresets(this.saveMode);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0004C41C File Offset: 0x0004A61C
		public virtual void CreatePresetButton()
		{
			this.CreatePreset(this.picker.CurrentColor);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004C430 File Offset: 0x0004A630
		public virtual void LoadPresets(ColorPickerPresets.SaveType saveType)
		{
			string text = "";
			switch (saveType)
			{
			case ColorPickerPresets.SaveType.None:
				break;
			case ColorPickerPresets.SaveType.PlayerPrefs:
				if (PlayerPrefs.HasKey(this.playerPrefsKey))
				{
					text = PlayerPrefs.GetString(this.playerPrefsKey);
				}
				break;
			case ColorPickerPresets.SaveType.JsonFile:
				if (File.Exists(this.JsonFilePath))
				{
					text = File.ReadAllText(this.JsonFilePath);
				}
				break;
			default:
				throw new NotImplementedException(saveType.ToString());
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					ColorPickerPresets.JsonColor jsonColor = JsonUtility.FromJson<ColorPickerPresets.JsonColor>(text);
					this.presets.AddRange(jsonColor.GetColors());
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			foreach (Color color in this.presets)
			{
				this.CreatePreset(color, true);
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004C51C File Offset: 0x0004A71C
		public virtual void SavePresets(ColorPickerPresets.SaveType saveType)
		{
			if (this.presets == null || this.presets.Count <= 0)
			{
				Debug.LogError("presets cannot be null or empty: " + ((this.presets == null) ? "NULL" : "EMPTY"));
				return;
			}
			ColorPickerPresets.JsonColor jsonColor = new ColorPickerPresets.JsonColor();
			jsonColor.SetColors(this.presets.ToArray());
			string text = JsonUtility.ToJson(jsonColor);
			switch (saveType)
			{
			case ColorPickerPresets.SaveType.None:
				Debug.LogWarning("Called SavePresets with SaveType = None...");
				return;
			case ColorPickerPresets.SaveType.PlayerPrefs:
				PlayerPrefs.SetString(this.playerPrefsKey, text);
				return;
			case ColorPickerPresets.SaveType.JsonFile:
				File.WriteAllText(this.JsonFilePath, text);
				return;
			default:
				throw new NotImplementedException(saveType.ToString());
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004C5CC File Offset: 0x0004A7CC
		public virtual void CreatePreset(Color color, bool loading)
		{
			this.createButton.gameObject.SetActive(this.presets.Count < this.maxPresets);
			GameObject gameObject = Object.Instantiate<GameObject>(this.presetPrefab, this.presetPrefab.transform.parent);
			gameObject.transform.SetAsLastSibling();
			gameObject.SetActive(true);
			gameObject.GetComponent<Image>().color = color;
			this.createPresetImage.color = Color.white;
			if (!loading)
			{
				this.presets.Add(color);
				this.SavePresets(this.saveMode);
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004C65F File Offset: 0x0004A85F
		public virtual void CreatePreset(Color color)
		{
			this.CreatePreset(color, false);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004C669 File Offset: 0x0004A869
		public virtual void PresetSelect(Image sender)
		{
			this.picker.CurrentColor = sender.color;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004C67C File Offset: 0x0004A87C
		protected virtual void HSVChanged(float h, float s, float v)
		{
			this.createPresetImage.color = HSVUtil.ConvertHsvToRgb((double)(h * 360f), (double)s, (double)v, 1f);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004C69F File Offset: 0x0004A89F
		protected virtual void ColorChanged(Color color)
		{
			this.createPresetImage.color = color;
		}

		// Token: 0x04000E22 RID: 3618
		public ColorPickerControl picker;

		// Token: 0x04000E23 RID: 3619
		[SerializeField]
		protected GameObject presetPrefab;

		// Token: 0x04000E24 RID: 3620
		[SerializeField]
		protected int maxPresets = 16;

		// Token: 0x04000E25 RID: 3621
		[SerializeField]
		protected Color[] predefinedPresets;

		// Token: 0x04000E26 RID: 3622
		protected List<Color> presets = new List<Color>();

		// Token: 0x04000E27 RID: 3623
		public Image createPresetImage;

		// Token: 0x04000E28 RID: 3624
		public Transform createButton;

		// Token: 0x04000E29 RID: 3625
		[SerializeField]
		public ColorPickerPresets.SaveType saveMode;

		// Token: 0x04000E2A RID: 3626
		[SerializeField]
		protected string playerPrefsKey;

		// Token: 0x02001179 RID: 4473
		public enum SaveType
		{
			// Token: 0x0400927D RID: 37501
			None,
			// Token: 0x0400927E RID: 37502
			PlayerPrefs,
			// Token: 0x0400927F RID: 37503
			JsonFile
		}

		// Token: 0x0200117A RID: 4474
		protected class JsonColor
		{
			// Token: 0x06009FD2 RID: 40914 RVA: 0x0033D3C8 File Offset: 0x0033B5C8
			public void SetColors(Color[] colorsIn)
			{
				this.colors = new Color32[colorsIn.Length];
				for (int i = 0; i < colorsIn.Length; i++)
				{
					this.colors[i] = colorsIn[i];
				}
			}

			// Token: 0x06009FD3 RID: 40915 RVA: 0x0033D40C File Offset: 0x0033B60C
			public Color[] GetColors()
			{
				Color[] array = new Color[this.colors.Length];
				for (int i = 0; i < this.colors.Length; i++)
				{
					array[i] = this.colors[i];
				}
				return array;
			}

			// Token: 0x04009280 RID: 37504
			public Color32[] colors;
		}
	}
}
