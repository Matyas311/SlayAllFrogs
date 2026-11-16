using System;
using System.Collections.Generic;

[Serializable]
public class WaveTier
{
    public string name;
    public int fromWave;
    public int toWave;
    public List<WaveEntry> entries;
}