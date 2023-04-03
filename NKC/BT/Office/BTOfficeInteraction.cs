using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using NKC.Office;
using NKC.Templet.Office;

namespace NKC.BT.Office
{
	// Token: 0x0200081B RID: 2075
	public class BTOfficeInteraction : BTOfficeActionBase
	{
		// Token: 0x060051F5 RID: 20981 RVA: 0x0018E380 File Offset: 0x0018C580
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			if (this.m_Character.CurrentFurnitureInteractionTemplet != null && this.m_Character.CurrentInteractionTargetFurniture != null)
			{
				NKCOfficeFurnitureInteractionTemplet currentFurnitureInteractionTemplet = this.m_Character.CurrentFurnitureInteractionTemplet;
				List<NKCAnimationEventTemplet> list = NKCAnimationEventManager.Find(currentFurnitureInteractionTemplet.UnitAni);
				if (list == null)
				{
					this.bActionSuccessFlag = false;
					return;
				}
				this.m_Character.SetPlayingInteractionAnimation(true);
				if (this.m_Character.CurrentFurnitureInteractionTemplet.eActType == NKCOfficeFurnitureInteractionTemplet.ActType.Reaction || this.m_Character.CurrentInteractionTargetFurniture.GetInteractionPoint() == null)
				{
					bool bLeft = this.m_Character.transform.position.x >= this.m_Character.CurrentInteractionTargetFurniture.transform.position.x;
					this.m_Character.EnqueueAnimation(base.GetInvertDirectionInstance(bLeft));
				}
				else
				{
					this.m_Character.EnqueueAnimation(base.GetInvertDirectionInstance(this.m_Character.CurrentInteractionTargetFurniture.GetInvert()));
				}
				this.m_Character.EnqueueAnimation(list);
				this.m_Character.CurrentInteractionTargetFurniture.PlayAnimationEvent(currentFurnitureInteractionTemplet.InteriorAni);
				this.bActionSuccessFlag = true;
				return;
			}
			else
			{
				if (this.m_Character.CurrentUnitInteractionTemplet == null)
				{
					this.m_Character.UnregisterInteraction();
					this.bActionSuccessFlag = false;
					return;
				}
				if (this.m_Character.CurrentUnitInteractionTemplet.IsSoloAction)
				{
					List<NKCAnimationEventTemplet> list2 = NKCAnimationEventManager.Find(this.m_Character.CurrentUnitInteractionTemplet.ActorAni);
					if (list2 == null)
					{
						this.bActionSuccessFlag = false;
						return;
					}
					this.m_Character.SetPlayingInteractionAnimation(true);
					this.m_Character.EnqueueAnimation(list2);
					this.bActionSuccessFlag = true;
					return;
				}
				else
				{
					NKCOfficeCharacter currentUnitInteractionTarget = this.m_Character.CurrentUnitInteractionTarget;
					NKCOfficeUnitInteractionTemplet currentUnitInteractionTemplet = this.m_Character.CurrentUnitInteractionTemplet;
					if (currentUnitInteractionTemplet == null || currentUnitInteractionTarget == null)
					{
						this.bActionSuccessFlag = false;
						return;
					}
					List<NKCAnimationEventTemplet> list3 = NKCAnimationEventManager.Find(this.m_Character.CurrentUnitInteractionIsMainActor ? currentUnitInteractionTemplet.ActorAni : currentUnitInteractionTemplet.TargetAni);
					if (list3 == null)
					{
						this.bActionSuccessFlag = false;
						return;
					}
					this.m_Character.SetPlayingInteractionAnimation(true);
					bool bLeft2 = this.m_Character.transform.position.x >= currentUnitInteractionTarget.transform.position.x;
					this.m_Character.EnqueueAnimation(base.GetInvertDirectionInstance(bLeft2));
					this.m_Character.EnqueueAnimation(list3);
					this.bActionSuccessFlag = true;
					return;
				}
			}
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0018E5FB File Offset: 0x0018C7FB
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0018E617 File Offset: 0x0018C817
		public override void OnEnd()
		{
			this.m_Character.SetPlayingInteractionAnimation(false);
		}
	}
}
