using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Mode;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E0 RID: 2528
	public class NKCUIShadowPalaceSlot : MonoBehaviour
	{
		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06006C92 RID: 27794 RVA: 0x0023788A File Offset: 0x00235A8A
		public int PalaceID
		{
			get
			{
				if (this.m_templet == null)
				{
					return 0;
				}
				return this.m_templet.PALACE_ID;
			}
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x002378A1 File Offset: 0x00235AA1
		public void Init()
		{
			NKCUIComStateButton btn = this.m_btn;
			if (btn != null)
			{
				btn.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btn2 = this.m_btn;
			if (btn2 == null)
			{
				return;
			}
			btn2.PointerClick.AddListener(new UnityAction(this.OnTouch));
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x002378DC File Offset: 0x00235ADC
		public void SetData(NKMShadowPalaceTemplet palaceTemplet, NKMPalaceData palaceData, NKCUIShadowPalaceSlot.OnTouchSlot onTouchSlot)
		{
			this.m_templet = palaceTemplet;
			this.m_data = palaceData;
			this.dOnTouchSlot = onTouchSlot;
			NKCUtil.SetLabelText(this.m_txtNumber, string.Format("#" + palaceTemplet.PALACE_NUM_UI.ToString(), Array.Empty<object>()));
			List<NKMShadowBattleTemplet> battleTemplets = NKMShadowPalaceManager.GetBattleTemplets(palaceTemplet.PALACE_ID);
			if (battleTemplets == null)
			{
				return;
			}
			bool dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(battleTemplets.Last<NKMShadowBattleTemplet>().DUNGEON_ID) != null;
			Sprite sp = null;
			if (dungeonTempletBase)
			{
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_INVEN_ICON_UNIT", palaceTemplet.PALACE_IMG, false);
			}
			NKCUtil.SetImageSprite(this.m_imgBoss, sp, true);
			string msg = "-:--:--";
			if (palaceData != null && palaceData.dungeonDataList.Count == battleTemplets.Count)
			{
				int num = 0;
				for (int i = 0; i < palaceData.dungeonDataList.Count; i++)
				{
					NKMPalaceDungeonData nkmpalaceDungeonData = palaceData.dungeonDataList[i];
					num += nkmpalaceDungeonData.bestTime;
				}
				if (num > 0)
				{
					msg = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)num));
				}
			}
			NKCUtil.SetLabelText(this.m_txtTime, msg);
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x002379D9 File Offset: 0x00235BD9
		public void SetLock(bool bLock)
		{
			NKCUtil.SetGameobjectActive(this.m_objLock, bLock);
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x002379E7 File Offset: 0x00235BE7
		public void SetProgress(bool bCurrent)
		{
			NKCUtil.SetGameobjectActive(this.m_objCurrent, bCurrent);
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x002379F5 File Offset: 0x00235BF5
		public void SetLine(bool bFirst, bool bLast)
		{
			NKCUtil.SetGameobjectActive(this.m_objLeftLine, !bFirst);
			NKCUtil.SetGameobjectActive(this.m_objRightLine, !bLast);
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x00237A18 File Offset: 0x00235C18
		public void PlaySelect(bool bSelect, bool bEffect)
		{
			if (bSelect)
			{
				if (bEffect)
				{
					this.m_aniSelect.Play("NKM_UI_SHADOW_PALACE_SLOT_SELECT_INTRO");
					return;
				}
				this.m_aniSelect.Play("NKM_UI_SHADOW_PALACE_SLOT_SELECT_IDLE");
				return;
			}
			else
			{
				if (bEffect)
				{
					this.m_aniSelect.Play("NKM_UI_SHADOW_PALACE_SLOT_SELECT_OUTRO");
					return;
				}
				this.m_aniSelect.Play("NKM_UI_SHADOW_PALACE_SLOT_BASE");
				return;
			}
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x00237A71 File Offset: 0x00235C71
		public void OnTouch()
		{
			NKCUIShadowPalaceSlot.OnTouchSlot onTouchSlot = this.dOnTouchSlot;
			if (onTouchSlot == null)
			{
				return;
			}
			onTouchSlot(this.m_templet.PALACE_ID);
		}

		// Token: 0x04005851 RID: 22609
		[Header("Info")]
		public Text m_txtNumber;

		// Token: 0x04005852 RID: 22610
		public Text m_txtTime;

		// Token: 0x04005853 RID: 22611
		public Image m_imgBoss;

		// Token: 0x04005854 RID: 22612
		[Header("State")]
		public GameObject m_objLock;

		// Token: 0x04005855 RID: 22613
		public GameObject m_objCurrent;

		// Token: 0x04005856 RID: 22614
		[Header("Animation")]
		public Animator m_aniSelect;

		// Token: 0x04005857 RID: 22615
		[Header("deco")]
		public GameObject m_objLeftLine;

		// Token: 0x04005858 RID: 22616
		public GameObject m_objRightLine;

		// Token: 0x04005859 RID: 22617
		[Header("button")]
		public NKCUIComStateButton m_btn;

		// Token: 0x0400585A RID: 22618
		private NKCUIShadowPalaceSlot.OnTouchSlot dOnTouchSlot;

		// Token: 0x0400585B RID: 22619
		private NKMShadowPalaceTemplet m_templet;

		// Token: 0x0400585C RID: 22620
		private NKMPalaceData m_data;

		// Token: 0x020016EC RID: 5868
		// (Invoke) Token: 0x0600B1B9 RID: 45497
		public delegate void OnTouchSlot(int palaceID);
	}
}
