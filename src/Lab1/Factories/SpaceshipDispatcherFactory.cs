using System;
using System.Data;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.OnPath;
using Itmo.ObjectOrientedProgramming.Lab1.Dispatchers.Spaceships.ThroughEnvironment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Impediment;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Path;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Spaceships;
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
        SpaceshipResultData? spaceshipResultData = null;

        switch (spaceship)
        {
            case { JumpEngine: not null, ImpulseEngine: null }:
            {
                flightResultResponse = FlyEngine(spaceship.JumpEngine, spaceship, environment);

                break;
            }

            case { JumpEngine: null, ImpulseEngine: not null }:
            {
                flightResultResponse = FlyEngine(spaceship.ImpulseEngine, spaceship, environment);

                break;
            }

            case { JumpEngine: not null, ImpulseEngine: not null }:
                flightResultResponse = FlyEngines(
                        preferJumpEngine ? spaceship.JumpEngine : spaceship.ImpulseEngine,
                        preferJumpEngine ? spaceship.ImpulseEngine : spaceship.JumpEngine,
                        spaceship,
                        environment);

                break;
            default:
                throw new NoNullAllowedException(preferJumpEngine ? nameof(spaceship.JumpEngine) : nameof(spaceship.ImpulseEngine));
        }

        if (flightResultResponse.FlightResultData != null)
            spaceshipResultData = new SpaceshipResultData(flightResultResponse.FlightResultData);

        switch (flightResultResponse.FlightResult)
        {
            case FlightResult.FuelRanOut:
                return new SpaceshipResultResponse(SpaceshipResult.NotEnoughFuel, spaceshipResultData);
            case FlightResult.PartiallyOvercome:
                return new SpaceshipResultResponse(SpaceshipResult.NotEnoughFuel, spaceshipResultData);
            case FlightResult.EngineNotSuit:
                return new SpaceshipResultResponse(SpaceshipResult.NoSuitableEngines, spaceshipResultData);
        }

        foreach (IImpediment impediment in environment.Impediments)
        {
            if (spaceship.Emitter != null)
            {
                if (impediment.DamagePoints > 0)
                    impediment.AcceptImpedimentService(spaceship.Emitter.AcceptEmitterVisitor(emitterVisitor));
            }

            if (spaceship.Deflector != null)
            {
                if (impediment.DamagePoints > 0)
                {
                    impediment.AcceptImpedimentService(spaceship.Deflector.AcceptDeflectorVisitor(deflectorVisitor));
                }
            }

            if (spaceship.Armor == null) continue;
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

        if (flightResult.FlightResultData != null)
            spaceship.CurrentVelocity = flightResult.FlightResultData.CurrentVelocity;

        switch (flightResult.FlightResult)
        {
            case FlightResult.Overcome:
                if (flightResult.FlightResultData == null)
                    throw new NoNullAllowedException(nameof(flightResult.FlightResultData));

                environment.Length -= flightResult.FlightResultData.Length;
                break;
            case FlightResult.PartiallyOvercome:
                if (flightResult.FlightResultData == null)
                    throw new NoNullAllowedException(nameof(flightResult.FlightResultData));

                environment.Length -= flightResult.FlightResultData.Length;
                break;
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

        if (firstFlightResponse.FlightResultData != null)
            spaceship.CurrentVelocity = firstFlightResponse.FlightResultData.CurrentVelocity;

        FlightResult flightResult = firstFlightResponse.FlightResult;
        FlightResultData? flightResultData = firstFlightResponse.FlightResultData;

        if (flightResultData != null)
        {
            environment.Length -= flightResultData.Length;
            spaceship.CurrentVelocity = flightResultData.CurrentVelocity;
        }

        switch (firstFlightResponse.FlightResult)
        {
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
                if (firstFlightResponse.FlightResultData == null)
                    throw new NoNullAllowedException(nameof(firstFlightResponse.FlightResultData));

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

                if (secondEngineResponsePo.FlightResultData != null)
                {
                    environment.Length -= secondEngineResponsePo.FlightResultData.Length;
                    spaceship.CurrentVelocity = secondEngineResponsePo.FlightResultData.CurrentVelocity;

                    flightResultData = new FlightResultData(
                        secondEngineResponsePo.FlightResultData.Time + firstFlightResponse.FlightResultData.Time,
                        secondEngineResponsePo.FlightResultData.CurrentVelocity,
                        secondEngineResponsePo.FlightResultData.Length + firstFlightResponse.FlightResultData.Length,
                        secondEngineResponsePo.FlightResultData.Fuel + firstFlightResponse.FlightResultData.Fuel);
                }

                break;
            }

            case FlightResult.Overcome:
            {
                if (firstFlightResponse.FlightResultData == null)
                    throw new NoNullAllowedException(nameof(firstFlightResponse.FlightResultData));

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

            if (response.SpaceshipResultData != null)
            {
                data.Time += response.SpaceshipResultData.Time;
                data.CurrentVelocity = response.SpaceshipResultData.CurrentVelocity;
                data.Length += response.SpaceshipResultData.Length;
                data.Fuel += response.SpaceshipResultData.Fuel;
            }

            if (response.SpaceshipResult != SpaceshipResult.Overcome)
            {
                return new SpaceshipResultResponse(response.SpaceshipResult, data);
            }
        }

        return new SpaceshipResultResponse(SpaceshipResult.Overcome, data);
    }
}