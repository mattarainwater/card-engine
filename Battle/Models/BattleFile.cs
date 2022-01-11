using System;
using System.Collections.Generic;

public class BattleFile
{
    public BattleFile()
    {

    }

    public string BackgroundTexture { get; set; }
    public List<int> Actors { get; set; }
    public string VictoryStoryPath { get; set; }
    public string DefeatStoryPath { get; set; }
}