using System;
using System.Collections.Generic;

[Serializable]
public struct StackData
{
    public BlockData[] blocks;
}

[Serializable]
public struct BlockData
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}