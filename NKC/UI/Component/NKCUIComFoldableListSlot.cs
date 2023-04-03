using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Component
{
	// Token: 0x02000C58 RID: 3160
	public class NKCUIComFoldableListSlot : MonoBehaviour
	{
		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06009346 RID: 37702 RVA: 0x00324491 File Offset: 0x00322691
		// (set) Token: 0x06009347 RID: 37703 RVA: 0x00324499 File Offset: 0x00322699
		public int MajorKey { get; private set; }

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06009348 RID: 37704 RVA: 0x003244A2 File Offset: 0x003226A2
		// (set) Token: 0x06009349 RID: 37705 RVA: 0x003244AA File Offset: 0x003226AA
		public int MinorKey { get; private set; }

		// Token: 0x0600934A RID: 37706 RVA: 0x003244B4 File Offset: 0x003226B4
		public void SetData(NKCUIComFoldableList.Element element, NKCUIComFoldableListSlot.OnSelectSlot onSelectSlot)
		{
			this.MajorKey = element.MajorKey;
			this.MinorKey = element.MinorKey;
			this.isMajor = element.isMajor;
			if (this.m_Toggle != null)
			{
				NKCUtil.SetToggleValueChangedDelegate(this.m_Toggle, new UnityAction<bool>(this.OnToggle));
			}
			this.SetData(element);
			this.dOnSelectSlot = onSelectSlot;
		}

		// Token: 0x0600934B RID: 37707 RVA: 0x00324518 File Offset: 0x00322718
		protected virtual void SetData(NKCUIComFoldableList.Element element)
		{
		}

		// Token: 0x0600934C RID: 37708 RVA: 0x0032451A File Offset: 0x0032271A
		public void SetToggleGroup(NKCUIComToggleGroup tglGroup)
		{
			NKCUIComToggle toggle = this.m_Toggle;
			if (toggle == null)
			{
				return;
			}
			toggle.SetToggleGroup(tglGroup);
		}

		// Token: 0x0600934D RID: 37709 RVA: 0x0032452D File Offset: 0x0032272D
		public void Select(bool bSelect, bool bForce = false)
		{
			this.m_Toggle.Select(bSelect, bForce, false);
		}

		// Token: 0x0600934E RID: 37710 RVA: 0x0032453E File Offset: 0x0032273E
		private void OnToggle(bool value)
		{
			NKCUIComFoldableListSlot.OnSelectSlot onSelectSlot = this.dOnSelectSlot;
			if (onSelectSlot == null)
			{
				return;
			}
			onSelectSlot(this.MajorKey, this.MinorKey, value, this.isMajor);
		}

		// Token: 0x0600934F RID: 37711 RVA: 0x00324563 File Offset: 0x00322763
		public void ClearChild()
		{
			List<NKCUIComFoldableListSlot> lstChild = this.m_lstChild;
			if (lstChild != null)
			{
				lstChild.Clear();
			}
			this.m_lstChild = null;
		}

		// Token: 0x06009350 RID: 37712 RVA: 0x0032457D File Offset: 0x0032277D
		public void AddChild(NKCUIComFoldableListSlot child)
		{
			if (this.m_lstChild == null)
			{
				this.m_lstChild = new List<NKCUIComFoldableListSlot>();
			}
			this.m_lstChild.Add(child);
		}

		// Token: 0x06009351 RID: 37713 RVA: 0x003245A0 File Offset: 0x003227A0
		public void ActivateChild(bool value)
		{
			if (this.m_lstChild != null)
			{
				foreach (NKCUIComFoldableListSlot targetMono in this.m_lstChild)
				{
					NKCUtil.SetGameobjectActive(targetMono, value);
				}
			}
		}

		// Token: 0x04008036 RID: 32822
		public NKCUIComToggle m_Toggle;

		// Token: 0x04008039 RID: 32825
		private bool isMajor;

		// Token: 0x0400803A RID: 32826
		private NKCUIComFoldableListSlot.OnSelectSlot dOnSelectSlot;

		// Token: 0x0400803B RID: 32827
		private List<NKCUIComFoldableListSlot> m_lstChild;

		// Token: 0x02001A1A RID: 6682
		// (Invoke) Token: 0x0600BB12 RID: 47890
		public delegate void OnSelectSlot(int majorKey, int minorKey, bool bToggleValue, bool isMajor);
	}
}
