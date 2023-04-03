using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F3 RID: 2035
	public class NKCPopupEmoticonSlotComment : MonoBehaviour
	{
		// Token: 0x060050A0 RID: 20640 RVA: 0x00186297 File Offset: 0x00184497
		public void SetClickEvent(NKCPopupEmoticonSlotComment.dOnClick _dOnClick)
		{
			this.m_dOnClick = _dOnClick;
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x001862A0 File Offset: 0x001844A0
		public void SetClickEventForChange(NKCPopupEmoticonSlotComment.dOnClick _dOnClickForChange)
		{
			this.m_dOnClickForChange = _dOnClickForChange;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x001862AC File Offset: 0x001844AC
		public static NKCPopupEmoticonSlotComment GetNewInstanceLarge(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_EMOTICON", "NKM_UI_EMOTICON_SLOT_COMMENT_LARGE", false, null);
			NKCPopupEmoticonSlotComment component = nkcassetInstanceData.m_Instant.GetComponent<NKCPopupEmoticonSlotComment>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCPopupEmoticonSlotComment Prefab null!");
				return null;
			}
			component.m_cNKCAssetInstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x00186350 File Offset: 0x00184550
		private void Start()
		{
			this.m_csbtnComment.PointerClick.RemoveAllListeners();
			this.m_csbtnComment.PointerClick.AddListener(new UnityAction(this.OnClick));
			if (this.m_csbtnCommentForChange != null)
			{
				this.m_csbtnCommentForChange.PointerClick.RemoveAllListeners();
				this.m_csbtnCommentForChange.PointerClick.AddListener(new UnityAction(this.OnClickForChange));
			}
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x001863C3 File Offset: 0x001845C3
		public void PlayChangeEffect()
		{
			if (this.m_amtorChangeEffect != null)
			{
				this.m_amtorChangeEffect.Play("NKM_UI_COMMENT_CHANGE_BASE", -1, 0f);
			}
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x001863E9 File Offset: 0x001845E9
		public int GetEmoticonID()
		{
			return this.m_EmoticonID;
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x001863F4 File Offset: 0x001845F4
		public void SetUI(int emoticonID)
		{
			this.m_EmoticonID = emoticonID;
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(this.m_EmoticonID);
			if (nkmemoticonTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbComment, nkmemoticonTemplet.GetEmoticonName());
				if (this.m_imgBG != null)
				{
					if (nkmemoticonTemplet.m_EmoticonaAnimationName == "WHITE")
					{
						this.m_imgBG.sprite = this.m_spBGWhite;
						return;
					}
					if (nkmemoticonTemplet.m_EmoticonaAnimationName == "GOLD")
					{
						this.m_imgBG.sprite = this.m_spBGGold;
					}
				}
			}
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x0018647D File Offset: 0x0018467D
		private void OnClick()
		{
			if (this.m_dOnClick != null)
			{
				this.m_dOnClick(this.m_EmoticonID);
			}
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00186498 File Offset: 0x00184698
		private void OnClickForChange()
		{
			if (this.m_dOnClickForChange != null)
			{
				this.m_dOnClickForChange(this.m_EmoticonID);
			}
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x001864B3 File Offset: 0x001846B3
		public void SetSelected(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelected, bSet);
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x001864C1 File Offset: 0x001846C1
		public bool GetSelected()
		{
			return this.m_objSelected.activeSelf;
		}

		// Token: 0x04004098 RID: 16536
		public Text m_lbComment;

		// Token: 0x04004099 RID: 16537
		public NKCUIComStateButton m_csbtnComment;

		// Token: 0x0400409A RID: 16538
		public NKCUIComStateButton m_csbtnCommentForChange;

		// Token: 0x0400409B RID: 16539
		public GameObject m_objSelected;

		// Token: 0x0400409C RID: 16540
		public Animator m_amtorChangeEffect;

		// Token: 0x0400409D RID: 16541
		public Image m_imgBG;

		// Token: 0x0400409E RID: 16542
		public Sprite m_spBGWhite;

		// Token: 0x0400409F RID: 16543
		public Sprite m_spBGGold;

		// Token: 0x040040A0 RID: 16544
		private int m_EmoticonID;

		// Token: 0x040040A1 RID: 16545
		private NKCPopupEmoticonSlotComment.dOnClick m_dOnClick;

		// Token: 0x040040A2 RID: 16546
		private NKCPopupEmoticonSlotComment.dOnClick m_dOnClickForChange;

		// Token: 0x040040A3 RID: 16547
		private NKCAssetInstanceData m_cNKCAssetInstanceData;

		// Token: 0x020014AF RID: 5295
		// (Invoke) Token: 0x0600A9A5 RID: 43429
		public delegate void dOnClick(int emoticonID);
	}
}
