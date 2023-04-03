using System;

namespace NKC
{
	// Token: 0x020006BA RID: 1722
	public class NKCPowerSaveMode
	{
		// Token: 0x06003A6A RID: 14954 RVA: 0x0012CF4E File Offset: 0x0012B14E
		public void SetEnable(bool bEnable)
		{
			this.m_bEnable = bEnable;
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x0012CF57 File Offset: 0x0012B157
		public bool GetEnable()
		{
			return this.m_bEnable;
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x0012CF5F File Offset: 0x0012B15F
		public void SetJukeBoxMode(bool bEnable)
		{
			this.m_bJukeBoxMode = bEnable;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x0012CF68 File Offset: 0x0012B168
		public bool IsJukeBoxMode
		{
			get
			{
				return this.m_bJukeBoxMode;
			}
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x0012CF70 File Offset: 0x0012B170
		public void SetLastKeyInputTime(float fInputTime)
		{
			this.m_fLastKeyInputTime = fInputTime;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x0012CF79 File Offset: 0x0012B179
		public void SetTurnOnEvent(NKCPowerSaveMode.DoAtEvent dgDoAtEvent)
		{
			this.m_dgPowerSaveMode = dgDoAtEvent;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x0012CF82 File Offset: 0x0012B182
		public void SetTurnOffEvent(NKCPowerSaveMode.DoAtEvent dgDoAtEvent)
		{
			this.m_dgTurnOffPowerSaveMode = dgDoAtEvent;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x0012CF8B File Offset: 0x0012B18B
		public void SetMaxModeEvent(NKCPowerSaveMode.DoAtEvent dgDoAtEvent)
		{
			this.m_dgMaxPowerSaveMode = dgDoAtEvent;
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x0012CF94 File Offset: 0x0012B194
		public void SetMaxModeNextFrameEvent(NKCPowerSaveMode.DoAtEvent dgDoAtEvent)
		{
			this.m_dgMaxPowerSaveModeNextFrame = dgDoAtEvent;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x0012CFA0 File Offset: 0x0012B1A0
		public void Update(float fTime)
		{
			if (this.GetEnable())
			{
				if (this.m_fLastKeyInputTime + this.TIME_UNTIL_MAX_POWER_SAVE_MODE < fTime)
				{
					this.m_bPowerSaveModeCallback = false;
					this.m_bTurnOffPowerSaveModeCallback = false;
					if (!this.m_bMaxPowerSaveModeCallback)
					{
						this.m_bMaxPowerSaveModeCallback = true;
						if (this.m_dgMaxPowerSaveMode != null)
						{
							this.m_dgMaxPowerSaveMode();
							return;
						}
					}
					else if (!this.m_bMaxPowerSaveModeNextFrameCallback)
					{
						this.m_bMaxPowerSaveModeNextFrameCallback = true;
						if (this.m_dgMaxPowerSaveModeNextFrame != null)
						{
							this.m_dgMaxPowerSaveModeNextFrame();
							return;
						}
					}
				}
				else
				{
					this.m_bMaxPowerSaveModeCallback = false;
					this.m_bMaxPowerSaveModeNextFrameCallback = false;
					this.m_bTurnOffPowerSaveModeCallback = false;
					if (!this.m_bPowerSaveModeCallback)
					{
						this.m_bPowerSaveModeCallback = true;
						if (this.m_dgPowerSaveMode != null)
						{
							this.m_dgPowerSaveMode();
							return;
						}
					}
				}
			}
			else
			{
				this.m_bMaxPowerSaveModeNextFrameCallback = false;
				this.m_bPowerSaveModeCallback = false;
				if (!this.m_bTurnOffPowerSaveModeCallback)
				{
					this.m_bTurnOffPowerSaveModeCallback = true;
					if (this.m_dgTurnOffPowerSaveMode != null)
					{
						this.m_dgTurnOffPowerSaveMode();
					}
				}
			}
		}

		// Token: 0x04003511 RID: 13585
		private float TIME_UNTIL_MAX_POWER_SAVE_MODE = 5f;

		// Token: 0x04003512 RID: 13586
		private bool m_bEnable;

		// Token: 0x04003513 RID: 13587
		private bool m_bJukeBoxMode;

		// Token: 0x04003514 RID: 13588
		private float m_fLastKeyInputTime;

		// Token: 0x04003515 RID: 13589
		private bool m_bPowerSaveModeCallback;

		// Token: 0x04003516 RID: 13590
		private bool m_bMaxPowerSaveModeCallback;

		// Token: 0x04003517 RID: 13591
		private bool m_bMaxPowerSaveModeNextFrameCallback;

		// Token: 0x04003518 RID: 13592
		private bool m_bTurnOffPowerSaveModeCallback = true;

		// Token: 0x04003519 RID: 13593
		private NKCPowerSaveMode.DoAtEvent m_dgPowerSaveMode;

		// Token: 0x0400351A RID: 13594
		private NKCPowerSaveMode.DoAtEvent m_dgMaxPowerSaveMode;

		// Token: 0x0400351B RID: 13595
		private NKCPowerSaveMode.DoAtEvent m_dgMaxPowerSaveModeNextFrame;

		// Token: 0x0400351C RID: 13596
		private NKCPowerSaveMode.DoAtEvent m_dgTurnOffPowerSaveMode;

		// Token: 0x02001385 RID: 4997
		// (Invoke) Token: 0x0600A60F RID: 42511
		public delegate void DoAtEvent();
	}
}
