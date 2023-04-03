using System;
using NKC;
using NKC.UI;
using UnityEngine;

namespace NKM
{
	// Token: 0x0200036A RID: 874
	public class NKCUnitTouchObject
	{
		// Token: 0x0600151C RID: 5404 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		public void Init()
		{
			this.m_UnitTouchObject = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UNIT_GAME_NKM_UNIT", "NKM_GAME_UNIT_TOUCH", false, null);
			if (this.m_UnitTouchObject == null)
			{
				return;
			}
			this.m_UnitTouchObject_RectTransform = this.m_UnitTouchObject.m_Instant.GetComponent<RectTransform>();
			this.m_UnitTouchObject_NKCUIComButton = this.m_UnitTouchObject.m_Instant.GetComponent<NKCUIComButton>();
			this.m_UnitTouchObject_RectTransform.transform.SetParent(NKCUIManager.m_NUF_GAME_TOUCH_OBJECT.transform, false);
			this.m_UnitTouchObject.m_Instant.SetActive(false);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0004F845 File Offset: 0x0004DA45
		public void SetLinkButton(NKCUIComButton cLinkButton)
		{
			this.m_UnitTouchObject_NKCUIComButton.SetLinkButton(cLinkButton);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0004F854 File Offset: 0x0004DA54
		public void SetSize(NKMUnitTemplet cNKMUnitTemplet)
		{
			if (cNKMUnitTemplet.m_UnitSizeX > 200f)
			{
				this.m_UnitTouchObject_RectTransform.SetWidth(cNKMUnitTemplet.m_UnitSizeX);
			}
			if (cNKMUnitTemplet.m_fGageOffsetY > 300f)
			{
				this.m_UnitTouchObject_RectTransform.SetHeight(cNKMUnitTemplet.m_fGageOffsetY + 200f);
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0004F8A3 File Offset: 0x0004DAA3
		public void Close()
		{
			NKCUIComButton unitTouchObject_NKCUIComButton = this.m_UnitTouchObject_NKCUIComButton;
			if (unitTouchObject_NKCUIComButton != null)
			{
				unitTouchObject_NKCUIComButton.SetLinkButton(null);
			}
			if (this.m_UnitTouchObject != null)
			{
				NKCUtil.SetGameobjectActive(this.m_UnitTouchObject.m_Instant, false);
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0004F8D0 File Offset: 0x0004DAD0
		public void Unload()
		{
			NKCAssetResourceManager.CloseInstance(this.m_UnitTouchObject);
			this.m_UnitTouchObject = null;
			this.m_UnitTouchObject_RectTransform = null;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0004F8EC File Offset: 0x0004DAEC
		public void ActiveObject(bool bActive)
		{
			if (bActive)
			{
				if (!this.m_UnitTouchObject.m_Instant.activeSelf)
				{
					this.m_UnitTouchObject.m_Instant.SetActive(true);
					return;
				}
			}
			else if (this.m_UnitTouchObject.m_Instant.activeSelf)
			{
				this.m_UnitTouchObject.m_Instant.SetActive(false);
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004F943 File Offset: 0x0004DB43
		public bool IsActiveObject()
		{
			return this.m_UnitTouchObject.m_Instant.activeSelf;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004F955 File Offset: 0x0004DB55
		public GameObject GetGameObject()
		{
			return this.m_UnitTouchObject.m_Instant;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004F962 File Offset: 0x0004DB62
		public RectTransform GetRectTransform()
		{
			return this.m_UnitTouchObject_RectTransform;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004F96A File Offset: 0x0004DB6A
		public NKCUIComButton GetButton()
		{
			return this.m_UnitTouchObject_NKCUIComButton;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004F972 File Offset: 0x0004DB72
		public void MoveToLastTouchObject()
		{
			this.m_UnitTouchObject_RectTransform.SetAsLastSibling();
		}

		// Token: 0x04000E7A RID: 3706
		private NKCAssetInstanceData m_UnitTouchObject;

		// Token: 0x04000E7B RID: 3707
		private RectTransform m_UnitTouchObject_RectTransform;

		// Token: 0x04000E7C RID: 3708
		private NKCUIComButton m_UnitTouchObject_NKCUIComButton;
	}
}
