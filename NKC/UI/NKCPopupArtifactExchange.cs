using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A32 RID: 2610
	public class NKCPopupArtifactExchange : NKCUIBase
	{
		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x0600724F RID: 29263 RVA: 0x0025FF56 File Offset: 0x0025E156
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06007250 RID: 29264 RVA: 0x0025FF59 File Offset: 0x0025E159
		public override string MenuName
		{
			get
			{
				return "PopupArtifactExchange";
			}
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x0025FF60 File Offset: 0x0025E160
		public void InitUI()
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			this.m_lsrArtifact.dOnGetObject += this.GetArtifactSlot;
			this.m_lsrArtifact.dOnReturnObject += this.ReturnArtifactSlot;
			this.m_lsrArtifact.dOnProvideData += this.ProvideArtifactSlot;
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x00260004 File Offset: 0x0025E204
		public RectTransform GetArtifactSlot(int index)
		{
			return NKCPopupArtifactExchangeSlot.GetNewInstance(null).GetComponent<RectTransform>();
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x00260011 File Offset: 0x0025E211
		public void ReturnArtifactSlot(Transform tr)
		{
			tr.SetParent(base.transform);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x0026002C File Offset: 0x0025E22C
		public void ProvideArtifactSlot(Transform tr, int index)
		{
			NKCPopupArtifactExchangeSlot component = tr.GetComponent<NKCPopupArtifactExchangeSlot>();
			if (component != null && this.m_lstArtifact.Count > index)
			{
				component.SetData(this.m_lstArtifact[index]);
			}
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x00260069 File Offset: 0x0025E269
		public void Open(List<int> _lstArtifact, int getMiscItemID, NKCPopupArtifactExchange.dOnClose _dOnClose = null)
		{
			this.m_dOnClose = _dOnClose;
			this.m_lstArtifact.Clear();
			if (_lstArtifact != null)
			{
				this.m_lstArtifact.AddRange(_lstArtifact);
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
			this.SetUI(getMiscItemID);
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x002600A8 File Offset: 0x0025E2A8
		private void SetUI(int getMiscItemID)
		{
			if (this.m_bFirstOpen)
			{
				this.m_lsrArtifact.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			this.m_lsrArtifact.TotalCount = this.m_lstArtifact.Count;
			this.m_lsrArtifact.SetIndexPosition(0);
			NKCUtil.SetLabelText(this.m_lbArtifactCount, string.Format(NKCStringTable.GetString("SI_DP_DIVE_ARTIFACT_COUNT_DESC", false), this.m_lstArtifact.Count));
			int num = 0;
			for (int i = 0; i < this.m_lstArtifact.Count; i++)
			{
				NKMDiveArtifactTemplet.Find(this.m_lstArtifact[i]);
			}
			this.m_imgTotalGetItemIcon.sprite = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(getMiscItemID);
			NKCUtil.SetLabelText(this.m_lbTotalGetItemCount, num.ToString());
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(getMiscItemID);
			if (itemMiscTempletByID != null)
			{
				NKCUtil.SetLabelText(this.m_lbTotalGetItem, string.Format(NKCUtilString.GET_STRING_DIVE_ARTIFACT_EXCHANGE_TOTAL_GET_ITEM, itemMiscTempletByID.GetItemName()));
			}
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x0026018F File Offset: 0x0025E38F
		private void Update()
		{
			this.m_NKCUIOpenAnimator.Update();
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x0026019C File Offset: 0x0025E39C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_dOnClose != null)
			{
				this.m_dOnClose();
				this.m_dOnClose = null;
			}
		}

		// Token: 0x04005E35 RID: 24117
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE";

		// Token: 0x04005E36 RID: 24118
		public const string UI_ASSET_NAME = "NKM_UI_DIVE_ARTIFACT_EXCHANGE_POPUP";

		// Token: 0x04005E37 RID: 24119
		public EventTrigger m_etBG;

		// Token: 0x04005E38 RID: 24120
		public Text m_lbArtifactCount;

		// Token: 0x04005E39 RID: 24121
		public Text m_lbTotalGetItem;

		// Token: 0x04005E3A RID: 24122
		public Image m_imgTotalGetItemIcon;

		// Token: 0x04005E3B RID: 24123
		public Text m_lbTotalGetItemCount;

		// Token: 0x04005E3C RID: 24124
		public LoopScrollRect m_lsrArtifact;

		// Token: 0x04005E3D RID: 24125
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04005E3E RID: 24126
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005E3F RID: 24127
		private List<int> m_lstArtifact = new List<int>();

		// Token: 0x04005E40 RID: 24128
		private bool m_bFirstOpen = true;

		// Token: 0x04005E41 RID: 24129
		private NKCPopupArtifactExchange.dOnClose m_dOnClose;

		// Token: 0x02001776 RID: 6006
		// (Invoke) Token: 0x0600B35D RID: 45917
		public delegate void dOnClose();
	}
}
