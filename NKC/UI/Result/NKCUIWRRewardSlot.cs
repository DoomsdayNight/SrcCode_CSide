using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BAD RID: 2989
	public class NKCUIWRRewardSlot : MonoBehaviour
	{
		// Token: 0x06008A06 RID: 35334 RVA: 0x002ECAE8 File Offset: 0x002EACE8
		public static NKCUIWRRewardSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_RESULT", "AB_ICON_SLOT_WF_RESULT", false, null);
			NKCUIWRRewardSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIWRRewardSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIWRRewardSlot Prefab null!");
				return null;
			}
			component.m_instance = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			component.m_AB_ICON_SLOT.Init();
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06008A07 RID: 35335 RVA: 0x002ECB80 File Offset: 0x002EAD80
		public void ManualUpdate()
		{
			if (this.m_bReservedAni)
			{
				this.m_fElapsedTime += Time.deltaTime;
				if (this.m_fElapsedTime > (float)this.m_seqToAni * 0.13333334f)
				{
					NKCUtil.SetGameobjectActive(base.gameObject, true);
					this.m_Animator.Play("AB_ICON_SLOT_RESULT_INTRO");
					this.m_bReservedAni = false;
				}
			}
		}

		// Token: 0x06008A08 RID: 35336 RVA: 0x002ECBDF File Offset: 0x002EADDF
		public void InvalidAni()
		{
			this.m_bReservedAni = false;
		}

		// Token: 0x06008A09 RID: 35337 RVA: 0x002ECBE8 File Offset: 0x002EADE8
		public void SetUI(NKCUISlot.SlotData slotData, int seqToAni)
		{
			this.m_seqToAni = seqToAni;
			this.m_bReservedAni = true;
			this.m_fElapsedTime = 0f;
			bool bShowNumber = slotData.eType == NKCUISlot.eSlotMode.ItemMisc || slotData.eType == NKCUISlot.eSlotMode.Mold || slotData.eType == NKCUISlot.eSlotMode.EquipCount;
			this.m_AB_ICON_SLOT.SetData(slotData, false, bShowNumber, true, null);
			this.m_AB_ICON_SLOT.SetBonusRate(slotData.BonusRate);
			this.m_AB_ICON_SLOT.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
			this.m_AB_ICON_SLOT_RESULT_TEXT.text = NKCUISlot.GetName(slotData.eType, slotData.ID);
		}

		// Token: 0x06008A0A RID: 35338 RVA: 0x002ECC7B File Offset: 0x002EAE7B
		public void SetFirstMark(bool bFirst)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_FIRSTCLEAR, bFirst);
			NKCUtil.SetLabelText(this.m_lbFirstClear, NKCUtilString.GET_STRING_REWARD_FIRST_CLEAR);
		}

		// Token: 0x06008A0B RID: 35339 RVA: 0x002ECC99 File Offset: 0x002EAE99
		public void SetFirstAllClearMark(bool bClear)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_FIRSTCLEAR, bClear);
			NKCUtil.SetLabelText(this.m_lbFirstClear, NKCUtilString.GET_STRING_WARFARE_FIRST_ALL_CLEAR);
		}

		// Token: 0x06008A0C RID: 35340 RVA: 0x002ECCB7 File Offset: 0x002EAEB7
		public void SetContainerMark(bool bContainer)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_CONTAINER, bContainer);
		}

		// Token: 0x06008A0D RID: 35341 RVA: 0x002ECCC5 File Offset: 0x002EAEC5
		public void SetArtifactMark(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_ARTIFACT, bSet);
		}

		// Token: 0x06008A0E RID: 35342 RVA: 0x002ECCD3 File Offset: 0x002EAED3
		public void SetDiveStormMark(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_DIVE_STORM, bSet);
		}

		// Token: 0x06008A0F RID: 35343 RVA: 0x002ECCE1 File Offset: 0x002EAEE1
		public void SetChanceUpMark(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_FIRSTCLEAR, bSet);
			NKCUtil.SetLabelText(this.m_lbFirstClear, NKCUtilString.GET_STRING_REWARD_CHANCE_UP);
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x002ECCFF File Offset: 0x002EAEFF
		public void SetMultiplyMark(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_MULTIPLY_REWARD, bSet);
		}

		// Token: 0x06008A11 RID: 35345 RVA: 0x002ECD0D File Offset: 0x002EAF0D
		public void SetKillRewardMark(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_WF_RESULT_KILL_REWARD, bSet);
		}

		// Token: 0x06008A12 RID: 35346 RVA: 0x002ECD1B File Offset: 0x002EAF1B
		public void Close()
		{
			NKCAssetResourceManager.CloseInstance(this.m_instance);
			this.m_instance = null;
		}

		// Token: 0x04007696 RID: 30358
		public NKCUISlot m_AB_ICON_SLOT;

		// Token: 0x04007697 RID: 30359
		public Text m_AB_ICON_SLOT_RESULT_TEXT;

		// Token: 0x04007698 RID: 30360
		public Animator m_Animator;

		// Token: 0x04007699 RID: 30361
		public GameObject m_AB_ICON_SLOT_WF_RESULT_CONTAINER;

		// Token: 0x0400769A RID: 30362
		public GameObject m_AB_ICON_SLOT_WF_RESULT_FIRSTCLEAR;

		// Token: 0x0400769B RID: 30363
		public Text m_lbFirstClear;

		// Token: 0x0400769C RID: 30364
		public GameObject m_AB_ICON_SLOT_WF_RESULT_ARTIFACT;

		// Token: 0x0400769D RID: 30365
		public GameObject m_AB_ICON_SLOT_WF_RESULT_DIVE_STORM;

		// Token: 0x0400769E RID: 30366
		public GameObject m_AB_ICON_SLOT_WF_RESULT_MULTIPLY_REWARD;

		// Token: 0x0400769F RID: 30367
		public GameObject m_AB_ICON_SLOT_WF_RESULT_KILL_REWARD;

		// Token: 0x040076A0 RID: 30368
		public const float WHITE_IN_ANI_TIME = 0.8333333f;

		// Token: 0x040076A1 RID: 30369
		private float m_fElapsedTime;

		// Token: 0x040076A2 RID: 30370
		private int m_seqToAni;

		// Token: 0x040076A3 RID: 30371
		private bool m_bReservedAni;

		// Token: 0x040076A4 RID: 30372
		private NKCAssetInstanceData m_instance;
	}
}
