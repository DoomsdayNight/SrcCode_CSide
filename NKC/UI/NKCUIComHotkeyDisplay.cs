using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000937 RID: 2359
	public class NKCUIComHotkeyDisplay : MonoBehaviour
	{
		// Token: 0x06005E37 RID: 24119 RVA: 0x001D2B70 File Offset: 0x001D0D70
		public static void OpenInstance(Transform parent, InGamehotkeyEventType type)
		{
			if (parent == null)
			{
				return;
			}
			if (!parent.gameObject.activeInHierarchy)
			{
				return;
			}
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HOTKEY", "NKM_UI_HOTKEY", false, null);
			NKCUIComHotkeyDisplay component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIComHotkeyDisplay>();
			component.SetPosition(parent);
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			component.SetText(NKCInputManager.GetInputManager().GetHotkeyString(type));
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x001D2BD0 File Offset: 0x001D0DD0
		public static void OpenInstance(Transform parent, HotkeyEventType type)
		{
			if (parent == null)
			{
				return;
			}
			if (!parent.gameObject.activeInHierarchy)
			{
				return;
			}
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HOTKEY", "NKM_UI_HOTKEY", false, null);
			NKCUIComHotkeyDisplay component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIComHotkeyDisplay>();
			component.SetPosition(parent);
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			component.SetText(NKCInputManager.GetInputManager().GetHotkeyString(type));
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x001D2C30 File Offset: 0x001D0E30
		public static void OpenInstance(Transform parent, params HotkeyEventType[] types)
		{
			if (parent == null)
			{
				return;
			}
			if (!parent.gameObject.activeInHierarchy)
			{
				return;
			}
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HOTKEY", "NKM_UI_HOTKEY", false, null);
			NKCUIComHotkeyDisplay component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIComHotkeyDisplay>();
			component.SetPosition(parent);
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			component.SetText(NKCUIComHotkeyDisplay.MakeHotkeyString(types));
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x001D2C8C File Offset: 0x001D0E8C
		public static void OpenInstance(ScrollRect sr, Transform trans = null)
		{
			if (sr == null)
			{
				return;
			}
			if (!sr.gameObject.activeInHierarchy)
			{
				return;
			}
			if (trans == null)
			{
				if (sr.viewport != null)
				{
					trans = sr.viewport;
				}
				else
				{
					trans = sr.transform;
				}
			}
			if (sr.vertical && sr.horizontal)
			{
				NKCUIComHotkeyDisplay.OpenInstance(trans, new HotkeyEventType[]
				{
					HotkeyEventType.Left,
					HotkeyEventType.Down,
					HotkeyEventType.Up,
					HotkeyEventType.Right
				});
				return;
			}
			if (sr.vertical)
			{
				NKCUIComHotkeyDisplay.OpenInstance(trans, new HotkeyEventType[]
				{
					HotkeyEventType.Down,
					HotkeyEventType.Up
				});
				return;
			}
			if (sr.horizontal)
			{
				NKCUIComHotkeyDisplay.OpenInstance(trans, new HotkeyEventType[]
				{
					HotkeyEventType.Left,
					HotkeyEventType.Right
				});
			}
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x001D2D3C File Offset: 0x001D0F3C
		public static void OpenInstance(LoopScrollRect sr, Transform trans = null)
		{
			if (sr == null)
			{
				return;
			}
			if (!sr.gameObject.activeInHierarchy)
			{
				return;
			}
			if (trans == null)
			{
				if (sr.viewport != null)
				{
					trans = sr.viewport;
				}
				else
				{
					trans = sr.transform;
				}
			}
			if (sr.vertical && sr.horizontal)
			{
				NKCUIComHotkeyDisplay.OpenInstance(trans, new HotkeyEventType[]
				{
					HotkeyEventType.Left,
					HotkeyEventType.Down,
					HotkeyEventType.Up,
					HotkeyEventType.Right
				});
				return;
			}
			if (sr.vertical)
			{
				NKCUIComHotkeyDisplay.OpenInstance(trans, new HotkeyEventType[]
				{
					HotkeyEventType.Down,
					HotkeyEventType.Up
				});
				return;
			}
			if (sr.horizontal)
			{
				NKCUIComHotkeyDisplay.OpenInstance(trans, new HotkeyEventType[]
				{
					HotkeyEventType.Left,
					HotkeyEventType.Right
				});
			}
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x001D2DEC File Offset: 0x001D0FEC
		private static string MakeHotkeyString(HotkeyEventType[] types)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 0; i < types.Length; i++)
			{
				string hotkeyString = NKCInputManager.GetInputManager().GetHotkeyString(types[i]);
				if (!string.IsNullOrEmpty(hotkeyString))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(hotkeyString);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x001D2E48 File Offset: 0x001D1048
		private void SetPosition(Transform parent)
		{
			RectTransform uibaseRect = NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIOverlay);
			base.transform.SetParent(uibaseRect);
			RectTransform rectTransform = parent as RectTransform;
			if (rectTransform != null)
			{
				base.transform.position = rectTransform.GetCenterWorldPos();
				return;
			}
			base.transform.position = parent.position;
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x001D2E9B File Offset: 0x001D109B
		public void SetText(string str)
		{
			NKCUtil.SetLabelText(this.m_lbText, str);
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x001D2EA9 File Offset: 0x001D10A9
		private void Update()
		{
			if (!NKCInputManager.IsHotkeyPressed(HotkeyEventType.ShowHotkey))
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x001D2EC0 File Offset: 0x001D10C0
		private void OnDisable()
		{
			this.CloseInstance();
		}

		// Token: 0x06005E41 RID: 24129 RVA: 0x001D2EC8 File Offset: 0x001D10C8
		private void CloseInstance()
		{
			if (this.m_NKCAssetInstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
				this.m_NKCAssetInstanceData = null;
			}
		}

		// Token: 0x04004A71 RID: 19057
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_HOTKEY";

		// Token: 0x04004A72 RID: 19058
		private const string ASSET_NAME = "NKM_UI_HOTKEY";

		// Token: 0x04004A73 RID: 19059
		public Text m_lbText;

		// Token: 0x04004A74 RID: 19060
		private NKCAssetInstanceData m_NKCAssetInstanceData;
	}
}
