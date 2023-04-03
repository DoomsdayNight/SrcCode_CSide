using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A28 RID: 2600
	public class NKCPopupMessageCompanyBuff : NKCUIBase
	{
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x060071D7 RID: 29143 RVA: 0x0025D7E0 File Offset: 0x0025B9E0
		public static NKCPopupMessageCompanyBuff Instance
		{
			get
			{
				if (NKCPopupMessageCompanyBuff.m_Instance == null)
				{
					NKCPopupMessageCompanyBuff.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupMessageCompanyBuff>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_MESSAGE_EVENTBUFF", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupMessageCompanyBuff.CleanupInstance)).GetInstance<NKCPopupMessageCompanyBuff>();
				}
				return NKCPopupMessageCompanyBuff.m_Instance;
			}
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x0025D81A File Offset: 0x0025BA1A
		private static void CleanupInstance()
		{
			NKCPopupMessageCompanyBuff.m_Instance = null;
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x060071D9 RID: 29145 RVA: 0x0025D822 File Offset: 0x0025BA22
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x060071DA RID: 29146 RVA: 0x0025D825 File Offset: 0x0025BA25
		public override string MenuName
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x0025D82C File Offset: 0x0025BA2C
		public void Open(NKMCompanyBuffData buffData, bool bAddBuff)
		{
			this.m_lstBuffData.Enqueue(buffData);
			this.m_lstAddBuff.Enqueue(bAddBuff);
			if (!this.m_bPlaying)
			{
				base.gameObject.SetActive(true);
				base.UIOpened(true);
				base.StartCoroutine(this.Process());
				this.m_bPlaying = true;
			}
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x0025D880 File Offset: 0x0025BA80
		private IEnumerator Process()
		{
			while (this.m_lstBuffData.Count > 0 && this.m_lstAddBuff.Count > 0)
			{
				NKMCompanyBuffData nkmcompanyBuffData = this.m_lstBuffData.Dequeue();
				bool bAddBuff = this.m_lstAddBuff.Dequeue();
				if (nkmcompanyBuffData != null)
				{
					NKMCompanyBuffTemplet nkmcompanyBuffTemplet = NKMTempletContainer<NKMCompanyBuffTemplet>.Find(nkmcompanyBuffData.Id);
					NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
					this.m_imgIcon.sprite = NKCUtil.GetCompanyBuffIconSprite(nkmcompanyBuffTemplet.m_CompanyBuffIcon);
					this.m_lbTitle.text = NKCUtilString.GetCompanyBuffTitle(nkmcompanyBuffTemplet.GetBuffName(), bAddBuff);
					this.m_lbMessage.text = NKCUtil.GetCompanyBuffDesc(nkmcompanyBuffTemplet.m_CompanyBuffID);
					yield return base.StartCoroutine(this.ProcessShowBuff(bAddBuff));
				}
			}
			base.Close();
			yield break;
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x0025D88F File Offset: 0x0025BA8F
		private IEnumerator ProcessShowBuff(bool bAddBuff)
		{
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, true);
			if (bAddBuff)
			{
				this.m_Ani.Play("NKM_UI_POPUP_MESSAGE_EVENTBUFF_INTRO");
			}
			else
			{
				this.m_Ani.Play("NKM_UI_POPUP_MESSAGE_EVENTBUFF_OFF");
			}
			yield return new WaitForSeconds(3f);
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
			yield break;
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x0025D8A5 File Offset: 0x0025BAA5
		public override void CloseInternal()
		{
			this.m_bPlaying = false;
			this.m_lstBuffData.Clear();
			this.m_lstAddBuff.Clear();
			NKCUtil.SetGameobjectActive(this.m_rtMessageRoot, false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x04005DCB RID: 24011
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04005DCC RID: 24012
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_MESSAGE_EVENTBUFF";

		// Token: 0x04005DCD RID: 24013
		private static NKCPopupMessageCompanyBuff m_Instance;

		// Token: 0x04005DCE RID: 24014
		private const float MESSAGE_STAY_TIME = 3f;

		// Token: 0x04005DCF RID: 24015
		public Animator m_Ani;

		// Token: 0x04005DD0 RID: 24016
		public RectTransform m_rtMessageRoot;

		// Token: 0x04005DD1 RID: 24017
		public Image m_imgIcon;

		// Token: 0x04005DD2 RID: 24018
		public Text m_lbTitle;

		// Token: 0x04005DD3 RID: 24019
		public Text m_lbMessage;

		// Token: 0x04005DD4 RID: 24020
		private Queue<NKMCompanyBuffData> m_lstBuffData = new Queue<NKMCompanyBuffData>();

		// Token: 0x04005DD5 RID: 24021
		private Queue<bool> m_lstAddBuff = new Queue<bool>();

		// Token: 0x04005DD6 RID: 24022
		private bool m_bPlaying;
	}
}
