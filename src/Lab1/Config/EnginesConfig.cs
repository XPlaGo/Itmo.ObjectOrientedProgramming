namespace Itmo.ObjectOrientedProgramming.Lab1.Config;

public static class EnginesConfig
{
    public static double ImpulseEngineFuelMaxLevel => 20;
    public static double JumpEngineFuelMaxLevel => 50;
    public static double CImpulseEngineFuelConsumption => 1;
    public static double EImpulseEngineFuelConsumption => 1.1;
    public static double EImpulseEngineEpsilon => 0.001;
    public static double EImpulseEngineUpperbound => 700;
    public static double EImpulseEngineMaxIterations => 1000;
    public static double AlphaJumpEngineFuelConsumption => 1;
    public static double GammaJumpEngineFuelConsumption => 1;
    public static double OmegaJumpEngineFuelConsumption => 1;
    public static double OmegaJumpEngineEpsilon => 0.001;
    public static double OmegaJumpEngineUpperbound => 1e6;
    public static double OmegaJumpEngineMaxIterations => 100;
}