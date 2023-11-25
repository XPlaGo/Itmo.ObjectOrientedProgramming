using System;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.ThroughEnvironment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngine;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
using Itmo.ObjectOrientedProgramming.Lab1.Exceptions.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.ArmorProtectionResult;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.DeflectorProtectionResult;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.EmitterProtectionResult;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.FlightResult;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Result.SpaceshipResult;
using Itmo.ObjectOrientedProgramming.Lab1.Services.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Armor;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Emitters;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Visitors.Spaceship.ThroughEnvironment;

namespace Itmo.ObjectOrientedProgramming.Lab1.Factories;

public class SpaceshipDispatcherFactory
{
    public ISpaceshipThroughEnvironmentVisitor<IEnvironmentVisitor<SpaceshipResultResponse>> CreateThroughEnvironmentDispatcher()
    {
        return new SpaceshipThroughEnvironmentDispatcher<SpaceshipResultResponse>(Fly)
            .ByWalkingSpaceship
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .ByStellaSpaceship
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .ByAugurSpaceship
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .ByMeridianSpaceship
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .ByQuantumSpaceship
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm()
            .ByVaclasSpaceship
                .ThroughNebulaeOfIncreasedDensityOfSpace(Fly)
                .Confirm();
    }

    public ISpaceshipOnPathVisitor<IPathVisitor<SpaceshipResultResponse>> CreateOnPathDispatcher()
    {
        return new SpaceshipOnPathDispatcher<SpaceshipResultResponse>(Fly);
    }

    private static SpaceshipResultResponse FlyTemplate(ISpaceship spaceship, IEnvironment environment, bool preferJumpEngine)
    {
        ArgumentNullException.ThrowIfNull(spaceship);
        ArgumentNullException.ThrowIfNull(environment);

        var armorFactory = new ArmorDispatcherFactory();
        var deflectorFactory = new DeflectorDispatcherFactory();
        var emitterFactory = new EmitterDispatcherFactory();

        IArmorVisitor<IImpedimentVisitor<ArmorProtectionResult>> armorVisitor = armorFactory.CreateDispatcher();
        IDeflectorsVisitor<IImpedimentVisitor<DeflectorProtectionResult>> deflectorVisitor = deflectorFactory.CreateDispatcher();
        IEmitterVisitor<IImpedimentVisitor<EmitterProtectionResult>> emitterVisitor = emitterFactory.CreateDispatcher();

        FlightResultResponse flightResultResponse;

        switch (spaceship)
        {
            case { JumpEngine: not (null or NoneJumpEngine), ImpulseEngine: null or NoneImpulseEngine }:
            {
                flightResultResponse = FlyEngine(spaceship.JumpEngine, spaceship, environment);

                break;
            }

            case { JumpEngine: null or NoneJumpEngine, ImpulseEngine: not (null or NoneImpulseEngine) }:
            {
                flightResultResponse = FlyEngine(spaceship.ImpulseEngine, spaceship, environment);

                break;
            }

            case { JumpEngine: not (null or NoneJumpEngine), ImpulseEngine: not (null or NoneImpulseEngine) }:
                flightResultResponse = FlyEngines(
                        preferJumpEngine ? spaceship.JumpEngine : spaceship.ImpulseEngine,
                        preferJumpEngine ? spaceship.ImpulseEngine : spaceship.JumpEngine,
                        spaceship,
                        environment);

                break;
            default:
                throw new NoEnginesException(preferJumpEngine ? nameof(spaceship.JumpEngine) : nameof(spaceship.ImpulseEngine));
        }

        var spaceshipResultData = new SpaceshipResultData(flightResultResponse.FlightResultData);

        switch (flightResultResponse.FlightResult)
        {
            case FlightResult.None:
                return new SpaceshipResultResponse(SpaceshipResult.None, spaceshipResultData);
            case FlightResult.FuelRanOut:
                return new SpaceshipResultResponse(SpaceshipResult.NotEnoughFuel, spaceshipResultData);
            case FlightResult.PartiallyOvercome:
                return new SpaceshipResultResponse(SpaceshipResult.NotEnoughFuel, spaceshipResultData);
            case FlightResult.EngineNotSuit:
                return new SpaceshipResultResponse(SpaceshipResult.NoSuitableEngines, spaceshipResultData);
        }

        foreach (IImpediment impediment in environment.Impediments)
        {
            if (spaceship.Emitter is not (null or NoneEmitter))
            {
                if (impediment.DamagePoints > 0)
                    impediment.AcceptImpedimentService(spaceship.Emitter.AcceptEmitterVisitor(emitterVisitor));
            }

            if (spaceship.Deflector is not (null or NoneDeflector))
            {
                if (impediment.DamagePoints > 0)
                {
                    impediment.AcceptImpedimentService(spaceship.Deflector.AcceptDeflectorVisitor(deflectorVisitor));
                }
            }

            if (spaceship.Armor is null) continue;
            {
                if (impediment.DamagePoints > 0)
                {
                    ArmorProtectionResult armorResult = impediment.AcceptImpedimentService(spaceship.Armor.AcceptArmorVisitor(armorVisitor));
                    switch (armorResult)
                    {
                        case ArmorProtectionResult.Destroyed:
                            return new SpaceshipResultResponse(SpaceshipResult.Destroyed, spaceshipResultData);
                        case ArmorProtectionResult.LossOfCrew:
                            return new SpaceshipResultResponse(SpaceshipResult.LossOfCrew, spaceshipResultData);
                    }
                }
            }
        }

        return new SpaceshipResultResponse(SpaceshipResult.Overcome, spaceshipResultData);
    }

