using System;
using System.Collections.Generic;
using ClientPacket.Office;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000972 RID: 2418
	public abstract class NKCUIBase : MonoBehaviour
	{
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06006184 RID: 24964 RVA: 0x001E918D File Offset: 0x001E738D
		public bool IsOpen
		{
			get
			{
				return this.m_bOpen;
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06006185 RID: 24965 RVA: 0x001E9195 File Offset: 0x001E7395
		public bool IsHidden
		{
			get
			{
				return this.m_bHide;
			}
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x001E919D File Offset: 0x001E739D
		protected static NKCAssetResourceData OpenInstanceAsync<T>(string BundleName, string AssetName) where T : NKCUIBase
		{
			return NKCAssetResourceManager.OpenResource<GameObject>(BundleName, AssetName, true, null);
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x001E91A8 File Offset: 0x001E73A8
		public static T OpenInstance<T>(NKMAssetName assetName, NKCUIManager.eUIBaseRect baseRect, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(assetName, NKCUIManager.GetUIBaseRect(baseRect), onCleanupInstance).GetInstance<T>();
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x001E91BC File Offset: 0x001E73BC
		public static T OpenInstance<T>(string bundleName, string assetName, NKCUIManager.eUIBaseRect baseRect, NKCUIManager.LoadedUIData.OnCleanupInstance onCleanupInstance) where T : NKCUIBase
		{
			return NKCUIManager.OpenNewInstance<T>(bundleName, assetName, NKCUIManager.GetUIBaseRect(baseRect), onCleanupInstance).GetInstance<T>();
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x001E91D4 File Offset: 0x001E73D4
		protected static bool CheckInstanceLoaded<T>(NKCAssetResourceData assetData, Transform parent, out T instance) where T : NKCUIBase
		{
			if (!assetData.IsDone())
			{
				instance = default(T);
				return false;
			}
			GameObject asset = assetData.GetAsset<GameObject>();
			if (asset != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(asset, parent);
				instance = gameObject.GetComponent<T>();
				if (instance != null)
				{
					NKCUIManager.OpenUI(instance.gameObject);
				}
				return true;
			}
			instance = default(T);
			return true;
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x001E9245 File Offset: 0x001E7445
		public override string ToString()
		{
			return string.Format("{0}({1}){2}", this.MenuName, base.gameObject.name, (this.m_bOpen && this.m_bHide) ? "(Hide)" : "");
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600618B RID: 24971
		public abstract NKCUIBase.eMenutype eUIType { get; }

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x0600618C RID: 24972 RVA: 0x001E927E File Offset: 0x001E747E
		public virtual NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (!this.IsFullScreenUI)
				{
					return NKCUIUpsideMenu.eMode.Invalid;
				}
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x0600618D RID: 24973 RVA: 0x001E928B File Offset: 0x001E748B
		public bool IsFullScreenUI
		{
			get
			{
				return this.eUIType == NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x0600618E RID: 24974 RVA: 0x001E9296 File Offset: 0x001E7496
		public bool IsPopupUI
		{
			get
			{
				return this.eUIType == NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x0600618F RID: 24975 RVA: 0x001E92A1 File Offset: 0x001E74A1
		public virtual bool WillCloseUnderPopupOnOpen
		{
			get
			{
				return this.IsFullScreenUI;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06006190 RID: 24976 RVA: 0x001E92A9 File Offset: 0x001E74A9
		public virtual NKCUIBase.eTransitionEffectType eTransitionEffect
		{
			get
			{
				return NKCUIBase.eTransitionEffectType.None;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06006191 RID: 24977
		public abstract string MenuName { get; }

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06006192 RID: 24978 RVA: 0x001E92AC File Offset: 0x001E74AC
		public virtual List<int> UpsideMenuShowResourceList
		{
			get
			{
				return NKCUIBase.DEFAULT_RESOURCE_LIST;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06006193 RID: 24979 RVA: 0x001E92B3 File Offset: 0x001E74B3
		public virtual NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x001E92B6 File Offset: 0x001E74B6
		public virtual bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06006195 RID: 24981 RVA: 0x001E92B9 File Offset: 0x001E74B9
		public virtual bool DisableSubMenu
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x001E92BC File Offset: 0x001E74BC
		protected void UIOpened(bool bSetAnchoredPosToZero = true)
		{
			if (bSetAnchoredPosToZero)
			{
				RectTransform component = base.GetComponent<RectTransform>();
				if (component != null)
				{
					component.anchoredPosition = Vector2.zero;
				}
			}
			this.UIPrepare();
			this.UIReady();
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x001E92F3 File Offset: 0x001E74F3
		protected void UIPrepare()
		{
			NKCUIManager.UIPrepare(this);
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x001E92FB File Offset: 0x001E74FB
		protected void UIReady()
		{
			NKCUIManager.UIReady(this);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x001E9303 File Offset: 0x001E7503
		public virtual void Activate()
		{
			base.gameObject.SetActive(true);
			this.m_bOpen = true;
			this.m_bHide = false;
			this.SetupScrollRects(base.gameObject);
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x001E932C File Offset: 0x001E752C
		protected void SetupScrollRects(GameObject go)
		{
			float scrollSensibility = NKCInputManager.ScrollSensibility;
			foreach (ScrollRect scrollRect in go.GetComponentsInChildren<ScrollRect>(true))
			{
				if (scrollRect.horizontal && !scrollRect.vertical)
				{
					scrollRect.scrollSensitivity = -scrollSensibility;
				}
				else
				{
					scrollRect.scrollSensitivity = scrollSensibility;
				}
			}
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x001E937A File Offset: 0x001E757A
		protected void UpdateUpsideMenu()
		{
			NKCUIManager.UpdateUpsideMenu();
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x001E9381 File Offset: 0x001E7581
		public virtual bool IsResetUpsideMenuWhenOpenAgain()
		{
			return false;
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600619D RID: 24989 RVA: 0x001E9384 File Offset: 0x001E7584
		public virtual string GuideTempletID
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x001E938B File Offset: 0x001E758B
		public virtual Vector3 GetBackgroundDimension()
		{
			return new Vector3(1024f, 1024f, 2000f);
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x001E93A1 File Offset: 0x001E75A1
		public virtual Sprite GetBackgroundSprite()
		{
			return null;
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x001E93A4 File Offset: 0x001E75A4
		public virtual Color GetBackgroundColor()
		{
			return Color.white;
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x001E93AB File Offset: 0x001E75AB
		public virtual void UpdateCamera()
		{
			if (!NKCCamera.IsTrackingCameraPos())
			{
				NKCCamera.TrackingPos(10f, NKMRandom.Range(-50f, 50f), NKMRandom.Range(-50f, 50f), NKMRandom.Range(-1000f, -900f));
			}
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x001E93EC File Offset: 0x001E75EC
		public void OnDragBackground(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			float num = NKCCamera.GetPosNowX(false) - pointerEventData.delta.x * 10f;
			float num2 = NKCCamera.GetPosNowY(false) - pointerEventData.delta.y * 10f;
			num = Mathf.Clamp(num, -100f, 100f);
			num2 = Mathf.Clamp(num2, -100f, 100f);
			NKCCamera.TrackingPos(1f, num, num2, -1f);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x001E9465 File Offset: 0x001E7665
		public virtual void Initialize()
		{
			Debug.LogError(string.Format("{0} : Initialize() Not Implemented!", base.GetType()));
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x001E947C File Offset: 0x001E767C
		public virtual void OpenByShortcut(Dictionary<string, string> dicParam)
		{
			Debug.LogError(string.Format("{0} : OpenByShortcut() Not Implemented!", base.GetType()));
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x001E9493 File Offset: 0x001E7693
		public void Close()
		{
			if (!this.m_bOpen)
			{
				return;
			}
			this.m_bOpen = false;
			NKCUIManager.UIClosed(this);
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x001E94AB File Offset: 0x001E76AB
		internal void _ForceCloseInternal()
		{
			this.m_bOpen = false;
			this.CloseInternal();
		}

		// Token: 0x060061A7 RID: 24999
		public abstract void CloseInternal();

		// Token: 0x060061A8 RID: 25000 RVA: 0x001E94BA File Offset: 0x001E76BA
		public virtual void OnCloseInstance()
		{
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x001E94BC File Offset: 0x001E76BC
		public virtual void Hide()
		{
			this.m_bHide = true;
			base.gameObject.SetActive(false);
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x001E94D1 File Offset: 0x001E76D1
		public virtual void UnHide()
		{
			this.m_bHide = false;
			base.gameObject.SetActive(true);
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x001E94E6 File Offset: 0x001E76E6
		public virtual void OnBackButton()
		{
			this.Close();
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x001E94EE File Offset: 0x001E76EE
		public virtual bool OnHomeButton()
		{
			return true;
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x001E94F1 File Offset: 0x001E76F1
		public virtual void OnUserLevelChanged(NKMUserData userData)
		{
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x001E94F3 File Offset: 0x001E76F3
		public virtual void OnInventoryChange(NKMItemMiscData itemData)
		{
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x001E94F5 File Offset: 0x001E76F5
		public virtual void OnInteriorInventoryUpdate(NKMInteriorData interiorData, bool bAdded)
		{
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x001E94F7 File Offset: 0x001E76F7
		public virtual void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x001E94F9 File Offset: 0x001E76F9
		public virtual void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x001E94FB File Offset: 0x001E76FB
		public virtual void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData)
		{
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x001E94FD File Offset: 0x001E76FD
		public virtual void OnDeckUpdate(NKMDeckIndex deckIndex, NKMDeckData deckData)
		{
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x001E94FF File Offset: 0x001E76FF
		public virtual void OnCompanyBuffUpdate(NKMUserData userData)
		{
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x001E9504 File Offset: 0x001E7704
		public virtual void OnScreenResolutionChanged()
		{
			LoopScrollRect[] componentsInChildren = base.GetComponentsInChildren<LoopScrollRect>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Repopulate();
			}
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x001E952F File Offset: 0x001E772F
		public virtual void OnGuildDataChanged()
		{
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x001E9531 File Offset: 0x001E7731
		public virtual void OnMissionUpdated()
		{
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001E9533 File Offset: 0x001E7733
		public virtual bool OnHotkey(HotkeyEventType hotkey)
		{
			return false;
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x001E9536 File Offset: 0x001E7736
		public virtual void OnHotkeyHold(HotkeyEventType hotkey)
		{
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x001E9538 File Offset: 0x001E7738
		public virtual void OnHotkeyRelease(HotkeyEventType hotkey)
		{
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x001E953A File Offset: 0x001E773A
		protected static void SetGameObjectActive(GameObject targetObj, bool bValue)
		{
			if (targetObj != null && targetObj.activeSelf != bValue)
			{
				targetObj.SetActive(bValue);
			}
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x001E9555 File Offset: 0x001E7755
		protected static void SetGameObjectActive(Transform targetTransform, bool bValue)
		{
			if (targetTransform != null && targetTransform.gameObject.activeSelf != bValue)
			{
				targetTransform.gameObject.SetActive(bValue);
			}
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x001E957A File Offset: 0x001E777A
		protected static void SetGameobjectActive(MonoBehaviour targetMono, bool bValue)
		{
			if (targetMono != null && targetMono.gameObject.activeSelf != bValue)
			{
				targetMono.gameObject.SetActive(bValue);
			}
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x001E959F File Offset: 0x001E779F
		protected static void SetLabelText(Text label, string msg)
		{
			if (label != null)
			{
				label.text = msg;
			}
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x001E95B1 File Offset: 0x001E77B1
		protected static void SetLabelTextColor(Text label, Color col)
		{
			if (label != null)
			{
				label.color = col;
			}
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x001E95C3 File Offset: 0x001E77C3
		protected static void SetLabelText(Text label, string msg, params object[] args)
		{
			if (label != null)
			{
				label.text = string.Format(msg, args);
			}
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x001E95DB File Offset: 0x001E77DB
		protected static void SetImageSprite(Image image, Sprite sp, bool bDisableIfSpriteNull = false)
		{
			if (image != null)
			{
				image.sprite = sp;
			}
			if (bDisableIfSpriteNull)
			{
				NKCUIBase.SetGameobjectActive(image, sp != null);
			}
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x001E95FD File Offset: 0x001E77FD
		protected static void SetImageColor(Image image, Color color)
		{
			if (image != null)
			{
				image.color = color;
			}
		}

		// Token: 0x04004DBB RID: 19899
		protected bool m_bOpen;

		// Token: 0x04004DBC RID: 19900
		protected bool m_bHide;

		// Token: 0x04004DBD RID: 19901
		private static readonly List<int> DEFAULT_RESOURCE_LIST = new List<int>
		{
			1,
			2,
			101
		};

		// Token: 0x02001611 RID: 5649
		public enum eMenutype
		{
			// Token: 0x0400A310 RID: 41744
			FullScreen,
			// Token: 0x0400A311 RID: 41745
			Popup,
			// Token: 0x0400A312 RID: 41746
			Overlay
		}

		// Token: 0x02001612 RID: 5650
		public enum eTransitionEffectType
		{
			// Token: 0x0400A314 RID: 41748
			None,
			// Token: 0x0400A315 RID: 41749
			SmallLoading,
			// Token: 0x0400A316 RID: 41750
			FullScreenLoading,
			// Token: 0x0400A317 RID: 41751
			FadeInOut
		}
	}
}
