using System;
using NKM;
using NKM.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD7 RID: 3031
	public class NKCUIEventSlot : MonoBehaviour
	{
		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06008C93 RID: 35987 RVA: 0x002FD1B6 File Offset: 0x002FB3B6
		public int EventID
		{
			get
			{
				return this.m_EventID;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06008C94 RID: 35988 RVA: 0x002FD1BE File Offset: 0x002FB3BE
		public NKM_EVENT_TYPE EventType
		{
			get
			{
				return this.m_EventType;
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06008C96 RID: 35990 RVA: 0x002FD1CF File Offset: 0x002FB3CF
		// (set) Token: 0x06008C95 RID: 35989 RVA: 0x002FD1C6 File Offset: 0x002FB3C6
		public NKMEventTabTemplet EventTabTemplet { get; private set; }

		// Token: 0x06008C97 RID: 35991 RVA: 0x002FD1D8 File Offset: 0x002FB3D8
		public bool SetData(NKMEventTabTemplet tabTemplet, NKCUIComToggleGroup tglGroup, bool bSelected, NKCUIEventSlot.OnSelect onSelect)
		{
			if (tabTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return false;
			}
			this.EventTabTemplet = tabTemplet;
			this.m_EventID = tabTemplet.m_EventID;
			this.m_EventType = tabTemplet.m_EventType;
			this.m_tgl.SetToggleGroup(tglGroup);
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClick));
			this.SetToggle(bSelected, true, true);
			this.dOnSelect = onSelect;
			NKCUtil.SetImageSprite(this.m_imgBg, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("ab_ui_nkm_ui_event_texture", tabTemplet.m_EventTabImage)), false);
			NKCUtil.SetLabelText(this.m_lbTitle, tabTemplet.GetTitle());
			if (tabTemplet.HasTimeLimit)
			{
				this.m_bShowRemainTime = true;
				this.m_EndTick = tabTemplet.TimeLimit.Ticks;
				this.SetRemainTime(this.m_EndTick);
			}
			else
			{
				this.m_bShowRemainTime = false;
				this.m_EndTick = 0L;
			}
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, this.m_bShowRemainTime);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			return true;
		}

		// Token: 0x06008C98 RID: 35992 RVA: 0x002FD2FC File Offset: 0x002FB4FC
		private void SetRemainTime(long endTick)
		{
			if (NKCSynchronizedTime.GetTimeLeft(this.EventTabTemplet.EventDateEndUtc).TotalDays > (double)NKCSynchronizedTime.UNLIMITD_REMAIN_DAYS)
			{
				NKCUtil.SetLabelText(this.m_lbRemainTime, NKCUtilString.GET_STRING_EVENT_DATE_UNLIMITED_TEXT);
				return;
			}
			string @string = NKCStringTable.GetString("SI_DP_TIMER_REMAIN", new object[]
			{
				NKCUtilString.GetRemainTimeString(new DateTime(endTick), 1)
			});
			NKCUtil.SetLabelText(this.m_lbRemainTime, @string);
		}

		// Token: 0x06008C99 RID: 35993 RVA: 0x002FD366 File Offset: 0x002FB566
		public void CheckRedDot()
		{
			if (this.EventTabTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, NKMEventManager.CheckRedDot(this.EventTabTemplet));
			}
		}

		// Token: 0x06008C9A RID: 35994 RVA: 0x002FD386 File Offset: 0x002FB586
		public void SetToggle(bool bSelect, bool bForce, bool bImmediate)
		{
			this.m_tgl.Select(bSelect, bForce, bImmediate);
			if (bSelect)
			{
				NKCUtil.SetLabelTextColor(this.m_lbTitle, this.m_colSelectedText);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbTitle, Color.white);
		}

		// Token: 0x06008C9B RID: 35995 RVA: 0x002FD3BC File Offset: 0x002FB5BC
		public void OnClick(bool bSelect)
		{
			if (bSelect)
			{
				NKCUIEventSlot.OnSelect onSelect = this.dOnSelect;
				if (onSelect == null)
				{
					return;
				}
				onSelect(this.EventTabTemplet);
			}
		}

		// Token: 0x06008C9C RID: 35996 RVA: 0x002FD3D8 File Offset: 0x002FB5D8
		private void Update()
		{
			if (!this.m_bShowRemainTime)
			{
				return;
			}
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				this.SetRemainTime(this.m_EndTick);
			}
		}

		// Token: 0x0400797E RID: 31102
		public Image m_imgBg;

		// Token: 0x0400797F RID: 31103
		public Text m_lbTitle;

		// Token: 0x04007980 RID: 31104
		public GameObject m_objRemainTime;

		// Token: 0x04007981 RID: 31105
		public Text m_lbRemainTime;

		// Token: 0x04007982 RID: 31106
		public GameObject m_objRedDot;

		// Token: 0x04007983 RID: 31107
		public NKCUIComToggle m_tgl;

		// Token: 0x04007984 RID: 31108
		private int m_EventID;

		// Token: 0x04007985 RID: 31109
		private NKM_EVENT_TYPE m_EventType;

		// Token: 0x04007986 RID: 31110
		private bool m_bShowRemainTime;

		// Token: 0x04007987 RID: 31111
		private long m_EndTick;

		// Token: 0x04007988 RID: 31112
		private float m_fDeltaTime;

		// Token: 0x04007989 RID: 31113
		public Color m_colSelectedText = new Color(0.003921569f, 0.105882354f, 0.23137255f);

		// Token: 0x0400798B RID: 31115
		private NKCUIEventSlot.OnSelect dOnSelect;

		// Token: 0x020019B3 RID: 6579
		// (Invoke) Token: 0x0600B9CE RID: 47566
		public delegate void OnSelect(NKMEventTabTemplet tabTemplet);
	}
}
