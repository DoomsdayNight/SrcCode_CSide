using System;
using ClientPacket.Common;
using NKC.Trim;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AAD RID: 2733
	public class NKCUITrimLevelSlot : MonoBehaviour
	{
		// Token: 0x060079A3 RID: 31139 RVA: 0x00287B94 File Offset: 0x00285D94
		public static NKCUITrimLevelSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_TRIM", "AB_UI_TRIM_DUNGEON_TAB", false, null);
			NKCUITrimLevelSlot nkcuitrimLevelSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUITrimLevelSlot>() : null;
			if (nkcuitrimLevelSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUITrimLevelSlot  Prefab null!");
				return null;
			}
			nkcuitrimLevelSlot.m_InstanceData = nkcassetInstanceData;
			nkcuitrimLevelSlot.Init();
			if (parent != null)
			{
				nkcuitrimLevelSlot.transform.SetParent(parent);
			}
			nkcuitrimLevelSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuitrimLevelSlot.gameObject.SetActive(false);
			return nkcuitrimLevelSlot;
		}

		// Token: 0x060079A4 RID: 31140 RVA: 0x00287C2E File Offset: 0x00285E2E
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x00287C4D File Offset: 0x00285E4D
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnButton, new UnityAction(this.OnClickButton));
			this.m_csbtnButton.m_bGetCallbackWhileLocked = true;
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x00287C74 File Offset: 0x00285E74
		public void SetData(int trimId, int trimLevel, int clearedLevel, NKCUITrimLevelSlot.OnClickSlot onClickSlot)
		{
			this.m_dOnClickSlot = onClickSlot;
			this.m_trimId = trimId;
			this.m_trimLevel = trimLevel;
			string msg = trimLevel.ToString();
			NKCUtil.SetLabelText(this.m_lbTrimLevel, msg);
			NKCUtil.SetLabelText(this.m_lbTrimLevelSel, msg);
			NKCUtil.SetLabelText(this.m_lbTrimLevelLock, msg);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMTrimClearData nkmtrimClearData;
			if (nkmuserData == null)
			{
				nkmtrimClearData = null;
			}
			else
			{
				NKCTrimData trimData = nkmuserData.TrimData;
				nkmtrimClearData = ((trimData != null) ? trimData.GetTrimClearData(trimId, trimLevel) : null);
			}
			NKMTrimClearData nkmtrimClearData2 = nkmtrimClearData;
			string msg2 = ((nkmtrimClearData2 != null) ? nkmtrimClearData2.score : 0).ToString();
			NKCUtil.SetLabelText(this.m_lbScore, msg2);
			NKCUtil.SetLabelText(this.m_lbScoreSel, msg2);
			NKCUtil.SetLabelText(this.m_lbScoreLock, msg2);
			NKCUtil.SetGameobjectActive(this.m_objNew, trimLevel == clearedLevel + 1 && nkmtrimClearData2 == null);
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x00287D34 File Offset: 0x00285F34
		public void SetSelectedState(int selectedLevel)
		{
			NKCUIComStateButton csbtnButton = this.m_csbtnButton;
			if (csbtnButton == null)
			{
				return;
			}
			csbtnButton.Select(this.m_trimLevel == selectedLevel, false, false);
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x00287D52 File Offset: 0x00285F52
		public void SetLock(bool value)
		{
			NKCUIComStateButton csbtnButton = this.m_csbtnButton;
			if (csbtnButton == null)
			{
				return;
			}
			csbtnButton.SetLock(value, false);
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x00287D68 File Offset: 0x00285F68
		private void OnClickButton()
		{
			if (this.m_csbtnButton.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCStringTable.GetString("SI_DP_TRIM_MAIN_NEXT_LEVEL_TEXT", false), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (this.m_dOnClickSlot != null)
			{
				this.m_dOnClickSlot(this.m_trimLevel, this.m_csbtnButton.m_bLock);
			}
		}

		// Token: 0x0400665C RID: 26204
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x0400665D RID: 26205
		public GameObject m_objNew;

		// Token: 0x0400665E RID: 26206
		[Header("Normal")]
		public Text m_lbTrimLevel;

		// Token: 0x0400665F RID: 26207
		public Text m_lbScore;

		// Token: 0x04006660 RID: 26208
		[Header("Selected")]
		public Text m_lbTrimLevelSel;

		// Token: 0x04006661 RID: 26209
		public Text m_lbScoreSel;

		// Token: 0x04006662 RID: 26210
		[Header("Locked")]
		public Text m_lbTrimLevelLock;

		// Token: 0x04006663 RID: 26211
		public Text m_lbScoreLock;

		// Token: 0x04006664 RID: 26212
		private int m_trimId;

		// Token: 0x04006665 RID: 26213
		private int m_trimLevel;

		// Token: 0x04006666 RID: 26214
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04006667 RID: 26215
		private NKCUITrimLevelSlot.OnClickSlot m_dOnClickSlot;

		// Token: 0x02001802 RID: 6146
		// (Invoke) Token: 0x0600B4E3 RID: 46307
		public delegate void OnClickSlot(int trimLevel, bool isLocked);
	}
}