    private static FlightResultResponse FlyEngine(IEngine engine, ISpaceship spaceship, IEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(engine);
        ArgumentNullException.ThrowIfNull(spaceship);
        ArgumentNullException.ThrowIfNull(environment);

        var engineFactory = new EngineDispatcherFactory();
        IEngineVisitor<IEnvironmentVisitor<FlightResultResponse>> engineVisitor = engineFactory.CreateDispatcher();

        FlightResultResponse flightResult = environment.AcceptEnvironmentVisitor(engine.AcceptEngineVisitor(engineVisitor));

        if (flightResult.FlightResult is FlightResult.Overcome or FlightResult.PartiallyOvercome)
        {
            if (flightResult.FlightResultData is null)
                throw new FlightDataCannotBeNull(nameof(flightResult.FlightResultData));

            spaceship.UpdateVelocity(flightResult.FlightResultData.CurrentVelocity);
            environment.DecreaseLength(flightResult.FlightResultData.Length);
        }

        return new FlightResultResponse(flightResult.FlightResult, flightResult.FlightResultData);
    }

    private static FlightResultResponse FlyEngines(IEngine firstEngine, IEngine secondEngine, ISpaceship spaceship, IEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(firstEngine);
        ArgumentNullException.ThrowIfNull(secondEngine);
        ArgumentNullException.ThrowIfNull(spaceship);
        ArgumentNullException.ThrowIfNull(environment);

        var engineFactory = new EngineDispatcherFactory();
        IEngineVisitor<IEnvironmentVisitor<FlightResultResponse>> engineVisitor = engineFactory.CreateDispatcher();

        FlightResultResponse firstFlightResponse = environment.AcceptEnvironmentVisitor(firstEngine.AcceptEngineVisitor(engineVisitor));

        FlightResult flightResult = firstFlightResponse.FlightResult;
        FlightResultData flightResultData = firstFlightResponse.FlightResultData;

        environment.DecreaseLength(flightResultData.Length);
        spaceship.UpdateVelocity(flightResultData.CurrentVelocity);

        switch (firstFlightResponse.FlightResult)
        {
            case FlightResult.None:
                FlightResultResponse secondFlightResponseN = FlyEngine(secondEngine, spaceship, environment);

                flightResultData = secondFlightResponseN.FlightResultData;
                flightResult = secondFlightResponseN.FlightResult;

                break;

            case FlightResult.FuelRanOut:
                FlightResultResponse secondFlightResponseFro = FlyEngine(secondEngine, spaceship, environment);

                flightResultData = secondFlightResponseFro.FlightResultData;

                flightResult = secondFlightResponseFro.FlightResult switch
                {
                    FlightResult.EngineNotSuit => FlightResult.FuelRanOut,
                    _ => secondFlightResponseFro.FlightResult,
                };

                break;

            case FlightResult.EngineNotSuit:
                FlightResultResponse secondFlightResponseEns = FlyEngine(secondEngine, spaceship, environment);

                flightResultData = secondFlightResponseEns.FlightResultData;
                flightResult = secondFlightResponseEns.FlightResult;

                break;

            case FlightResult.PartiallyOvercome:
            {
                if (firstFlightResponse.FlightResultData is null)
                    throw new FlightDataCannotBeNull(nameof(firstFlightResponse.FlightResultData));

                FlightResultResponse secondEngineResponsePo = FlyEngine(secondEngine, spaceship, environment);

                flightResult = firstFlightResponse.FlightResult;

                switch (firstFlightResponse.FlightResult)
                {
                    case FlightResult.EngineNotSuit:
                        flightResult = FlightResult.PartiallyOvercome;
                        break;
                    case FlightResult.FuelRanOut:
                        flightResult = FlightResult.PartiallyOvercome;
                        break;
                }

                environment.DecreaseLength(secondEngineResponsePo.FlightResultData.Length);
                spaceship.UpdateVelocity(secondEngineResponsePo.FlightResultData.CurrentVelocity);

                flightResultData = new FlightResultData(
                    secondEngineResponsePo.FlightResultData.Time + firstFlightResponse.FlightResultData.Time,
                    secondEngineResponsePo.FlightResultData.CurrentVelocity,
                    secondEngineResponsePo.FlightResultData.Length + firstFlightResponse.FlightResultData.Length,
                    secondEngineResponsePo.FlightResultData.Fuel + firstFlightResponse.FlightResultData.Fuel);

                break;
            }

            case FlightResult.Overcome:
            {
                if (firstFlightResponse.FlightResultData is null)
                    throw new FlightDataCannotBeNull(nameof(firstFlightResponse.FlightResultData));

                break;
            }
        }

        return new FlightResultResponse(flightResult, flightResultData);
    }

