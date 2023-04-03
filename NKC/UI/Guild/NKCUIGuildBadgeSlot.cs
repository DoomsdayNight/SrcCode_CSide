using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B4C RID: 2892
	public class NKCUIGuildBadgeSlot : MonoBehaviour
	{
		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060083A2 RID: 33698 RVA: 0x002C5AF3 File Offset: 0x002C3CF3
		// (set) Token: 0x060083A3 RID: 33699 RVA: 0x002C5AFB File Offset: 0x002C3CFB
		public int m_slotId { get; private set; }

		// Token: 0x060083A4 RID: 33700 RVA: 0x002C5B04 File Offset: 0x002C3D04
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
		}

		// Token: 0x060083A5 RID: 33701 RVA: 0x002C5B14 File Offset: 0x002C3D14
		public static NKCUIGuildBadgeSlot GetNewInstance(Transform parent, NKCUIComToggleGroup tglGroup, NKCUIGuildBadgeSlot.OnSelectedSlot selectedSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_MARK_SETTING_SLOT", false, null);
			NKCUIGuildBadgeSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIGuildBadgeSlot>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIGuildBadgeSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			component.SetOnSelectedSlot(selectedSlot);
			component.m_tgl.SetToggleGroup(tglGroup);
			component.m_tgl.OnValueChanged.RemoveAllListeners();
			component.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(component.OnValueChanged));
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060083A6 RID: 33702 RVA: 0x002C5BC4 File Offset: 0x002C3DC4
		public void SetData(NKMGuildBadgeFrameTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgFrame, true);
			NKCUtil.SetGameobjectActive(this.m_imgMark, false);
			NKCUtil.SetGameobjectActive(this.m_imgColor, false);
			NKCUtil.SetImageSprite(this.m_imgFrame, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", templet.BadgeFrameImg, false), false);
			this.m_slotId = templet.ID;
			this.SetLockObject(templet.UnlockInfo, templet.LockVisible);
		}

		// Token: 0x060083A7 RID: 33703 RVA: 0x002C5C40 File Offset: 0x002C3E40
		public void SetData(NKMGuildBadgeMarkTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgFrame, false);
			NKCUtil.SetGameobjectActive(this.m_imgMark, true);
			NKCUtil.SetGameobjectActive(this.m_imgColor, false);
			NKCUtil.SetImageSprite(this.m_imgMark, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Mark", templet.BadgeMarkImg, false), false);
			this.m_slotId = templet.ID;
			this.SetLockObject(templet.UnlockInfo, templet.LockVisible);
		}

		// Token: 0x060083A8 RID: 33704 RVA: 0x002C5CBC File Offset: 0x002C3EBC
		public void SetData(NKMGuildBadgeColorTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgFrame, false);
			NKCUtil.SetGameobjectActive(this.m_imgMark, false);
			NKCUtil.SetGameobjectActive(this.m_imgColor, true);
			NKCUtil.SetImageColor(this.m_imgColor, NKCUtil.GetColor(templet.BadgeColorCode));
			this.m_slotId = templet.ID;
			this.SetLockObject(templet.UnlockInfo, templet.LockVisible);
		}

		// Token: 0x060083A9 RID: 33705 RVA: 0x002C5D31 File Offset: 0x002C3F31
		private void SetLockObject(UnlockInfo unlockInfo, bool bLockVisible)
		{
			if (NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), unlockInfo, false))
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, false);
				return;
			}
			if (bLockVisible)
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, true);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060083AA RID: 33706 RVA: 0x002C5D6B File Offset: 0x002C3F6B
		private void SetOnSelectedSlot(NKCUIGuildBadgeSlot.OnSelectedSlot selectedSlot)
		{
			this.m_dOnSelectedSlot = selectedSlot;
		}

		// Token: 0x060083AB RID: 33707 RVA: 0x002C5D74 File Offset: 0x002C3F74
		private void OnValueChanged(bool bValue)
		{
			if (bValue)
			{
				this.m_dOnSelectedSlot(this.m_slotId);
			}
		}

		// Token: 0x04006FCB RID: 28619
		public NKCUIComToggle m_tgl;

		// Token: 0x04006FCC RID: 28620
		public Image m_imgColor;

		// Token: 0x04006FCD RID: 28621
		public Image m_imgFrame;

		// Token: 0x04006FCE RID: 28622
		public Image m_imgMark;

		// Token: 0x04006FCF RID: 28623
		public GameObject m_objSelected;

		// Token: 0x04006FD0 RID: 28624
		public GameObject m_objLocked;

		// Token: 0x04006FD1 RID: 28625
		private NKCUIGuildBadgeSlot.OnSelectedSlot m_dOnSelectedSlot;

		// Token: 0x04006FD2 RID: 28626
		private NKCAssetInstanceData m_instance;

		// Token: 0x020018E4 RID: 6372
		// (Invoke) Token: 0x0600B715 RID: 46869
		public delegate void OnSelectedSlot(int id);
	}
}
