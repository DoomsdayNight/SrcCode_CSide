using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DB RID: 219
	[TaskDescription("Allows multiple conditional tasks to be added to a single node.")]
	[TaskIcon("{SkinColor}StackedConditionalIcon.png")]
	public class StackedConditional : Conditional
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x00019338 File Offset: 0x00017538
		public override void OnAwake()
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].GameObject = this.gameObject;
					this.conditionals[i].Transform = this.transform;
					this.conditionals[i].Owner = base.Owner;
					this.conditionals[i].OnAwake();
				}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000193B4 File Offset: 0x000175B4
		public override void OnStart()
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnStart();
				}
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000193F4 File Offset: 0x000175F4
		public override TaskStatus OnUpdate()
		{
			if (this.conditionals == null)
			{
				return TaskStatus.Failure;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					TaskStatus taskStatus = this.conditionals[i].OnUpdate();
					if (this.comparisonType == StackedConditional.ComparisonType.Sequence && taskStatus == TaskStatus.Failure)
					{
						return TaskStatus.Failure;
					}
					if (this.comparisonType == StackedConditional.ComparisonType.Selector && taskStatus == TaskStatus.Success)
					{
						return TaskStatus.Success;
					}
				}
			}
			if (this.comparisonType != StackedConditional.ComparisonType.Sequence)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00019460 File Offset: 0x00017660
		public override void OnFixedUpdate()
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnFixedUpdate();
				}
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000194A0 File Offset: 0x000176A0
		public override void OnLateUpdate()
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnLateUpdate();
				}
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000194E0 File Offset: 0x000176E0
		public override void OnEnd()
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnEnd();
				}
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00019520 File Offset: 0x00017720
		public override void OnTriggerEnter(Collider other)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnTriggerEnter(other);
				}
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00019564 File Offset: 0x00017764
		public override void OnTriggerEnter2D(Collider2D other)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnTriggerEnter2D(other);
				}
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000195A8 File Offset: 0x000177A8
		public override void OnTriggerExit(Collider other)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnTriggerExit(other);
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000195EC File Offset: 0x000177EC
		public override void OnTriggerExit2D(Collider2D other)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnTriggerExit2D(other);
				}
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00019630 File Offset: 0x00017830
		public override void OnCollisionEnter(Collision collision)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnCollisionEnter(collision);
				}
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00019674 File Offset: 0x00017874
		public override void OnCollisionEnter2D(Collision2D collision)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnCollisionEnter2D(collision);
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000196B8 File Offset: 0x000178B8
		public override void OnCollisionExit(Collision collision)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnCollisionExit(collision);
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000196FC File Offset: 0x000178FC
		public override void OnCollisionExit2D(Collision2D collision)
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnCollisionExit2D(collision);
				}
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00019740 File Offset: 0x00017940
		public override string OnDrawNodeText()
		{
			if (this.conditionals == null || !this.graphLabel)
			{
				return string.Empty;
			}
			string text = string.Empty;
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += "\n";
					}
					text += this.conditionals[i].GetType().Name;
				}
			}
			return text;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000197B4 File Offset: 0x000179B4
		public override void OnReset()
		{
			if (this.conditionals == null)
			{
				return;
			}
			for (int i = 0; i < this.conditionals.Length; i++)
			{
				if (this.conditionals[i] != null)
				{
					this.conditionals[i].OnReset();
				}
			}
		}

		// Token: 0x04000352 RID: 850
		[InspectTask]
		public Conditional[] conditionals;

		// Token: 0x04000353 RID: 851
		[Tooltip("Specifies if the tasks should be traversed with an AND (Sequence) or an OR (Selector).")]
		public StackedConditional.ComparisonType comparisonType;

		// Token: 0x04000354 RID: 852
		[Tooltip("Should the tasks be labeled within the graph?")]
		public bool graphLabel;

		// Token: 0x0200110E RID: 4366
		public enum ComparisonType
		{
			// Token: 0x04009155 RID: 37205
			Sequence,
			// Token: 0x04009156 RID: 37206
			Selector
		}
	}
}
