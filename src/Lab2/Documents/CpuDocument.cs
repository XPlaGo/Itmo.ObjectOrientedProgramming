using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Models.DocumentIds;

namespace Itmo.ObjectOrientedProgramming.Lab2.Documents;

public class CpuDocument : IDocument
{
    public CpuDocument(
        DocumentId id,
        long corsFrequency,
        int corsNumber,
        string socket,
        bool videoCorePresence,
        IReadOnlyList<string> supportedMemoryVersions,
        float tdp,
        float powerConsumption)
    {
        Id = id;
        CorsFrequency = corsFrequency;
        CorsNumber = corsNumber;
        Socket = socket;
        VideoCorePresence = videoCorePresence;
        SupportedMemoryVersions = supportedMemoryVersions;
        Tdp = tdp;
        PowerConsumption = powerConsumption;
    }

    public CpuDocument(
        string id,
        long corsFrequency,
        int corsNumber,
        string socket,
        bool videoCorePresence,
        IReadOnlyList<string> supportedMemoryVersions,
        long tdp,
        int powerConsumption)
        : this(
            new DocumentId(id),
            corsFrequency,
            corsNumber,
            socket,
            videoCorePresence,
            supportedMemoryVersions,
            tdp,
            powerConsumption)
    {
    }

    public DocumentId Id { get; set; }
    public long CorsFrequency { get; set; }
    public int CorsNumber { get; set; }
    public string Socket { get; set; }
    public bool VideoCorePresence { get; set; }
    public IReadOnlyList<string> SupportedMemoryVersions { get; set; }
    public float Tdp { get; set; }
    public float PowerConsumption { get; set; }

    internal class CpuDocumentBuilder
    {
        private DocumentId? _id;
        private long _corsFrequency;
        private int _corsNumber;
        private string? _socket;
        private bool _videoCorePresence;
        private IReadOnlyList<string>? _supportedMemoryFrequencies;
        private float _tdp;
        private int _powerConsumption;

        public CpuDocumentBuilder Id(string id)
        {
            _id = new DocumentId(id);
            return this;
        }

        public CpuDocumentBuilder CorsFrequency(long corsFrequency)
        {
            this._corsFrequency = corsFrequency;
            return this;
        }

        public CpuDocumentBuilder CorsNumber(int corsNumber)
        {
            this._corsNumber = corsNumber;
            return this;
        }

        public CpuDocumentBuilder Socket(string socket)
        {
            this._socket = socket;
            return this;
        }

        public CpuDocumentBuilder VideoCorePresence(bool videoCorePresence)
        {
            this._videoCorePresence = videoCorePresence;
            return this;
        }

        public CpuDocumentBuilder SupportedMemoryFrequencies(IReadOnlyList<string> supportedMemoryFrequencies)
        {
            this._supportedMemoryFrequencies = supportedMemoryFrequencies;
            return this;
        }

        public CpuDocumentBuilder Tdp(float tdp)
        {
            this._tdp = tdp;
            return this;
        }

        public CpuDocumentBuilder PowerConsumption(int powerConsumption)
        {
            _powerConsumption = powerConsumption;
            return this;
        }

        public CpuDocument Build()
        {
            if (_id is null) throw new BuilderNullComponentException(nameof(_id));
            if (_socket is null) throw new BuilderNullComponentException(nameof(_socket));
            if (_supportedMemoryFrequencies is null) throw new BuilderNullComponentException(nameof(_supportedMemoryFrequencies));

            return new CpuDocument(
                _id,
                _corsFrequency,
                _corsNumber,
                _socket,
                _videoCorePresence,
                _supportedMemoryFrequencies,
                _tdp,
                _powerConsumption);
        }
    }
}