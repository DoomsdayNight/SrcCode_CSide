using System;
using System.Collections;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007C7 RID: 1991
	public class NKCUILifetimeContract : MonoBehaviour
	{
		// Token: 0x06004EB9 RID: 20153 RVA: 0x0017BF38 File Offset: 0x0017A138
		public void Init(NKCUILifetimeContract.OnEndDrag onEndDrag, NKCUILifetimeContract.OnEndAni onEndAni)
		{
			this.m_comDrag.BeginDrag.RemoveAllListeners();
			this.m_comDrag.BeginDrag.AddListener(new UnityAction<PointerEventData>(this.OnDragStart));
			this.m_comDrag.Drag.RemoveAllListeners();
			this.m_comDrag.Drag.AddListener(new UnityAction<PointerEventData>(this.OnDraging));
			this.m_comDrag.EndDrag.RemoveAllListeners();
			this.m_comDrag.EndDrag.AddListener(new UnityAction<PointerEventData>(this.OnDragEnd));
			this.dOnEndDrag = onEndDrag;
			this.dOnEndAni = onEndAni;
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0017BFD8 File Offset: 0x0017A1D8
		public void SetData(NKMUnitData unit)
		{
			this.m_bDrag = false;
			Vector3 localPosition = this.m_objStamp.transform.localPosition;
			localPosition.y = this.m_fStampInitY;
			this.m_objStamp.transform.localPosition = localPosition;
			NKCUtil.SetGameobjectActive(this.m_objHelp, true);
			NKCUtil.SetGameobjectActive(this.m_objArrow, true);
			NKCUtil.SetGameobjectActive(this.m_objPlayerStamp, false);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unit.m_UnitID);
			if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC)
			{
				this.m_txtTitle.text = NKCUtilString.GET_STRING_LIFETIME_CONTRACT_TITLE_MECHANIC;
			}
			else
			{
				this.m_txtTitle.text = NKCUtilString.GET_STRING_LIFETIME_CONTRACT_TITLE;
			}
			this.m_txtSubTitle.text = NKCUtilString.GetUnitStyleString(unitTempletBase);
			this.m_imgUnitBG.sprite = this.GetUnitGradeBG(unitTempletBase.m_NKM_UNIT_GRADE);
			this.m_imgUnitFace.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unit);
			this.m_txtUnitName.text = unitTempletBase.GetUnitName();
			this.m_txtDesc.text = NKCUtilString.GetLifetimeContractDesc(unitTempletBase, NKCScenManager.CurrentUserData().m_UserNickName);
			NKCUtil.SetLabelText(this.m_txtUnitSign, NKCUtilString.GET_STRING_LIFETIME_CONTRACT_UNIT_SIGN_TWO_PARAM, new object[]
			{
				unitTempletBase.GetUnitTitle(),
				unitTempletBase.GetUnitName()
			});
			this.m_txtUnitStamp.text = unitTempletBase.GetUnitName();
			NKCUtil.SetLabelText(this.m_txtPlayerSign, NKCUtilString.GET_STRING_LIFETIME_CONTRACT_PLAYER_SIGN_ONE_PARAM, new object[]
			{
				NKCScenManager.CurrentUserData().m_UserNickName
			});
			this.m_objStamp.GetComponent<Image>().color = Color.white;
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0017C14D File Offset: 0x0017A34D
		public void PlayAni()
		{
			this.m_aniContract.Play("NKM_UI_UNIT_INFO_LIFETIME_OUTRO");
			base.StartCoroutine(this.EndAni());
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x0017C16C File Offset: 0x0017A36C
		private IEnumerator EndAni()
		{
			do
			{
				yield return null;
			}
			while (this.m_aniContract.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f);
			NKCUILifetimeContract.OnEndAni onEndAni = this.dOnEndAni;
			if (onEndAni != null)
			{
				onEndAni();
			}
			yield break;
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0017C17B File Offset: 0x0017A37B
		private void OnDragStart(PointerEventData eventData)
		{
			NKCUtil.SetGameobjectActive(this.m_objHelp, false);
			NKCUtil.SetGameobjectActive(this.m_objArrow, false);
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0017C198 File Offset: 0x0017A398
		private void OnDraging(PointerEventData eventData)
		{
			if (this.m_bDrag)
			{
				return;
			}
			Vector3 localPosition = this.m_objStamp.transform.localPosition;
			localPosition.y += eventData.delta.y;
			if (localPosition.y < this.m_fStampMinY)
			{
				localPosition.y = this.m_fStampMinY;
			}
			else if (localPosition.y > this.m_fStampMaxY)
			{
				localPosition.y = this.m_fStampMaxY;
			}
			this.m_objStamp.transform.localPosition = localPosition;
			if (localPosition.y <= this.m_fSealY)
			{
				this.m_bDrag = true;
				NKCUtil.SetGameobjectActive(this.m_objPlayerStamp, true);
				NKCUILifetimeContract.OnEndDrag onEndDrag = this.dOnEndDrag;
				if (onEndDrag == null)
				{
					return;
				}
				onEndDrag();
			}
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0017C250 File Offset: 0x0017A450
		private void OnDragEnd(PointerEventData eventData)
		{
			if (this.m_bDrag)
			{
				return;
			}
			Vector3 localPosition = this.m_objStamp.transform.localPosition;
			if (localPosition.y > this.m_fSealY)
			{
				localPosition.y = this.m_fStampInitY;
				this.m_objStamp.transform.localPosition = localPosition;
				NKCUtil.SetGameobjectActive(this.m_objHelp, true);
				NKCUtil.SetGameobjectActive(this.m_objArrow, true);
				return;
			}
			this.m_bDrag = true;
			NKCUtil.SetGameobjectActive(this.m_objPlayerStamp, true);
			NKCUILifetimeContract.OnEndDrag onEndDrag = this.dOnEndDrag;
			if (onEndDrag == null)
			{
				return;
			}
			onEndDrag();
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x0017C2E0 File Offset: 0x0017A4E0
		private Sprite GetUnitGradeBG(NKM_UNIT_GRADE grade)
		{
			string bundleName = "ab_ui_unit_slot_card_sprite";
			string assetName;
			switch (grade)
			{
			default:
				assetName = "NKM_UI_UNIT_SELECT_LIST_GRADE_N";
				break;
			case NKM_UNIT_GRADE.NUG_R:
				assetName = "NKM_UI_UNIT_SELECT_LIST_GRADE_R";
				break;
			case NKM_UNIT_GRADE.NUG_SR:
				assetName = "NKM_UI_UNIT_SELECT_LIST_GRADE_SR";
				break;
			case NKM_UNIT_GRADE.NUG_SSR:
				assetName = "NKM_UI_UNIT_SELECT_LIST_GRADE_SSR";
				break;
			}
			NKCAssetResourceData assetResource = NKCResourceUtility.GetAssetResource(bundleName, assetName);
			if (assetResource != null)
			{
				return assetResource.GetAsset<Sprite>();
			}
			return null;
		}

		// Token: 0x04003E75 RID: 15989
		public Animator m_aniContract;

		// Token: 0x04003E76 RID: 15990
		public NKCUIComDrag m_comDrag;

		// Token: 0x04003E77 RID: 15991
		[Header("드래그 시작하면 끔")]
		public GameObject m_objHelp;

		// Token: 0x04003E78 RID: 15992
		public GameObject m_objArrow;

		// Token: 0x04003E79 RID: 15993
		[Header("도장 위치")]
		public GameObject m_objStamp;

		// Token: 0x04003E7A RID: 15994
		public float m_fStampMaxY;

		// Token: 0x04003E7B RID: 15995
		public float m_fStampMinY;

		// Token: 0x04003E7C RID: 15996
		public float m_fStampInitY;

		// Token: 0x04003E7D RID: 15997
		public float m_fSealY;

		// Token: 0x04003E7E RID: 15998
		[Header("도장 찍고나면 킴")]
		public GameObject m_objPlayerStamp;

		// Token: 0x04003E7F RID: 15999
		[Header("계약서")]
		public Text m_txtTitle;

		// Token: 0x04003E80 RID: 16000
		public Text m_txtSubTitle;

		// Token: 0x04003E81 RID: 16001
		public Image m_imgUnitFace;

		// Token: 0x04003E82 RID: 16002
		public Image m_imgUnitBG;

		// Token: 0x04003E83 RID: 16003
		public Text m_txtUnitName;

		// Token: 0x04003E84 RID: 16004
		public Text m_txtDesc;

		// Token: 0x04003E85 RID: 16005
		public Text m_txtUnitSign;

		// Token: 0x04003E86 RID: 16006
		public Text m_txtPlayerSign;

		// Token: 0x04003E87 RID: 16007
		public Text m_txtUnitStamp;

		// Token: 0x04003E88 RID: 16008
		private bool m_bDrag;

		// Token: 0x04003E89 RID: 16009
		private NKCUILifetimeContract.OnEndDrag dOnEndDrag;

		// Token: 0x04003E8A RID: 16010
		private NKCUILifetimeContract.OnEndAni dOnEndAni;

		// Token: 0x02001480 RID: 5248
		// (Invoke) Token: 0x0600A903 RID: 43267
		public delegate void OnEndDrag();

		// Token: 0x02001481 RID: 5249
		// (Invoke) Token: 0x0600A907 RID: 43271
		public delegate void OnEndAni();
	}
}
