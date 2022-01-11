using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Animation
{
    public Animation()
    {

    }

    public string MoveAnimationName { get; set; } = "move";
    public string PerformAnimationName { get; set; } = "attack";
    public MovementType MovementType { get; set; } = MovementType.TowardsTargetEnemy;
    public float TimeToUpdate { get; set; } = .5f;
}
