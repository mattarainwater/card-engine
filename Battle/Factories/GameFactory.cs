using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GameFactory
{
    public static Container Create()
    {
        Container game = new Container();

        game.AddAspect<ActionSystem>();
        game.AddAspect<DataSystem>();
        game.AddAspect<TurnSystem>();
        game.AddAspect<MoveSystem>();
        game.AddAspect<DamageSystem>();
        game.AddAspect<HealingSystem>();
        game.AddAspect<TargetSystem>();
        game.AddAspect<GuardSystem>();
        game.AddAspect<ActorSystem>();
        game.AddAspect<BleedSystem>();
        game.AddAspect<VictorySystem>();

        game.AddAspect<AISystem>();

        game.AddAspect<DeckbuilderSystem>();
        game.AddAspect<CardSystem>();
        game.AddAspect<DrawCardsSystem>();
        game.AddAspect<ShuffleSystem>();
        game.AddAspect<PlayCardSystem>();
        game.AddAspect<ImproviseSystem>();

        game.AddAspect<PairerSystem>();

        game.AddAspect<ProgrammedSystem>();

        game.AddAspect<StatusSystem>();

        return game;
    }
}