    private SpaceshipResultResponse Fly(ISpaceship spaceship, IEnvironment environment)
    {
        return FlyTemplate(spaceship, environment, false);
    }

    private SpaceshipResultResponse Fly(ISpaceship spaceship, NebulaeOfIncreasedDensityOfSpace environment)
    {
        return FlyTemplate(spaceship, environment, true);
    }

    private SpaceshipResultResponse Fly(ISpaceship spaceship, IPath path)
    {
        ISpaceshipThroughEnvironmentVisitor<IEnvironmentVisitor<SpaceshipResultResponse>> throughEnvironmentDispatcher = CreateThroughEnvironmentDispatcher();

        var data = new SpaceshipResultData(0, 0, 0, 0);

        foreach (IEnvironment environment in path.Environments)
        {
            SpaceshipResultResponse response =
                environment.AcceptEnvironmentVisitor(spaceship.AcceptSpaceshipVisitor(throughEnvironmentDispatcher));

            data.Time += response.SpaceshipResultData.Time;
            data.CurrentVelocity = response.SpaceshipResultData.CurrentVelocity;
            data.Length += response.SpaceshipResultData.Length;
            data.Fuel += response.SpaceshipResultData.Fuel;

            if (response.SpaceshipResult != SpaceshipResult.Overcome)
            {
                return new SpaceshipResultResponse(response.SpaceshipResult, data);
            }
        }

        return new SpaceshipResultResponse(SpaceshipResult.Overcome, data);
    }
}