using System;
using NKC.UI.Contract;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Event
{
	// Token: 0x02000BDD RID: 3037
	public class NKCUIEventSubUIContract : NKCUIEventSubUIBase
	{
		// Token: 0x06008CF0 RID: 36080 RVA: 0x002FEEB0 File Offset: 0x002FD0B0
		public override void Init()
		{
			base.Init();
			EventTrigger component = base.GetComponent<EventTrigger>();
			if (component != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnTouch));
				component.triggers.Clear();
				component.triggers.Add(entry);
			}
		}

		// Token: 0x06008CF1 RID: 36081 RVA: 0x002FEF10 File Offset: 0x002FD110
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_tabTemplet = tabTemplet;
			if (this.m_tabTemplet != null)
			{
				this.m_ShortcutType = this.m_tabTemplet.m_ShortCutType;
				this.m_ShortcutParam = this.m_tabTemplet.m_ShortCut;
				base.SetDateLimit();
				if (this.m_trBannerParent.childCount == 0)
				{
					this.OpenInstanceByAssetName<NKCUIContractBanner>(this.m_tabTemplet.m_EventBannerPrefabName, this.m_tabTemplet.m_EventBannerPrefabName, this.m_trBannerParent).SetActiveEventTag(true);
				}
			}
		}

		// Token: 0x06008CF2 RID: 36082 RVA: 0x002FEF89 File Offset: 0x002FD189
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_AssetData);
			this.m_AssetData = null;
		}

		// Token: 0x06008CF3 RID: 36083 RVA: 0x002FEF9D File Offset: 0x002FD19D
		public override void Refresh()
		{
		}

		// Token: 0x06008CF4 RID: 36084 RVA: 0x002FEF9F File Offset: 0x002FD19F
		private void OnTouch(BaseEventData baseEventData)
		{
			if (this.m_ShortcutType == NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				return;
			}
			if (!base.CheckEventTime(true))
			{
				return;
			}
			NKCContentManager.MoveToShortCut(this.m_ShortcutType, this.m_ShortcutParam, false);
		}

		// Token: 0x06008CF5 RID: 36085 RVA: 0x002FEFC8 File Offset: 0x002FD1C8
		public T OpenInstanceByAssetName<T>(string BundleName, string AssetName, Transform parent) where T : MonoBehaviour
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(BundleName, AssetName, false, parent);
			if (nkcassetInstanceData == null || !(nkcassetInstanceData.m_Instant != null))
			{
				Debug.LogWarning("prefab is null - " + BundleName + "/" + AssetName);
				return default(T);
			}
			GameObject instant = nkcassetInstanceData.m_Instant;
			T component = instant.GetComponent<T>();
			if (component == null)
			{
				UnityEngine.Object.Destroy(instant);
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				return default(T);
			}
			this.m_AssetData = nkcassetInstanceData;
			return component;
		}

		// Token: 0x040079D0 RID: 31184
		public NKM_SHORTCUT_TYPE m_ShortcutType;

		// Token: 0x040079D1 RID: 31185
		public string m_ShortcutParam;

		// Token: 0x040079D2 RID: 31186
		public Transform m_trBannerParent;

		// Token: 0x040079D3 RID: 31187
		private NKCAssetInstanceData m_AssetData;
	}
}
