using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Move
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public MoveTarget AllowedTargets { get; set; }
    public List<Ability> Abilities { get; set; }
    public Animation Animation { get; set; } = new Animation();

    public Move()
    {
        Abilities = new List<Ability>();
    }

    public Move(Move move)
    {
        Id = move.Id;
        Name = move.Name;
        Text = move.Text;
        AllowedTargets = move.AllowedTargets;
        Abilities = move.Abilities.Copy();
        Animation = move.Animation.Copy();
    }
}
