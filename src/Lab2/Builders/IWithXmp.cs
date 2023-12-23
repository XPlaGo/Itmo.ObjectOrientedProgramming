﻿using Itmo.ObjectOrientedProgramming.Lab2.Documents;

namespace Itmo.ObjectOrientedProgramming.Lab2.Builders;

public interface IWithXmp
{
    public IWithVideoCard WithXmp(XmpDocument xmpDocument);
}