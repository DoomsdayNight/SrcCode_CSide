using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C1 RID: 193
	[TaskDescription("Allows multiple action tasks to be added to a single node.")]
	[TaskIcon("{SkinColor}StackedActionIcon.png")]
	public class StackedAction : Action
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x000174DC File Offset: 0x000156DC
		public override void OnAwake()
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].GameObject = this.gameObject;
					this.actions[i].Transform = this.transform;
					this.actions[i].Owner = base.Owner;
					this.actions[i].OnAwake();
				}
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00017558 File Offset: 0x00015758
		public override void OnStart()
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnStart();
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00017598 File Offset: 0x00015798
		public override TaskStatus OnUpdate()
		{
			if (this.actions == null)
			{
				return TaskStatus.Failure;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					TaskStatus taskStatus = this.actions[i].OnUpdate();
					if (this.comparisonType == StackedAction.ComparisonType.Sequence && taskStatus == TaskStatus.Failure)
					{
						return TaskStatus.Failure;
					}
					if (this.comparisonType == StackedAction.ComparisonType.Selector && taskStatus == TaskStatus.Success)
					{
						return TaskStatus.Success;
					}
				}
			}
			if (this.comparisonType != StackedAction.ComparisonType.Sequence)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00017604 File Offset: 0x00015804
		public override void OnFixedUpdate()
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnFixedUpdate();
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00017644 File Offset: 0x00015844
		public override void OnLateUpdate()
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnLateUpdate();
				}
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00017684 File Offset: 0x00015884
		public override void OnEnd()
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnEnd();
				}
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000176C4 File Offset: 0x000158C4
		public override void OnTriggerEnter(Collider other)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnTriggerEnter(other);
				}
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00017708 File Offset: 0x00015908
		public override void OnTriggerEnter2D(Collider2D other)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnTriggerEnter2D(other);
				}
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001774C File Offset: 0x0001594C
		public override void OnTriggerExit(Collider other)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnTriggerExit(other);
				}
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00017790 File Offset: 0x00015990
		public override void OnTriggerExit2D(Collider2D other)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnTriggerExit2D(other);
				}
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000177D4 File Offset: 0x000159D4
		public override void OnCollisionEnter(Collision collision)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnCollisionEnter(collision);
				}
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00017818 File Offset: 0x00015A18
		public override void OnCollisionEnter2D(Collision2D collision)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnCollisionEnter2D(collision);
				}
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001785C File Offset: 0x00015A5C
		public override void OnCollisionExit(Collision collision)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnCollisionExit(collision);
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000178A0 File Offset: 0x00015AA0
		public override void OnCollisionExit2D(Collision2D collision)
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnCollisionExit2D(collision);
				}
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000178E4 File Offset: 0x00015AE4
		public override string OnDrawNodeText()
		{
			if (this.actions == null || !this.graphLabel)
			{
				return string.Empty;
			}
			string text = string.Empty;
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += "\n";
					}
					text += this.actions[i].GetType().Name;
				}
			}
			return text;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00017958 File Offset: 0x00015B58
		public override void OnReset()
		{
			if (this.actions == null)
			{
				return;
			}
			for (int i = 0; i < this.actions.Length; i++)
			{
				if (this.actions[i] != null)
				{
					this.actions[i].OnReset();
				}
			}
		}

		// Token: 0x040002F5 RID: 757
		[InspectTask]
		public Action[] actions;

		// Token: 0x040002F6 RID: 758
		[Tooltip("Specifies if the tasks should be traversed with an AND (Sequence) or an OR (Selector).")]
		public StackedAction.ComparisonType comparisonType;

		// Token: 0x040002F7 RID: 759
		[Tooltip("Should the tasks be labeled within the graph?")]
		public bool graphLabel;

		// Token: 0x0200110D RID: 4365
		public enum ComparisonType
		{
			// Token: 0x04009152 RID: 37202
			Sequence,
			// Token: 0x04009153 RID: 37203
			Selector
		}
	}
}
